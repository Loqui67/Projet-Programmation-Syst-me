using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;

namespace AppWPF.developpement.Commands
{
    public class OpenSettingsCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ProcessusStore _processusStore;
        private readonly ExtensionCryptageStore _extensionCryptageStore;

        public OpenSettingsCommand(ModalNavigationStore modalNavigationStore, ProcessusStore processusStore, ExtensionCryptageStore extensionCryptageStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _processusStore = processusStore;
            _extensionCryptageStore = extensionCryptageStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.CurrentViewModel = SettingsViewModel.LoadViewModel(_modalNavigationStore, _processusStore, _extensionCryptageStore);
        }
    }
}
