using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

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
            _backupJobsListingItemViewModel.IsDeleting = true;
            try
            {
                await _backupJobsStore.Delete(_backupJobsListingItemViewModel.BackupJobId);
            }
            catch (Exception) { }
            finally
            {
                _backupJobsListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
