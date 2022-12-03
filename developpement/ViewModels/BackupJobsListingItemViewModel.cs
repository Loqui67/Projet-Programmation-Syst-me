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
        public string Name { get; }
        public string SourcePath { get; }
        public string DestinationPath { get; }
        public string Type { get; }

        public ICommand EditBackupJobCommand { get; }
        public ICommand DeleteBackupJobCommand { get; }

        public BackupJobsListingItemViewModel(BackupJob backupJob)
        {
            Name = backupJob.Name;
            SourcePath = backupJob.SourcePath;
            DestinationPath = backupJob.DestinationPath;
            Type = backupJob.Type.ToString();
        }
    }
}
