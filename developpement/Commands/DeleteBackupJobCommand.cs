using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.Commands
{
    public class DeleteBackupJobCommand : AsyncCommandBase
    {
        private readonly BackupJobsListingItemViewModel _backupJobsListingItemViewModel;
        private readonly BackupJobsStore _backupJobsStore;

        public DeleteBackupJobCommand(BackupJobsListingItemViewModel backupJobsListingItemViewModel, BackupJobsStore backupJobsStore)
        {
            _backupJobsListingItemViewModel = backupJobsListingItemViewModel;
            _backupJobsStore = backupJobsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            {
                try
                {
                    await _backupJobsStore.Delete(_backupJobsListingItemViewModel.BackupJobId);
                }
                catch (Exception) { }
            }
        }

    }
    
}
