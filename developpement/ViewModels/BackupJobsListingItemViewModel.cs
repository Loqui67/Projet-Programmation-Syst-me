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
        public string Type => BackupJob.Type;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }

        ///Création de plusieurs variables/méthodes qui vont être ensuite "envoyé" vers la vue
        ///Creation of several variables/méthodes which will then be "sent" to the view
        private bool _isDeleting;
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private bool _isSaving;
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
