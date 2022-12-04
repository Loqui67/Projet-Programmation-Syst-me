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


        public ICommand LoadBackupJobsCommand { get; }

        public BackupJobsListingViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _backupJobsListingItemViewModels = new ObservableCollection<BackupJobsListingItemViewModel>();
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;

            LoadBackupJobsCommand = new LoadBackupJobsCommand(_backupJobsStore);

            _backupJobsStore.BackupJobAdded += BackupJobsStore_BackupJobAdded;
            _backupJobsStore.BackupJobUpdated += BackupJobsStore_BackupJobUpdated;
            _backupJobsStore.BackupJobDeleted += BackupJobsStore_BackupJobDeleted;
            _backupJobsStore.AllBackupJobsDeleted += BackupJobsStore_AllBackupJobsDeleted;
            _backupJobsStore.BackupJobsLoaded += BackupJobsStore_BackupJobsLoaded;

        }

        public static BackupJobsListingViewModel LoadViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            BackupJobsListingViewModel viewModel = new BackupJobsListingViewModel(modalNavigationStore, backupJobsStore);
            viewModel.LoadBackupJobsCommand.Execute(null);

            return viewModel;
        }

        protected override void Dispose()
        {
            _backupJobsStore.BackupJobAdded -= BackupJobsStore_BackupJobAdded;
            _backupJobsStore.BackupJobUpdated -= BackupJobsStore_BackupJobUpdated;
            _backupJobsStore.BackupJobDeleted -= BackupJobsStore_BackupJobDeleted;
            _backupJobsStore.AllBackupJobsDeleted -= BackupJobsStore_AllBackupJobsDeleted;
            _backupJobsStore.BackupJobsLoaded -= BackupJobsStore_BackupJobsLoaded;
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


        private void BackupJobsStore_BackupJobsLoaded()
        {
            _backupJobsListingItemViewModels.Clear();

            foreach (BackupJob backupJob in _backupJobsStore.backupJobs)
            {
                AddBackupJob(backupJob);
            }
        }

        private void BackupJobsStore_BackupJobDeleted(Guid id)
        {
            BackupJobsListingItemViewModel itemViewModel = _backupJobsListingItemViewModels.FirstOrDefault(y => y.BackupJob?.Id == id);

            if (itemViewModel != null)
            {
                _backupJobsListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void BackupJobsStore_AllBackupJobsDeleted()
        {
            _backupJobsListingItemViewModels.Clear();
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
