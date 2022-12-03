using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobsListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BackupJobsListingItemViewModel> _backupJobsListingItemViewModels;
        public IEnumerable<BackupJobsListingItemViewModel> BackupJobsListingItemViewModels 
            => _backupJobsListingItemViewModels;

        public BackupJobsListingViewModel()
        {
            _backupJobsListingItemViewModels = new ObservableCollection<BackupJobsListingItemViewModel>();

            _backupJobsListingItemViewModels.Add(new BackupJobsListingItemViewModel(new BackupJob {
                Name = "BackupJob1",
                SourcePath = "C:\\",
                DestinationPath = "D:\\",
                Type = "1" 
            }));
        }
    }
}
