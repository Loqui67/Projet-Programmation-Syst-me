using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.ViewModels
{
    public class ExtensionPriorityListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ExtensionPriorityListingItemViewModel> _extensionPriorityListingItemViewModels;
        private readonly ExtensionPriorityStore _extensionPriorityStore;

        public IEnumerable<ExtensionPriorityListingItemViewModel> ExtensionPriorityListingItemViewModels
            => _extensionPriorityListingItemViewModels;

        public ExtensionPriorityListingViewModel(ExtensionPriorityStore extensionPriorityStore)
        {
            _extensionPriorityListingItemViewModels = new ObservableCollection<ExtensionPriorityListingItemViewModel>();
            _extensionPriorityStore = extensionPriorityStore;

            _extensionPriorityStore.ExtensionPriorityAdded += ExtensionPriorityStore_ExtensionPriorityAdded;
            _extensionPriorityStore.ExtensionPriorityDeleted += ExtensionPriorityStore_ExtensionPriorityDeleted;
            _extensionPriorityStore.ExtensionPriorityLoaded += ExtensionPriorityStore_ExtensionPriorityLoaded;
        }

        protected override void Dispose()
        {
            _extensionPriorityStore.ExtensionPriorityAdded -= ExtensionPriorityStore_ExtensionPriorityAdded;
            _extensionPriorityStore.ExtensionPriorityDeleted -= ExtensionPriorityStore_ExtensionPriorityDeleted;
            _extensionPriorityStore.ExtensionPriorityLoaded -= ExtensionPriorityStore_ExtensionPriorityLoaded;
            base.Dispose();
        }

        private void ExtensionPriorityStore_ExtensionPriorityLoaded()
        {
            _extensionPriorityListingItemViewModels.Clear();

            foreach (ExtensionPriority extensionPriority in _extensionPriorityStore.allExtensionPriority)
            {
                AddExtensionPriority(extensionPriority);
            }
        }

        private void ExtensionPriorityStore_ExtensionPriorityDeleted(Guid id)
        {
            ExtensionPriorityListingItemViewModel itemViewModel = _extensionPriorityListingItemViewModels.FirstOrDefault(y => y.ExtensionPriority.Id == id);

            if (itemViewModel != null)
            {
                _extensionPriorityListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void ExtensionPriorityStore_ExtensionPriorityAdded(ExtensionPriority extensionPriority)
        {
            AddExtensionPriority(extensionPriority);
        }

        private void AddExtensionPriority(ExtensionPriority extensionPriority)
        {
            ExtensionPriorityListingItemViewModel itemViewModel = new ExtensionPriorityListingItemViewModel(extensionPriority, _extensionPriorityStore);
            _extensionPriorityListingItemViewModels.Add(itemViewModel);
        }
    }
}
