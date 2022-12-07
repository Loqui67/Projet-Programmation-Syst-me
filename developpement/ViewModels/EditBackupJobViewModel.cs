using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    ///Classe permetttant de gérer la vue de modification d'un travail de sauvegarde
    ///Class to manage the edit view of a backup job
    public class EditBackupJobViewModel : ViewModelBase
    {
        ///Variable permettant de stocker les id des travaux de sauvegarde
        ///Variable to store backup job ids
        public Guid BackupJobId { get; }

        ///Variables de types BackupJobDetailsFormViewModel permettant de stocker les informations du travail de sauvegarde
        ///Variables of BackupJobDetailsFormViewModel types to store backup job information
        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        ///Méthode permettant de stocker les informations sur un travail de sauvegarde
        ///Method to store information about a backup job
        public EditBackupJobViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            BackupJobId = backupJob.Id;

            ICommand submitCommand = new EditBackupJobCommand(this, backupJobsStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel(submitCommand, cancelCommand)
            {
                Name = backupJob.Name,
                SourcePath = backupJob.SourcePath,
                DestinationPath = backupJob.DestinationPath,
                Type = backupJob.Type,
            };
        }
    }
}
