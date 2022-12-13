using EasySave.developpement.Models;
using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasySave.developpement.Stores
{
    public class ProcessusStore
    {
        public List<Processus>? allProcessus;

        public event Action<Processus> ProcessusAdded;
        public event Action<Guid> ProcessusDeleted;
        public event Action ProcessusLoaded;

        public ProcessusStore()
        {
            allProcessus = new List<Processus>();
        }

        public async Task Add(Processus processus)
        {
            BackupJobsViewModel.config.AllProcessus.Add(processus);
            await Task.Run(() => FileManager.SaveConfig(BackupJobsViewModel.config));
            allProcessus = BackupJobsViewModel.config.AllProcessus;
            ProcessusAdded?.Invoke(processus);
        }

        public async Task Delete(Guid ProcessusId)
        {
            BackupJobsViewModel.config.AllProcessus.Remove(allProcessus.Find(processus => processus.Id == ProcessusId));
            await Task.Run(() => FileManager.SaveConfig(BackupJobsViewModel.config));
            allProcessus = BackupJobsViewModel.config.AllProcessus;
            ProcessusDeleted?.Invoke(ProcessusId);
        }

        public async Task Load()
        {
            allProcessus = BackupJobsViewModel.config.AllProcessus;
            allProcessus ??= new List<Processus>();
            ProcessusLoaded?.Invoke();
        }
    }
}
