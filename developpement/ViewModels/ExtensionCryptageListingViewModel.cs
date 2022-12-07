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
    public class ExtensionCryptageListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ExtensionCryptageListingItemViewModel> _extensionCryptageListingItemViewModels;
        private readonly ExtensionCryptageStore _extensionCryptageStore;

        public IEnumerable<ExtensionCryptageListingItemViewModel> ExtensionCryptageListingItemViewModels
            => _extensionCryptageListingItemViewModels;

        public ExtensionCryptageListingViewModel(ExtensionCryptageStore extensionCryptageStore)
        {
            _extensionCryptageListingItemViewModels = new ObservableCollection<ExtensionCryptageListingItemViewModel>();
            _extensionCryptageStore = extensionCryptageStore;

            _extensionCryptageStore.ExtensionCryptageAdded += ExtensionCryptageStore_ExtensionCryptageAdded;
            _extensionCryptageStore.ExtensionCryptageDeleted += ExtensionCryptageStore_ExtensionCryptageDeleted;
            _extensionCryptageStore.ExtensionCryptageLoaded += ExtensionCryptageStore_ExtensionCryptageLoaded;
        }

        protected override void Dispose()
        {
            _extensionCryptageStore.ExtensionCryptageAdded -= ExtensionCryptageStore_ExtensionCryptageAdded;
            _extensionCryptageStore.ExtensionCryptageDeleted -= ExtensionCryptageStore_ExtensionCryptageDeleted;
            _extensionCryptageStore.ExtensionCryptageLoaded -= ExtensionCryptageStore_ExtensionCryptageLoaded;
            base.Dispose();
        }

        private void ExtensionCryptageStore_ExtensionCryptageLoaded()
        {
            _extensionCryptageListingItemViewModels.Clear();

            foreach (ExtensionCryptage extensionCryptage in _extensionCryptageStore.allExtensionCryptage)
            {
                AddExtensionCryptage(extensionCryptage);
            }
        }

        private void ExtensionCryptageStore_ExtensionCryptageDeleted(Guid id)
        {
            ExtensionCryptageListingItemViewModel itemViewModel = _extensionCryptageListingItemViewModels.FirstOrDefault(y => y.ExtensionCryptage.Id == id);

            if (itemViewModel != null)
            {
                _extensionCryptageListingItemViewModels.Remove(itemViewModel);
            }
        }

        private void ExtensionCryptageStore_ExtensionCryptageAdded(ExtensionCryptage extensionCryptage)
        {
            AddExtensionCryptage(extensionCryptage);
        }

        private void AddExtensionCryptage(ExtensionCryptage extensionCryptage)
        {
            ExtensionCryptageListingItemViewModel itemViewModel = new ExtensionCryptageListingItemViewModel(extensionCryptage, _extensionCryptageStore);
            _extensionCryptageListingItemViewModels.Add(itemViewModel);
        }
    }
}
