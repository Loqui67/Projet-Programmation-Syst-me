using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
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
        public Guid BackupJobId { get; }

        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        public EditBackupJobViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            BackupJobId = backupJob.Id;

            ICommand submitCommand = new EditBackupJobCommand(this, backupJobsStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel(submitCommand, cancelCommand)
            {
                Name = backupJob.Name,
                SourcePath = backupJob.SourcePath,
                DestinationPath = backupJob.DestinationPath,
                Type = backupJob.Type,
            };
        }
    }
}
