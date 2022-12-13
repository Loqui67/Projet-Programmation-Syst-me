using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Commands
{
    ///Classes qui permet d'ouvrir la sauvegarde de tous les travails de sauvegarde
    ///Classes that allows to open the backup of all backup jobs
    public class OpenSaveAllBackupJobsCommand : CommandBase
    {
        ///Variable qui permet d'instancier plusieurs classes
        ///Variable that allows to instantiate several classes
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        ///Méthode qui permet d'ouvrir la sauvegarde de tous les travails de sauvegarde
        ///Method to open backup of all backup jobs
        public OpenSaveAllBackupJobsCommand(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;
        }

        public override void Execute(object parameter)
        {
            SaveAllBackupJobsViewModel saveAllBackupJobsViewModel = new SaveAllBackupJobsViewModel(_modalNavigationStore, _backupJobsStore);
            _modalNavigationStore.CurrentViewModel = saveAllBackupJobsViewModel;
        }
    }
}
