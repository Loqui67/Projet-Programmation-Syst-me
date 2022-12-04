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
