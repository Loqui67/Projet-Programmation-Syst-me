using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;

namespace AppWPF.developpement.Commands
{
    public class OpenEditBackupJobCommand : CommandBase
    {
        private readonly BackupJobsListingItemViewModel _backupJobsListingItemViewModel;
        private readonly BackupJobsStore _backupJobsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditBackupJobCommand(BackupJobsListingItemViewModel backupJobsListingItemViewModel, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            _backupJobsListingItemViewModel = backupJobsListingItemViewModel;
            _backupJobsStore = backupJobsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            BackupJob backupJob = _backupJobsListingItemViewModel.BackupJob;
            EditBackupJobViewModel editBackupJobViewModel = new EditBackupJobViewModel(backupJob, _backupJobsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editBackupJobViewModel;
        }
    }
}
