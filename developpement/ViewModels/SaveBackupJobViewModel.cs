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


        ///Méthode utilisa pour sauvegarder les travaux de sauvegarde dans le ViewModel
        ///Method used to save backup jobs in the ViewModel
        public SaveBackupJobViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            SaveBackupJobStatusViewModel = new SaveBackupJobStatusViewModel();
            SaveCommand = new SaveBackupJobCommand(SaveBackupJobStatusViewModel, backupJob, backupJobsStore, modalNavigationStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }
    }
}
