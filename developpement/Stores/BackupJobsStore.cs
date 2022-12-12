using AppWPF.developpement.Models;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppWPF.developpement.Stores
{
    public class BackupJobsStore
    {
        public List<BackupJob>? backupJobs;

        public event Action<BackupJob> BackupJobAdded;
        public event Action<BackupJob> BackupJobUpdated;
        public event Action<Guid> BackupJobDeleted;
        public event Action AllBackupJobsDeleted;
        public event Action BackupJobsLoaded;

        public event Action<BackupJob> BackupJobSaved;
        public event Action AllBackupJobsSaved;


        public BackupJobsStore()
        {
            backupJobs = new List<BackupJob>();
        }

        public async Task Add(BackupJob backupJob)
        {
            await Task.Run(() => FileManager.AddBackupJobToFile(backupJob));
            backupJobs.Add(backupJob);
            BackupJobAdded?.Invoke(backupJob);
        }

        public async Task Update(BackupJob backupJob)
        {
            await Task.Run(() => FileManager.UpdateBackupJobInFile(backupJob));
            backupJobs[backupJobs.FindIndex(x => x.Id == backupJob.Id)] = backupJob;
            BackupJobUpdated?.Invoke(backupJob);
        }

        public async Task Delete(Guid backupJobId)
        {
            await Task.Run(() => FileManager.RemoveBackupJobFromFile(backupJobId));
            backupJobs.Remove(backupJobs.Find(backupJob => backupJob.Id == backupJobId));
            BackupJobDeleted?.Invoke(backupJobId);
        }

        public async Task DeleteAll()
        {
            await Task.Run(() => FileManager.RemoveAllBackupJobFromFile());
            backupJobs.Clear();
            AllBackupJobsDeleted?.Invoke();
        }

        public async Task Load()
        {
            backupJobs = FileManager.ReadBackupJobFile();
            backupJobs ??= new List<BackupJob>();

            BackupJobsLoaded?.Invoke();
        }

        public async Task Save(BackupJob backupJob, SaveBackupJobStatusViewModel saveBackupJobStatusViewModel, SaveBackupJobViewModel saveBackupJobViewModel, ModalNavigationStore _modalNavigationStore)
        {
            BackupJobSaver.saveBackupJobStatusViewModel = saveBackupJobStatusViewModel;
            BackupJobSaver.saveBackupJobViewModel = saveBackupJobViewModel;
            BackupJobSaver.modalNavigationStore = _modalNavigationStore;
            await Task.Run(async () =>
            {
                SaveFiles saveFiles = new SaveFiles();
                await saveFiles.GetInfos(backupJob);
                await BackupJobSaver.StartSave(saveFiles);
            });
            BackupJobSaved?.Invoke(backupJob);
        }


        public async Task SaveAll(SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, ModalNavigationStore modalNavigationStore)
        {
            int countSaveDone = 0;
            int saveNumber = backupJobs.Count;
            BackupJobSaver.fileNumberTotal = 0;
            BackupJobSaver.fileSizeTotal = 0;


            BackupJobSaver.saveAllBackupJobsViewModel = saveAllBackupJobsViewModel;
            List<SaveFiles> saveFilesList = new();
            saveAllBackupJobsViewModel.CurrentBackupJob = countSaveDone + "/" + saveNumber;

            saveAllBackupJobsViewModel.IsSaving = true;
            List<Log> logs = new();
            foreach (BackupJob backupJob in backupJobs)
            {
                SaveFiles saveFiles = new();
                await saveFiles.GetInfos(backupJob);
                
                BackupJobSaver.fileNumberTotal += saveFiles.FileNumberTotal;
                BackupJobSaver.fileSizeTotal += saveFiles.FileSizeTotal;
                BackupJobSaver.fileNumberLeft = BackupJobSaver.fileNumberTotal;
                BackupJobSaver.fileSizeLeft = BackupJobSaver.fileSizeTotal;

                saveFilesList.Add(saveFiles);

                Action action = new(() =>
                {
                    BackupJobSaver.SetIsPausedAllSave(false);
                    BackupJobSaver.isStoppingEncrypting = false;
                    BackupJobSaver.isStoppingCopying = false;
                    BackupJobSaver.isPausing = false;
                    saveAllBackupJobsViewModel.IsSaving = false;

                    BackupJobSaver.WriteToDailyLog(logs);
                    modalNavigationStore.Close();
                });
                Thread t = new(async () =>
                {
                    Log log = await BackupJobSaver.StartListSaveInParallel(saveFiles);
                    logs.Add(log);
                    countSaveDone++;
                    saveAllBackupJobsViewModel.CurrentBackupJob = countSaveDone + "/" + saveNumber;
                    if (countSaveDone == saveNumber) action.Invoke();
                });
                t.Start();
            }
        }


        public static void PauseSave()
        {
            BackupJobSaver.PauseSave();
        }

        public static void ResumeSave()
        {
            BackupJobSaver.ResumeSave();
        }

        public static void StopSave()
        {
            BackupJobSaver.StopSave();
        }
    }
}