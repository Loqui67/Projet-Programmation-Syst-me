using EasySave.developpement.Models;
using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EasySave.developpement.Commands
{
    ///CLasse qui permet de sauvegarder tous les travaux de sauvegardes 
    ///Class that saves all backup jobs
    public class SaveBackupJobCommand : AsyncCommandBase
    {
        private readonly SaveBackupJobViewModel _saveBackupJobViewModel;

        ///Variables qui permettent d'instancier plusieurs classes
        ///Variables that allow to instantiate several classes
        private readonly SaveBackupJobStatusViewModel _saveBackupJobStatusViewModel;
        private readonly BackupJob _backupJob;
        private readonly BackupJobsStore _backupJobsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        ///Méthode qui permet de sauvegarder tous les travaux de sauvegardes 
        ///Method that saves all backup jobs
        public SaveBackupJobCommand(SaveBackupJobViewModel saveBackupJobViewModel, SaveBackupJobStatusViewModel saveBackupJobStatusViewModel, BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            _saveBackupJobViewModel = saveBackupJobViewModel;
            _saveBackupJobStatusViewModel = saveBackupJobStatusViewModel;
            _backupJob = backupJob;
            _backupJobsStore = backupJobsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _saveBackupJobStatusViewModel.BackupJobProgressBarValue = 0;
            _saveBackupJobStatusViewModel.BackupJobFileTransfering = "";
            _saveBackupJobStatusViewModel.BackupJobFileTransferingCount = "";
            try
            {
                await _backupJobsStore.Save(_backupJob, _saveBackupJobStatusViewModel, _saveBackupJobViewModel, _modalNavigationStore);
            }
            catch (Exception) { }
        }
    }
}
