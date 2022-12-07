using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet de supprimer un processus
    ///Class which allows to delete a process
    public class DeleteProcessusCommand : AsyncCommandBase
    {
        ///Variable qui instancie plusieurs class comme par exemple ici processusListingItemViewModel
        ///Variable that instantiates several classes such as here processListingItemViewModel
        private ProcessusListingItemViewModel _processusListingItemViewModel;
        private ProcessusStore _processusStore;

        ///Méthode qui permet de supprimer un processus
        ///Method to kill a process
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
