using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;

namespace AppWPF.developpement.Commands
{
    public class OpenAddBackupJobCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        public OpenAddBackupJobCommand(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;
        }

        public override void Execute(object parameter)
        {
            AddBackupJobViewModel addBackupJobViewModel = new AddBackupJobViewModel(_modalNavigationStore, _backupJobsStore);
            _modalNavigationStore.CurrentViewModel = addBackupJobViewModel;
        }

    }
}
