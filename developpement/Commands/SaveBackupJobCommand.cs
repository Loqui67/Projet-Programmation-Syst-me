using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class SaveBackupJobCommand : AsyncCommandBase
    {
        private readonly BackupJobsListingItemViewModel _backupJobsListingItemViewModel;
        private readonly BackupJobsStore _backupJobsStore;

        public SaveBackupJobCommand(BackupJobsListingItemViewModel backupJobsListingItemViewModel, BackupJobsStore backupJobsStore)
        {
            _backupJobsListingItemViewModel = backupJobsListingItemViewModel;
            _backupJobsStore = backupJobsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            {
                await Task.Delay(10000);
                try
                {
                    await _backupJobsStore.Save(new BackupJob(
                        _backupJobsListingItemViewModel.BackupJobId,
                        _backupJobsListingItemViewModel.Name,
                        _backupJobsListingItemViewModel.SourcePath,
                        _backupJobsListingItemViewModel.DestinationPath,
                        _backupJobsListingItemViewModel.Type
                    ));
                }
                catch (Exception) { }
            }
        }
    }
}
