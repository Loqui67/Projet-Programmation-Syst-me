using EasySave.developpement.Models;
using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;

namespace EasySave.developpement.Commands
{
    ///Classe qui permet d'ouvrir la modification d'un travail de sauvegarde 
    ///Class that allows to open the modification of a backup job
    public class OpenEditBackupJobCommand : CommandBase
    {
        ///Variables qui permettent d'instancier plusieurs classes
        ///Variables that allow to instantiate several classes
        private readonly BackupJobsListingItemViewModel _backupJobsListingItemViewModel;
        private readonly BackupJobsStore _backupJobsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        ///Méthode qui permet d'ouvrir la modification d'un travail de sauvegarde
        ///Method to open a backup job edit
        public OpenEditBackupJobCommand(BackupJobsListingItemViewModel backupJobsListingItemViewModel, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            _backupJobsListingItemViewModel = backupJobsListingItemViewModel;
            _backupJobsStore = backupJobsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            BackupJob backupJob = _backupJobsListingItemViewModel.BackupJob;
            EditBackupJobViewModel editBackupJobViewModel = new EditBackupJobViewModel(backupJob, _backupJobsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editBackupJobViewModel;
        }
    }
}
