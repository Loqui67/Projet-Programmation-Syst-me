using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class EditBackupJobCommand : AsyncCommandBase
    {
        private readonly EditBackupJobViewModel _editBackupJobViewModel;
        private readonly BackupJobsStore _backupJobsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditBackupJobCommand(EditBackupJobViewModel editBackupJobViewModel, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            _editBackupJobViewModel = editBackupJobViewModel;
            _backupJobsStore = backupJobsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            BackupJobDetailsFormViewModel formViewModel = _editBackupJobViewModel.BackupJobDetailsFormViewModel;
            BackupJob backupJob = new(
                _editBackupJobViewModel.BackupJobId,
                formViewModel.Name,
                formViewModel.SourcePath,
                formViewModel.DestinationPath,
                formViewModel.Type
            );
            try
            {
                await _backupJobsStore.Update(backupJob);
                _modalNavigationStore.Close();
            }
            catch (Exception) { }
        }
    }
}
