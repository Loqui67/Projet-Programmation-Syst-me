using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Diagnostics;
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
        private readonly SaveAllBackupJobsViewModel _saveAllBackupJobsViewModel;
        private readonly SaveBackupJobStatusViewModel _saveBackupJobStatusViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;

        ///Méthode qui permet de sauvegarder tous les travaux de commandes
        ///Method that saves all command jobs
        public SaveAllBackupJobsCommand(BackupJobsStore backupJobsStore, SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, SaveBackupJobStatusViewModel saveBackupJobStatusViewModel, ModalNavigationStore modalNavigationStore)
        {
            _backupJobsStore = backupJobsStore;
            _saveAllBackupJobsViewModel = saveAllBackupJobsViewModel;
            _saveBackupJobStatusViewModel = saveBackupJobStatusViewModel;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _saveAllBackupJobsViewModel.IsNotSaving = false;
            try
            {
                await _backupJobsStore.SaveAll(_saveAllBackupJobsViewModel, _saveBackupJobStatusViewModel);
            }
            catch (Exception) { }
            finally
            {
                _saveAllBackupJobsViewModel.IsNotSaving = true;
                _modalNavigationStore.Close();
            }
        }
    }
}
