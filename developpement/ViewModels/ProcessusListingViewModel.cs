using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppWPF.developpement.ViewModels
{
    public class ProcessusListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ProcessusListingItemViewModel> _processusListingItemViewModels;
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



        private void ProcessusStore_ProcessusLoaded()
        {
            _processusListingItemViewModels.Clear();

            foreach (Processus processus in _processusStore.allProcessus)
            {
                AddProcessus(processus);
            }
        }

        private void ProcessusStore_ProcessusDeleted(Guid id)
        {
            ProcessusListingItemViewModel itemViewModel = _processusListingItemViewModels.FirstOrDefault(y => y.Processus.Id == id);

            if (itemViewModel != null)
            {
                _processusListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void ProcessusStore_ProcessusAdded(Processus processus)
        {
            AddProcessus(processus);
        }

        private void AddProcessus(Processus processus)
        {
            ProcessusListingItemViewModel itemViewModel = new ProcessusListingItemViewModel(processus, _processusStore);
            _processusListingItemViewModels.Add(itemViewModel);
        }
    }
}
