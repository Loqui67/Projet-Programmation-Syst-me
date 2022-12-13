using EasySave.developpement.Models;
using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Stores
{
    public class ExtensionCryptageStore
    {
        public List<ExtensionCryptage>? allExtensionCryptage;

        public event Action<ExtensionCryptage> ExtensionCryptageAdded;
        public event Action<Guid> ExtensionCryptageDeleted;
        public event Action ExtensionCryptageLoaded;

        public ExtensionCryptageStore()
        {
            allExtensionCryptage = new List<ExtensionCryptage>();
        }

        public async Task Add(ExtensionCryptage extensionCryptage)
        {
            BackupJobsViewModel.config.AllExtensionCryptage.Add(extensionCryptage);
            await Task.Run(() => FileManager.SaveConfig(BackupJobsViewModel.config));
            allExtensionCryptage = BackupJobsViewModel.config.AllExtensionCryptage;
            ExtensionCryptageAdded?.Invoke(extensionCryptage);
        }

        public async Task Delete(Guid ExtensionCryptageId)
        {
            BackupJobsViewModel.config.AllExtensionCryptage.Remove(allExtensionCryptage.Find(extensionCryptage => extensionCryptage.Id == ExtensionCryptageId));
            await Task.Run(() => FileManager.SaveConfig(BackupJobsViewModel.config));
            allExtensionCryptage = BackupJobsViewModel.config.AllExtensionCryptage;
            ExtensionCryptageDeleted?.Invoke(ExtensionCryptageId);
        }

        public async Task Load()
        {
            allExtensionCryptage = BackupJobsViewModel.config.AllExtensionCryptage;
            allExtensionCryptage ??= new List<ExtensionCryptage>();
            ExtensionCryptageLoaded?.Invoke();
        }
    }
}
