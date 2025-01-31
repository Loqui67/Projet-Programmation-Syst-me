﻿using EasySave.developpement.Commands;
using EasySave.developpement.Models;
using EasySave.developpement.Stores;
using System.Diagnostics;
using System.Windows.Input;

namespace EasySave.developpement.ViewModels
{
    ///Classe permettant de sauvegarder les travaux de sauvegarde dans le ViewModel
    ///Class to save backup jobs in the ViewModel
    public class SaveBackupJobViewModel : ViewModelBase
    {

        public SaveBackupJobStatusViewModel SaveBackupJobStatusViewModel { get; }

        ///Variable permettant de faire fonctionner la fonction Sauvegarder
        ///Variable to make the Save function work
        public ICommand SaveCommand { get; }
        ///Variable permettant de faire fonctionner la fonction annuler
        ///Variable to make the Cancel function work
        public ICommand CancelCommand { get; }

        public ICommand PauseSaveCommand { get; }
        public ICommand StopSaveCommand { get; }
        public ICommand ResumeSaveCommand { get; }


        private bool _isSaving;
        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                _isSaving = value;
                OnPropertyChanged(nameof(IsSaving));
            }
        }

        private bool _isPaused;

        public bool IsPaused
        {
            get { return _isPaused; }
            set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(IsPaused));
            }
        }


        ///Méthode utilisa pour sauvegarder les travaux de sauvegarde dans le ViewModel
        ///Method used to save backup jobs in the ViewModel
        public SaveBackupJobViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            SaveBackupJobStatusViewModel = new SaveBackupJobStatusViewModel();
            SaveCommand = new SaveBackupJobCommand(this, SaveBackupJobStatusViewModel, backupJob, backupJobsStore, modalNavigationStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
            PauseSaveCommand = new PauseSaveCommand();
            StopSaveCommand = new StopSaveCommand(modalNavigationStore);
            ResumeSaveCommand = new ResumeSaveCommand();
        }
    }
}
