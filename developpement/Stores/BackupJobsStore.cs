using AppWPF.developpement.Models;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
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
                await BackupJobSaver.StartSave(saveFiles, backupJob);
            });
            BackupJobSaved?.Invoke(backupJob);
        }

        public async Task SaveAll(SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, SaveBackupJobStatusViewModel saveBackupJobStatusViewModel)
        {
            BackupJobSaver.saveBackupJobStatusViewModel = saveBackupJobStatusViewModel;
            await Task.Run(async () =>
            {
                List<SaveFiles> saveFilesList = new List<SaveFiles>();
                foreach (BackupJob backupJob in backupJobs)
                {
                    SaveFiles saveFiles = new SaveFiles();
                    await saveFiles.GetInfos(backupJob);
                    saveFilesList.Add(saveFiles);
                }
                await BackupJobSaver.StartListSaveInParallel(saveFilesList, saveAllBackupJobsViewModel, backupJobs);
            });
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