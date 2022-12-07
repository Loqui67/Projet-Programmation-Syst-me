using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet de sauvegarder tous les travaux de commandes
    ///Class that saves all command jobs
    public class SaveAllBackupJobsCommand : AsyncCommandBase
    {
        ///Variable qui instancie la classe BackupJobsStore
        ///Variable that instantiates the BackupJobsStore class
        private readonly BackupJobsStore _backupJobsStore;

        ///Méthode qui permet de sauvegarder tous les travaux de commandes
        ///Method that saves all command jobs
        public SaveAllBackupJobsCommand(BackupJobsStore backupJobsStore)
        {
            _backupJobsStore = backupJobsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            {
                try
                {
                    await _backupJobsStore.SaveAll();
                }
                catch (Exception) { }
            }
        }
    }
}
