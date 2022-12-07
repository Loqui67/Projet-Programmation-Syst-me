using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class DeleteExtensionCryptageCommand : AsyncCommandBase
    {
        private readonly ExtensionCryptageListingItemViewModel _extensionCryptageListingItemViewModel;
        private readonly ExtensionCryptageStore _extensionCryptageStore;

        public DeleteExtensionCryptageCommand(ExtensionCryptageListingItemViewModel extensionCryptageListingItemViewModel, ExtensionCryptageStore extensionCryptageStore)
        {
            _extensionCryptageListingItemViewModel = extensionCryptageListingItemViewModel;
            _extensionCryptageStore = extensionCryptageStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _extensionCryptageListingItemViewModel.IsDeleting = true;
            try { await _extensionCryptageStore.Delete(_extensionCryptageListingItemViewModel.ExtensionCryptageId); }
            catch (Exception) { }
            finally { _extensionCryptageListingItemViewModel.IsDeleting = false; }
        }
    }
}
