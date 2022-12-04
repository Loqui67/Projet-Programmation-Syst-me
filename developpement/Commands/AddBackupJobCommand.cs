using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class AddBackupJobCommand : AsyncCommandBase
    {
        private readonly AddBackupJobViewModel _addBackupJobViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        public AddBackupJobCommand(AddBackupJobViewModel addBackupJobViewModel, ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _addBackupJobViewModel = addBackupJobViewModel;
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            BackupJobDetailsFormViewModel formViewModel = _addBackupJobViewModel.BackupJobDetailsFormViewModel;
            formViewModel.IsSubmitting = true;
            BackupJob backupJob = new BackupJob (
                Guid.NewGuid(),
                formViewModel.Name,
                formViewModel.SourcePath,
                formViewModel.DestinationPath,
                formViewModel.Type
            );

            try
            {
                await _backupJobsStore.Add(backupJob);
                _modalNavigationStore.Close();
            }
            catch (Exception) { }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
