using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class OpenSaveAllBackupJobsCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        public OpenSaveAllBackupJobsCommand(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;
        }

        public override void Execute(object parameter)
        {
            SaveAllBackupJobsViewModel saveAllBackupJobsViewModel = new SaveAllBackupJobsViewModel(_modalNavigationStore, _backupJobsStore);
            _modalNavigationStore.CurrentViewModel = saveAllBackupJobsViewModel;
        }
    }
}
