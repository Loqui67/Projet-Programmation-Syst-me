using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class DeleteProcessusCommand : AsyncCommandBase
    {
        private ProcessusListingItemViewModel _processusListingItemViewModel;
        private ProcessusStore _processusStore;

        public DeleteProcessusCommand(ProcessusListingItemViewModel processusListingItemViewModel, ProcessusStore processusStore)
        {
            _processusListingItemViewModel = processusListingItemViewModel;
            _processusStore = processusStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _processusListingItemViewModel.IsDeleting = true;
            try
            {
                await _processusStore.Delete(_processusListingItemViewModel.ProcessusId);
            }
            catch (Exception) { }
            finally
            {
                _processusListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
