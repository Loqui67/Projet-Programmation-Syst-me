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
    ///Classe hérité de ViewModelBase et permet de sauvegarder tous les travaux de sauvegarde et les "envoyé" vers la vue
    ///Class inherited from ViewModelBase and allows saving all backup jobs and "sending" them to the view
    public class SaveAllBackupJobsViewModel : ViewModelBase
    {
        public SaveBackupJobStatusViewModel SaveBackupJobStatusViewModel { get; }

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

        private bool _isSaving = true;

        public bool IsNotSaving
        {
            get { return _isSaving; }
            set
            {
                _isSaving = value;
                OnPropertyChanged(nameof(IsNotSaving));
            }
        }

        ///Variables permettant de stocker la valeur de la sauvegarde de tous travaux de commandes
        ///Variables storing the value of saving all backup jobs
        public ICommand SaveAllBackupJobsCommand { get; }
        ///Variable permettant de stocker la valeur de l'annulation de la sauvegarde des travaux de sauvegarde
        ///Variable storing the value of canceling the backup job save
        public ICommand CancelSaveAllBackupJobsCommand { get; }

        ///Méthode permettant de sauvegarder tous les travaux de sauvegarde dans le ViewModel
        ///Method for saving all backup jobs in the ViewModel
        public SaveAllBackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            SaveBackupJobStatusViewModel = new SaveBackupJobStatusViewModel();
            CancelSaveAllBackupJobsCommand = new CloseModalCommand(modalNavigationStore);
            SaveAllBackupJobsCommand = new SaveAllBackupJobsCommand(backupJobsStore, this, SaveBackupJobStatusViewModel, modalNavigationStore);
        }
    }
}
