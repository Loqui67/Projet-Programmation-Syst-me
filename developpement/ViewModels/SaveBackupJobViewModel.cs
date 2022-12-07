using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    ///Classe permettant de sauvegarder les travaux de sauvegarde dans le ViewModel
    ///Class to save backup jobs in the ViewModel
    public class SaveBackupJobViewModel : ViewModelBase
    {
        ///Variable permettant de faire fonctionner la fonction Sauvegarder
        ///Variable to make the Save function work
        public ICommand SaveCommand { get; }
        ///Variable permettant de faire fonctionner la fonction annuler
        ///Variable to make the Cancel function work
        public ICommand CancelCommand { get; }

        ///Variable privée permettant de faire fonctionner la barre de progression pour la sauvegarde
        ///Private variable used  to make the progress bar work for the backup
        private float _BackupJobProgressBarValue;
        ///Variable publique permettant de faire fonctionner la barre de progression pour la sauvegarde
        ///Public variable used  to make the progress bar work for the backup
        public float BackupJobProgressBarValue
        {
            get { return _BackupJobProgressBarValue; }
            set
            {
                _BackupJobProgressBarValue = value;
                OnPropertyChanged(nameof(BackupJobProgressBarValue));
            }
        }

        ///Variable privée permettant de faire fonctionner la barre de progression pour le transfert de fichier
        ///Variable used to make the progress bar work for the file transfer
        private string _backupJobFileTransfering;
        ///Variable publique permettant de faire fonctionner la barre de progression pour le transfert de fichier
        ///Public variable used to make the progress bar work for the file transfer
        public string BackupJobFileTransfering
        {
            get { return _backupJobFileTransfering; }
            set
            {
                _backupJobFileTransfering = value;
                OnPropertyChanged(nameof(BackupJobFileTransfering));
            }
        }

        ///Variable privée permettant de compter le nombre de fichiers à transférer
        ///Private variable used to count the number of files to transfer
        private string _backupJobFileTransferingCount;
        ///Variable publique permettant de compter le nombre de fichiers à transférer
        ///Public variable used to count the number of files to transfer
        public string BackupJobFileTransferingCount
        {
            get { return _backupJobFileTransferingCount; }
            set
            {
                _backupJobFileTransferingCount = value;
                OnPropertyChanged(nameof(BackupJobFileTransferingCount));
            }
        }

        ///Variable privée utilisé pour savoir l'état de chargement
        ///Private variable used to know the loading state
        private string _isLoadingStats = "Visible";
        ///Variable publique utilisé pour savoir l'état de chargement
        ///Public variable used to know the loading state
        public string IsLoadingStats
        {
            get { return _isLoadingStats; }
            set
            {
                _isLoadingStats = value;
                OnPropertyChanged(nameof(IsLoadingStats));
            }
        }

        ///Méthode utilisa pour sauvegarder les travaux de sauvegarde dans le ViewModel
        ///Method used to save backup jobs in the ViewModel
        public SaveBackupJobViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            SaveCommand = new SaveBackupJobCommand(this, backupJob, backupJobsStore, modalNavigationStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }
    }
}
