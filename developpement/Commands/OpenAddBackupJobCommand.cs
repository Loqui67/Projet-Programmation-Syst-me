using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet d'ouvrir un travail de sauvegarde déja éxistant
    ///Class that opens a backup job
    public class OpenAddBackupJobCommand : CommandBase
    {
        ///Variables qui permettent d'instancier plusieurs classes
        ///Variables that allow to instantiate several classes
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        ///Méthode qui permet d'ouvrir un travail de sauvegarde déja éxistant
        ///Method to open an existing backup job
        public OpenAddBackupJobCommand(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;
        }

        public override void Execute(object parameter)
        {
            AddBackupJobViewModel addBackupJobViewModel = new AddBackupJobViewModel(_modalNavigationStore, _backupJobsStore);
            _modalNavigationStore.CurrentViewModel = addBackupJobViewModel;
        }

    }
}
