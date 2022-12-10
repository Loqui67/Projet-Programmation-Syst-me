using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    ///Création d'une classe héritant de la classe ViewModelBase servant à la gestion des données de la vue
    ///Creation of a class inheriting from the ViewModelBase class used to manage view data
    public class BackupJobsListingItemViewModel : ViewModelBase
    {
        ///Variables servant à l'instanciation de plusieurs classes
        ///Variables used to instantiate several classes
        public BackupJob BackupJob { get; private set; }
        public Guid BackupJobId => BackupJob.Id;
        public string Name => BackupJob.Name;
        public string SourcePath => BackupJob.SourcePath;
        public string DestinationPath => BackupJob.DestinationPath;
        public string Type
        {
            get
            {
                if (BackupJob.Type == "0") { return "Full"; } else { return "Differential"; }
            }
        }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }

        ///Variable privée permettant de vérifier si le fichier est entrain d'être supprimé 
        ///Private variable to check if the file is being deleted
        private bool _isDeleting;
        ///Variable Publique permettant de vérifier si le fichier est entrain d'être supprimé
        ///Variable Public to check if the file is being deleted
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        ///Variable privée permettant de vérifier si le fichier est entrain d'être sauvegardé
        ///Private variable to check if the file is being saved
        private bool _isSaving;
        ///Variable publique permettant de vérifier si le fichier est entrain d'être sauvegardé
        ///Public variable to check if the file is being saved
        public bool IsSaving
        {
            get => _isSaving;
            set
            {
                _isSaving = value;
                OnPropertyChanged(nameof(IsSaving));
            }
        }

        public BackupJobsListingItemViewModel(BackupJob backupJob, BackupJobsStore backupJobsStore, ModalNavigationStore modalNavigationStore)
        {
            BackupJob = backupJob;
            EditCommand = new OpenEditBackupJobCommand(this, backupJobsStore, modalNavigationStore);
            DeleteCommand = new DeleteBackupJobCommand(this, backupJobsStore);
            SaveCommand = new OpenSaveBackupJobCommand(this, backupJobsStore, modalNavigationStore);
        }

        ///Méthode permettant de mettre à jour les données de la vue
        ///Method to update view data
        public void Update(BackupJob backupJob)
        {
            BackupJob = backupJob;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(SourcePath));
            OnPropertyChanged(nameof(DestinationPath));
            OnPropertyChanged(nameof(Type));
        }
    }
}
