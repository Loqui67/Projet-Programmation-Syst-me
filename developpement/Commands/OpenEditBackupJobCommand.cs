using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class OpenEditBackupJobCommand : CommandBase
    {
        private readonly BackupJob backupJob;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditBackupJobCommand(BackupJob backupJob, ModalNavigationStore modalNavigationStore)
        {
            this.backupJob = backupJob;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            EditBackupJobViewModel editBackupJobViewModel = new EditBackupJobViewModel(backupJob, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editBackupJobViewModel;
        }

    }
}
