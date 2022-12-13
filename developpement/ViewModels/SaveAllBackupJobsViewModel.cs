using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class SaveAllBackupJobsViewModel : ViewModelBase
    {
        private float _progressBarAllBackupJobsValue = 0;

        public float ProgressBarAllBackupJobsValue
        {
            get { return _progressBarAllBackupJobsValue; }
            set
            {
                _progressBarAllBackupJobsValue = value;
                OnPropertyChanged(nameof(ProgressBarAllBackupJobsValue));
            }
        }

        private string _allBackupJobProgression = "";

        public string AllBackupJobProgression
        {
            get { return _allBackupJobProgression; }
            set
            {
                _allBackupJobProgression = value;
                OnPropertyChanged(nameof(AllBackupJobProgression));
            }
        }

        private string _currentBackupJob = "";

        public string CurrentBackupJob
        {
            get { return _currentBackupJob; }
            set
            {
                _currentBackupJob = value;
                OnPropertyChanged(nameof(CurrentBackupJob));
            }
        }

        private bool _isSaving = false;

        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                _isSaving = value;
                OnPropertyChanged(nameof(IsSaving));
            }
        }

        private bool _isPaused = false;

        public bool IsPaused
        {
            get { return _isPaused; }
            set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(IsPaused));
            }
        }

        public ICommand SaveAllBackupJobsCommand { get; }
        public ICommand CancelSaveAllBackupJobsCommand { get; }
        public ICommand PauseSaveCommand { get; }
        public ICommand StopSaveCommand { get; }
        public ICommand ResumeSaveCommand { get; }

        public SaveAllBackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            CancelSaveAllBackupJobsCommand = new CloseModalCommand(modalNavigationStore);
            SaveAllBackupJobsCommand = new SaveAllBackupJobsCommand(backupJobsStore, this, modalNavigationStore);
            PauseSaveCommand = new PauseSaveCommand();
            StopSaveCommand = new StopSaveCommand(modalNavigationStore);
            ResumeSaveCommand = new ResumeSaveCommand();
        }
    }
}
//reste a bind _isSaving et _isPaused