using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobsListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BackupJobsListingItemViewModel> _backupJobsListingItemViewModels;
        public IEnumerable<BackupJobsListingItemViewModel> BackupJobsListingItemViewModels 
            => _backupJobsListingItemViewModels;

        public BackupJobsListingViewModel(ModalNavigationStore modalNavigationStore)
        {
            _backupJobsListingItemViewModels = new ObservableCollection<BackupJobsListingItemViewModel>();

            AddBackupJob(new BackupJob {
                Name = "BackupJob1",
                SourcePath = "C:\\",
                DestinationPath = "D:\\",
                Type = "0" 
            }, modalNavigationStore);
        }

        private void AddBackupJob(BackupJob backupJob, ModalNavigationStore modalNavigationStore)
        {
            ICommand editCommand = new OpenEditBackupJobCommand(backupJob, modalNavigationStore);
            _backupJobsListingItemViewModels.Add(new BackupJobsListingItemViewModel(backupJob, editCommand));
        }
    }
}
