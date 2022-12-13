using EasySave.developpement.Models;
using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;

namespace EasySave.developpement.Commands
{
    ///Classe qui permet d'ouvrir la sauvegarde d'un travail de sauvegarde
    ///Class that opens the backup of a backup job
    public class OpenSaveBackupJobCommand : CommandBase
    {
        ///Variable qui permet d'instancier plusieurs classes
        ///Variable that allows to instantiate several classes
        private readonly BackupJobsListingItemViewModel _backupJobsListingItemViewModel;
        private readonly BackupJobsStore _backupJobsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        ///Méthode qui permet d'ouvrir la sauvegarde d'un travail de sauvegarde
        ///Method to open the backup of a backup job
        public OpenSaveBackupJobCommand(BackupJobsListingItemViewModel backupJobsListingItemViewModel, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            _backupJobsListingItemViewModel = backupJobsListingItemViewModel;
            _backupJobsStore = backupJobsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            BackupJob backupJob = _backupJobsListingItemViewModel.BackupJob;
            SaveBackupJobViewModel saveBackupJobViewModel = new SaveBackupJobViewModel(backupJob, _backupJobsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = saveBackupJobViewModel;
        }
    }
}
