using EasySave.developpement.Commands;
using EasySave.developpement.Stores;
using System.Windows.Input;

namespace EasySave.developpement.ViewModels
{
    ///Classe permettant de convertire l'information du travail de sauvegarde vers la vue
    ///Class to convert backup job information to view
    public class AddBackupJobViewModel : ViewModelBase
    {
        ///Variable servant à instancier la classe BackupJobDetailsFormViewModel
        ///Variable used to instantiate the BackupJobDetailsFormViewModel class
        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        ///Méthode servant à ajouter un travail de sauvegarde et à l'envoyer à la vue
        ///Method to add a backup job and submit it to the view
        public AddBackupJobViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            ICommand submitCommand = new AddBackupJobCommand(this, modalNavigationStore, backupJobsStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);

            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
