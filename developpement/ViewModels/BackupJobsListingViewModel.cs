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
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        public IEnumerable<BackupJobsListingItemViewModel> BackupJobsListingItemViewModels 
            => _backupJobsListingItemViewModels;

        public BackupJobsListingViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _backupJobsListingItemViewModels = new ObservableCollection<BackupJobsListingItemViewModel>();
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;

            _backupJobsStore.BackupJobAdded += BackupJobsStore_BackupJobAdded;
            _backupJobsStore.BackupJobUpdated += BackupJobsStore_BackupJobUpdated;
        }

        protected override void Dispose()
        {
            _backupJobsStore.BackupJobAdded -= BackupJobsStore_BackupJobAdded;
            _backupJobsStore.BackupJobUpdated -= BackupJobsStore_BackupJobUpdated;
            base.Dispose();
        }
        
        private void BackupJobsStore_BackupJobUpdated(BackupJob backupJob)
        {
            BackupJobsListingItemViewModel backupJobViewModel = _backupJobsListingItemViewModels.FirstOrDefault(vm => vm.BackupJob.Id == backupJob.Id);
            if (backupJobViewModel != null)
            {
                backupJobViewModel.Update(backupJob);
            }
        }

        private void BackupJobsStore_BackupJobAdded(BackupJob backupJob)
        {
            AddBackupJob(backupJob);
        }

        private void AddBackupJob(BackupJob backupJob)
        {
            BackupJobsListingItemViewModel itemViewModel = new BackupJobsListingItemViewModel(backupJob, _backupJobsStore, _modalNavigationStore);
            _backupJobsListingItemViewModels.Add(itemViewModel);
        }
    }
}
