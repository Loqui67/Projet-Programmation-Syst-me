using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Class qui permet de charger un travail de sauvegarde
    ///Class that allows to load a backup job
    public class LoadBackupJobsCommand : AsyncCommandBase
    {
        ///Variable qui permet d'instancier plusieurs classes
        ///Variable that allows to instantiate several classes
        private readonly BackupJobsViewModel _backupJobsViewModel;
        private readonly BackupJobsStore _backupJobsStore;

        ///Méthode qui permet de charger un travail de sauvegarde 
        ///Method to load a backup job
        public LoadBackupJobsCommand(BackupJobsViewModel backupJobsViewModel, BackupJobsStore backupJobsStore)
        {
            _backupJobsViewModel = backupJobsViewModel;
            _backupJobsStore = backupJobsStore;
        }

    
        public override async Task ExecuteAsync(object parameter)
        {
            _backupJobsViewModel.IsLoading = true;
            try
            {
                await _backupJobsStore.Load();
            }
            catch (Exception) { }
            finally
            {
                _backupJobsViewModel.IsLoading = false;
            }
        }
    }
}
