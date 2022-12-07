﻿using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///CLasse qui permet de sauvegarder tous les travaux de sauvegardes 
    ///Class that saves all backup jobs
    public class SaveBackupJobCommand : AsyncCommandBase
    {
        ///Variables qui permettent d'instancier plusieurs classes
        ///Variables that allow to instantiate several classes
        private readonly SaveBackupJobViewModel _saveBackupJobViewModel;
        private readonly BackupJob _backupJob;
        private readonly BackupJobsStore _backupJobsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        ///Méthode qui permet de sauvegarder tous les travaux de sauvegardes 
        ///Method that saves all backup jobs
        public SaveBackupJobCommand(SaveBackupJobViewModel saveBackupJobViewModel, BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            _saveBackupJobViewModel = saveBackupJobViewModel;
            _backupJob = backupJob;
            _backupJobsStore = backupJobsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _saveBackupJobViewModel.BackupJobProgressBarValue = 0;
            _saveBackupJobViewModel.BackupJobFileTransfering = "";
            _saveBackupJobViewModel.BackupJobFileTransferingCount = "";
            try
            {
                await _backupJobsStore.Save(_backupJob, _saveBackupJobViewModel);
            }
            catch (Exception) { }
            finally
            {
                _modalNavigationStore.Close();
            }
        }
    }
}
