using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobsListingItemViewModel : ViewModelBase
    {
        public BackupJob BackupJob { get; }
        public string Name => BackupJob.Name;
        public string SourcePath => BackupJob.SourcePath;
        public string DestinationPath => BackupJob.DestinationPath;
        public string Type => BackupJob.Type;

        public ICommand EditBackupJobCommand { get; }
        public ICommand DeleteBackupJobCommand { get; }

        public BackupJobsListingItemViewModel(BackupJob backupJob, ICommand editCommand)
        {
            BackupJob = backupJob;
            EditBackupJobCommand = editCommand;


        }
    }
}
