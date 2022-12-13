using EasySave.developpement.Models;
using EasySave.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EasySave.developpement.ViewModels
{
    ///Classe qui permet de gérer la liste des processus
    ///Class that manages the list of processes
    public class ProcessusListingViewModel : ViewModelBase
    {
        ///Variable qui permet de gérer la liste des processus
        ///Variable that manages the list of processes
        private readonly ObservableCollection<ProcessusListingItemViewModel> _processusListingItemViewModels;
        ///Variable qui permet de stoker la liste des processus
        ///Variable that stores the list of processes
        private readonly ProcessusStore _processusStore;
        
        public IEnumerable<ProcessusListingItemViewModel> ProcessusListingItemViewModels
        => _processusListingItemViewModels;
        public ProcessusListingViewModel(ProcessusStore processusStore)
        {
            _processusListingItemViewModels = new ObservableCollection<ProcessusListingItemViewModel>();
            _processusStore = processusStore;



            _processusStore.ProcessusAdded += ProcessusStore_ProcessusAdded;
            _processusStore.ProcessusDeleted += ProcessusStore_ProcessusDeleted;
            _processusStore.ProcessusLoaded += ProcessusStore_ProcessusLoaded;

        }

        protected override void Dispose()
        {
            _processusStore.ProcessusAdded -= ProcessusStore_ProcessusAdded;
            _processusStore.ProcessusDeleted -= ProcessusStore_ProcessusDeleted;
            _processusStore.ProcessusLoaded -= ProcessusStore_ProcessusLoaded;
            base.Dispose();
        }


        ///Méthode qui permet de passer des processus stocké aux processus chargé
        ///Method that allows switching from stored processes to loaded processes
        private void ProcessusStore_ProcessusLoaded()
        {
            _processusListingItemViewModels.Clear();

            foreach (Processus processus in _processusStore.allProcessus)
            {
                AddProcessus(processus);
            }
        }

        ///Méthode qui permet de passer des processus stocké aux processus supprimé
        ///Method that allows switching from stored processes to deleted processes
        private void ProcessusStore_ProcessusDeleted(Guid id)
        {
            ProcessusListingItemViewModel itemViewModel = _processusListingItemViewModels.FirstOrDefault(y => y.Processus.Id == id);

            if (itemViewModel != null)
            {
                _processusListingItemViewModels.Remove(itemViewModel);
            }
        }

        ///Méthode qui permet de passer des processus stocké aux processus ajouté
        ///Method that allows switching from stored processes to added processes
        private void ProcessusStore_ProcessusAdded(Processus processus)
        {
            AddProcessus(processus);
        }

        ///Méthode qui permet d'ajouter un procéssus à la liste
        ///Method that allows to add a process to the list
        private void AddProcessus(Processus processus)
        {
            ProcessusListingItemViewModel itemViewModel = new ProcessusListingItemViewModel(processus, _processusStore);
            _processusListingItemViewModels.Add(itemViewModel);
        }
    }
}
