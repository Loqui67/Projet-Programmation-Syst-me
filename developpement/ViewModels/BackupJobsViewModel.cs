using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobsViewModel : ViewModelBase
    {
        public BackupJobsListingViewModel BackupJobsListingViewModel { get; }
        public ICommand CreateBackupJobCommand { get; }

        public BackupJobsViewModel()
        {
            BackupJobsListingViewModel = new BackupJobsListingViewModel();
        }
    }
}
