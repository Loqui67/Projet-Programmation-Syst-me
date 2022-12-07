using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet de supprimer un travail de commande
    ///Class that allows to delete a command job
    public class DeleteBackupJobCommand : AsyncCommandBase
    {
        ///Variable qui instancie plusieurs class comme par exemple ici backupJobsListingItemViewModel
        ///Variable that instantiates several classes such as here backupJobsListingItemViewModel
        private readonly BackupJobsListingItemViewModel _backupJobsListingItemViewModel;
        private readonly BackupJobsStore _backupJobsStore;

        ///Méthode qui permet de supprimer un travail de sauvegarde
        ///Method to delete a backup job
        public DeleteBackupJobCommand(BackupJobsListingItemViewModel backupJobsListingItemViewModel, BackupJobsStore backupJobsStore)
        {
            _backupJobsListingItemViewModel = backupJobsListingItemViewModel;
            _backupJobsStore = backupJobsStore;
        }

        
        public override async Task ExecuteAsync(object parameter)
        {
            _backupJobsListingItemViewModel.IsDeleting = true;
            try
            {
                await _backupJobsStore.Delete(_backupJobsListingItemViewModel.BackupJobId);
            }
            catch (Exception) { }
            finally
            {
                _backupJobsListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
