using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class AddBackupJobViewModel : ViewModelBase
    {
        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        public AddBackupJobViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            ICommand submitCommand = new AddBackupJobCommand(this, modalNavigationStore, backupJobsStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);

            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
