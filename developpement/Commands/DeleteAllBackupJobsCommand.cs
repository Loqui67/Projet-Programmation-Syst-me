using AppWPF.developpement.Stores;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Classe qui doit permettre de supprimer tous les travaux de commandes
    ///Class that should allow to delete all command jobs
    public class DeleteAllBackupJobsCommand : AsyncCommandBase
    {
        ///Variable qui instancie la class BackupJobsStore
        ///Variable that instantiates the BackupJobsStore class
        private readonly BackupJobsStore _backupJobsStore;


        ///Méthode qui permet de supprimer tout les travaux de sauvegardes
        ///Method to delete all backup jobs
        public DeleteAllBackupJobsCommand(BackupJobsStore backupJobsStore)
        {
            _backupJobsStore = backupJobsStore;

        }

        ///Méthode qui permet d'éxecuter de maniere asynchrone
        ///Method that allows to execute asynchronously
        public override async Task ExecuteAsync(object parameter)
        {
            {
                try
                {
                    await _backupJobsStore.DeleteAll();
                }
                catch (Exception) { }
            }
        }
    }
}
