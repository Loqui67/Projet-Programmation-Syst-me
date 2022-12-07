using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;


namespace AppWPF.developpement.Commands
{
    ///Classe qui permet de créer une commande qui permet de créer un travail de sauvegarde
    ///Class that allows to create a command that allows to create a backup job
    public class AddBackupJobCommand : AsyncCommandBase
    {
        private readonly AddBackupJobViewModel _addBackupJobViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        ///Méthode qui permet de créer un travail de sauvegarde
        ///Method to create a backup job
        public AddBackupJobCommand(AddBackupJobViewModel addBackupJobViewModel, ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _addBackupJobViewModel = addBackupJobViewModel;
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;
        }

        ///Méthode qui permet d'éxécuter un travail de sauvegarde de maniere asymétrique
        ///Method to run a backup job asymmetrically
        public override async Task ExecuteAsync(object parameter)
        {
            BackupJobDetailsFormViewModel formViewModel = _addBackupJobViewModel.BackupJobDetailsFormViewModel;
            formViewModel.IsSubmitting = true;
            BackupJob backupJob = new BackupJob(
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
