using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Class qui permet de modifier le travail de sauvegarde
    ///Class that allows modifying the backup job
    public class EditBackupJobCommand : AsyncCommandBase
    {
        ///Variable qui d'instancier plusieurs classes comme par exemple ici EditBackupViewModel
        ///Variable which to instantiate several classes as for example here EditBackupViewModel
        private readonly EditBackupJobViewModel _editBackupJobViewModel;
        private readonly BackupJobsStore _backupJobsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        ///Méthode qui permet de modifier un travail de sauvegarde
        ///Method for modifying a backup job
        public EditBackupJobCommand(EditBackupJobViewModel editBackupJobViewModel, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            _editBackupJobViewModel = editBackupJobViewModel;
            _backupJobsStore = backupJobsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            BackupJobDetailsFormViewModel formViewModel = _editBackupJobViewModel.BackupJobDetailsFormViewModel;
            formViewModel.IsSubmitting = true;
            BackupJob backupJob = new BackupJob(
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
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
