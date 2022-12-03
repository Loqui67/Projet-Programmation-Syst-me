using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class AddBackupJobViewModel : ViewModelBase
    {
        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        public AddBackupJobViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel(null, cancelCommand);
        }
    }
}
