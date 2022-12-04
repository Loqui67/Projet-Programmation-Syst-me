﻿using Projet_Programmation_Système.developpement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public async Task Save(BackupJob backupJob)
        {
            await Task.Run(() => backupJob.Save(false));
            BackupJobSaved?.Invoke(backupJob);
        }

        public async Task SaveAll()
        {
            await Task.Run(() =>
            {
                foreach (BackupJob backupJob in backupJobs)
                {
                    backupJob.Save(false);
                }
            });
            AllBackupJobsSaved?.Invoke();
        }
    }
}
