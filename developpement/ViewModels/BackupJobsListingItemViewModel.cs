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
    public class BackupJobsListingItemViewModel : ViewModelBase
    {
        public BackupJob BackupJob { get; private set; }
        public Guid BackupJobId => BackupJob.Id;
        public string Name => BackupJob.Name;
        public string SourcePath => BackupJob.SourcePath;
        public string DestinationPath => BackupJob.DestinationPath;
        public string Type => BackupJob.Type;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }

        public BackupJobsListingItemViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            BackupJob = backupJob;
            EditCommand = new OpenEditBackupJobCommand(this, backupJobsStore, modalNavigationStore);
            DeleteCommand = new DeleteBackupJobCommand(this, backupJobsStore);
            SaveCommand = new SaveBackupJobCommand(this, backupJobsStore);
        }


        public void Update(BackupJob backupJob)
        {
            BackupJob = backupJob;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(SourcePath));
            OnPropertyChanged(nameof(DestinationPath));
            OnPropertyChanged(nameof(Type));
        }
    }
}
