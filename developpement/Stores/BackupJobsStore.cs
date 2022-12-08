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

        public async Task Save(BackupJob backupJob, SaveBackupJobStatusViewModel saveBackupJobStatusViewModel)
        {
            await Task.Run(() => backupJob.CompleteSaveProcedure(saveBackupJobStatusViewModel));
            BackupJobSaved?.Invoke(backupJob);
        }

        public async Task SaveAll(SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, SaveBackupJobStatusViewModel saveBackupJobStatusViewModel)
        {
            await Task.Run(() =>
            {
                int total = backupJobs.Count;
                int i = 0;
                foreach (BackupJob backupJob in backupJobs)
                {
                    i++;
                    saveAllBackupJobsViewModel.CurrentBackupJob = backupJob.Name;
                    saveAllBackupJobsViewModel.AllBackupJobProgression = i + "/" + total.ToString();
                    backupJob.CompleteSaveProcedure(saveBackupJobStatusViewModel);
                    saveAllBackupJobsViewModel.ProgressBarAllBackupJobsValue = i * 100 / total;
                }
                saveAllBackupJobsViewModel.CurrentBackupJob = "";
                saveAllBackupJobsViewModel.AllBackupJobProgression = "";
                AllBackupJobsSaved?.Invoke();
            });                    

        }

        public async Task SaveAllV2(SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, SaveBackupJobStatusViewModel saveBackupJobStatusViewModel)
        {
            //await Task.WhenAll(backupJobs.ForEach(backupJob => backupJob.CompleteSaveProcedure(saveBackupJobStatusViewModel)));
        }
    }
}
//note a essayer : creer un model pour les save et passer les arguments backupjob directement => peut faciliter la gestion en parallèle