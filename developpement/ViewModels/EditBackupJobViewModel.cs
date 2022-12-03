using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace AppWPF.developpement.ViewModels
{
    public class EditBackupJobViewModel : ViewModelBase
    {
        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        public EditBackupJobViewModel(BackupJob backupJob, ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel(null, cancelCommand)
            {
                Name = backupJob.Name,
                SourcePath = backupJob.SourcePath,
                DestinationPath = backupJob.DestinationPath,
                Type = backupJob.Type,
            };
        }
    }
}
