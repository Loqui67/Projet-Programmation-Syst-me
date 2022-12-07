using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class SaveAllBackupJobsViewModel : ViewModelBase
    {
        public ICommand SaveBackupJobCommand { get; }
        public ICommand CancelSaveBackupJobCommand { get; }

        public SaveAllBackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            SaveBackupJobCommand = new SaveAllBackupJobsCommand(backupJobsStore);
            CancelSaveBackupJobCommand = new CloseModalCommand(modalNavigationStore);
        }
    }
}
