using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class LoadBackupJobsCommand : AsyncCommandBase
    {
        private readonly BackupJobsViewModel _backupJobsViewModel;
        private readonly BackupJobsStore _backupJobsStore;


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
