using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class DeleteExtensionPriorityCommand : AsyncCommandBase
    {
        private readonly ExtensionPriorityListingItemViewModel _extensionPriorityListingItemViewModel;
        private readonly ExtensionPriorityStore _extensionPriorityStore;

        public DeleteExtensionPriorityCommand(ExtensionPriorityListingItemViewModel extensionPriorityListingItemViewModel, ExtensionPriorityStore extensionPriorityStore)
        {
            _extensionPriorityListingItemViewModel = extensionPriorityListingItemViewModel;
            _extensionPriorityStore = extensionPriorityStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _extensionPriorityListingItemViewModel.IsDeleting = true;
            try { await _extensionPriorityStore.Delete(_extensionPriorityListingItemViewModel.ExtensionPriorityId); }
            catch (Exception) { }
            finally { _extensionPriorityListingItemViewModel.IsDeleting = false; }
        }
    }
}
