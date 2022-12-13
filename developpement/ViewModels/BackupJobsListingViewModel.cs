using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppWPF.developpement.ViewModels
{
    ///Création d'une classe pour la gestion des travaux de sauvegarde héritant de la classe ViewModelBase
    ///Creating a class for managing backup jobs inheriting from the ViewModelBase class
    public class BackupJobsListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<BackupJobsListingItemViewModel> _backupJobsListingItemViewModels;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobsStore;

        ///Création de plusieurs variables/méthodes pour la gestion des travaux de sauvegarde qui seront "envoyé" à la vue
        ///Creation of several variables/méthodes for the management of backup jobs that will be "sent" to the view
        public IEnumerable<BackupJobsListingItemViewModel> BackupJobsListingItemViewModels
            => _backupJobsListingItemViewModels;

        ///Variable pour voir si le processus est détecté
        ///Variable to see if the process is detected
        public static bool _isProcessusNotDetected;
        public bool IsProcessusNotDetected
        {
            get => _isProcessusNotDetected;
            set
            {
                _isProcessusNotDetected = value;
                OnPropertyChanged(nameof(IsProcessusNotDetected));
            }
        }


        public BackupJobsListingViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            _backupJobsListingItemViewModels = new ObservableCollection<BackupJobsListingItemViewModel>();
            _modalNavigationStore = modalNavigationStore;
            _backupJobsStore = backupJobsStore;



            _backupJobsStore.BackupJobAdded += BackupJobsStore_BackupJobAdded;
            _backupJobsStore.BackupJobUpdated += BackupJobsStore_BackupJobUpdated;
            _backupJobsStore.BackupJobDeleted += BackupJobsStore_BackupJobDeleted;
            _backupJobsStore.AllBackupJobsDeleted += BackupJobsStore_AllBackupJobsDeleted;
            _backupJobsStore.BackupJobsLoaded += BackupJobsStore_BackupJobsLoaded;

        }



        protected override void Dispose()
        {
            _backupJobsStore.BackupJobAdded -= BackupJobsStore_BackupJobAdded;
            _backupJobsStore.BackupJobUpdated -= BackupJobsStore_BackupJobUpdated;
            _backupJobsStore.BackupJobDeleted -= BackupJobsStore_BackupJobDeleted;
            _backupJobsStore.AllBackupJobsDeleted -= BackupJobsStore_AllBackupJobsDeleted;
            _backupJobsStore.BackupJobsLoaded -= BackupJobsStore_BackupJobsLoaded;
            base.Dispose();
        }

        ///Méthode permettant de passer de l'état de stockage d'un travail de sauvegarde à l'état de mise à jour du travail de sauvegarde
        ///Method for passing from the storage state of a backup job to the update state of the backup job
        private void BackupJobsStore_BackupJobUpdated(BackupJob backupJob)
        {
            BackupJobsListingItemViewModel backupJobViewModel = _backupJobsListingItemViewModels.FirstOrDefault(vm => vm.BackupJob.Id == backupJob.Id);
            if (backupJobViewModel != null)
            {
                backupJobViewModel.Update(backupJob);
            }
        }

        ///Méthode permettant de passer de l'état de stockage d'un travail de sauvegarde à l'état de mise à jour du travail de chargement
        ///Method for passing from the storage state of a backup job to the update state of the loading job
        private void BackupJobsStore_BackupJobsLoaded()
        {
            _backupJobsListingItemViewModels.Clear();

            foreach (BackupJob backupJob in _backupJobsStore.backupJobs)
            {
                AddBackupJob(backupJob);
            }
        }

        ///Méthode permettant de passer de l'état de stockage d'un travail de sauvegarde à l'état de suppression du travail de sauvegarde
        ///Method for passing from the storage state of a backup job to the deletion state of the backup job
        private void BackupJobsStore_BackupJobDeleted(Guid id)
        {
            BackupJobsListingItemViewModel itemViewModel = _backupJobsListingItemViewModels.FirstOrDefault(y => y.BackupJob?.Id == id);

            if (itemViewModel != null)
            {
                _backupJobsListingItemViewModels.Remove(itemViewModel);
            }
        }

        ///Méthode permettant de passer de l'état de stockage d'un travail de sauvegarde à l'état de suppression de tout les travaux de sauvegarde
        ///Method for passing from the storage state of a backup job to the deletion state of all backup jobs
        private void BackupJobsStore_AllBackupJobsDeleted()
        {
            _backupJobsListingItemViewModels.Clear();
        }

        ///Méthode permettant de passer de l'état de stockage d'un travail de sauvegarde à l'état d'ajout de travail de sauvegarde
        ///Method for passing from the storage state of a backup job to the addition state of the backup job
        private void BackupJobsStore_BackupJobAdded(BackupJob backupJob)
        {
            AddBackupJob(backupJob);
        }

        ///Méthode qui permet d'ajouter un travail de sauvegarde
        ///Method that allows to add a backup job
        private void AddBackupJob(BackupJob backupJob)
        {
            BackupJobsListingItemViewModel itemViewModel = new BackupJobsListingItemViewModel(backupJob, _backupJobsStore, _modalNavigationStore);
            _backupJobsListingItemViewModels.Add(itemViewModel);
        }
    }
}
