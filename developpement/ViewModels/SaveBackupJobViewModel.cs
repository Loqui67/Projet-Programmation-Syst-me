using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class SaveBackupJobViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private float _BackupJobProgressBarValue;
        public float BackupJobProgressBarValue
        {
            get { return _BackupJobProgressBarValue; }
            set
            {
                _BackupJobProgressBarValue = value;
                OnPropertyChanged(nameof(BackupJobProgressBarValue));
            }
        }

        private string _backupJobFileTransfering;
        public string BackupJobFileTransfering
        {
            get { return _backupJobFileTransfering; }
            set
            {
                _backupJobFileTransfering = value;
                OnPropertyChanged(nameof(BackupJobFileTransfering));
            }
        }

        private string _backupJobFileTransferingCount;
        public string BackupJobFileTransferingCount
        {
            get { return _backupJobFileTransferingCount; }
            set
            {
                _backupJobFileTransferingCount = value;
                OnPropertyChanged(nameof(BackupJobFileTransferingCount));
            }
        }

        private string _isLoadingStats;
        public string IsLoadingStats
        {
            get { return _isLoadingStats; }
            set
            {
                _isLoadingStats = value;
                OnPropertyChanged(nameof(IsLoadingStats));
            }
        }

        public SaveBackupJobViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            SaveCommand = new SaveBackupJobCommand(this, backupJob, backupJobsStore, modalNavigationStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }
    }
}
