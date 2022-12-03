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
    public class BackupJobsViewModel : ViewModelBase
    {
        public BackupJobsListingViewModel BackupJobsListingViewModel { get; }
        public ICommand AddBackupJobCommand { get; }

        public BackupJobsViewModel(ModalNavigationStore modalNavigationStore)
        {
            BackupJobsListingViewModel = new BackupJobsListingViewModel(modalNavigationStore);

            AddBackupJobCommand = new OpenAddBackupJobCommand(modalNavigationStore);
        }
    }
}
