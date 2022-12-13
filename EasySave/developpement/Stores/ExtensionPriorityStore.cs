using EasySave.developpement.Models;
using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Stores
{
    public class ExtensionPriorityStore
    {
        public List<ExtensionPriority>? allExtensionPriority;

        public event Action<ExtensionPriority> ExtensionPriorityAdded;
        public event Action<Guid> ExtensionPriorityDeleted;
        public event Action ExtensionPriorityLoaded;

        public ExtensionPriorityStore()
        {
            allExtensionPriority = new List<ExtensionPriority>();
        }

        public async Task Add(ExtensionPriority extensionPriority)
        {
            BackupJobsViewModel.config.AllExtensionPriority.Add(extensionPriority);
            await Task.Run(() => FileManager.SaveConfig(BackupJobsViewModel.config));
            allExtensionPriority = BackupJobsViewModel.config.AllExtensionPriority;
            ExtensionPriorityAdded?.Invoke(extensionPriority);
        }

        public async Task Delete(Guid ExtensionPriorityId)
        {
            BackupJobsViewModel.config.AllExtensionPriority.Remove(allExtensionPriority.Find(extensionPriority => extensionPriority.Id == ExtensionPriorityId));
            await Task.Run(() => FileManager.SaveConfig(BackupJobsViewModel.config));
            allExtensionPriority = BackupJobsViewModel.config.AllExtensionPriority;
            ExtensionPriorityDeleted?.Invoke(ExtensionPriorityId);
        }

        public async Task Load()
        {
            allExtensionPriority = BackupJobsViewModel.config.AllExtensionPriority;
            allExtensionPriority ??= new List<ExtensionPriority>();
            ExtensionPriorityLoaded?.Invoke();
        }
    }
}
