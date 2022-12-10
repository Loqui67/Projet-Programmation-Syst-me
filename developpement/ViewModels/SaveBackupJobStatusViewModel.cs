using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.ViewModels
{
    public class SaveBackupJobStatusViewModel : ViewModelBase
    {

        ///Variable privée permettant de faire fonctionner la barre de progression pour la sauvegarde
        ///Private variable used  to make the progress bar work for the backup
        private float _backupJobProgressBarValue;
        ///Variable publique permettant de faire fonctionner la barre de progression pour la sauvegarde
        ///Public variable used  to make the progress bar work for the backup
        public float BackupJobProgressBarValue
        {
            get { return _backupJobProgressBarValue; }
            set
            {
                _backupJobProgressBarValue = value;
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
        private string _isLoadingStats = "Collapsed";
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

        public SaveBackupJobStatusViewModel()
        {
            BackupJobProgressBarValue = 0;
            BackupJobFileTransfering = "";
            BackupJobFileTransferingCount = "";
        }
    }
}
