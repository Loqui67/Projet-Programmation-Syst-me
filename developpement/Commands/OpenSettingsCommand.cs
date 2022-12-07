using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet d'ouvrir les parametres 
    ///Class that opens the parameters
    public class OpenSettingsCommand : CommandBase
    {
        ///Variables qui permettent d'instancier plusieurs classes
        ///Variables that allow to instantiate several classes
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ProcessusStore _processusStore;
        private readonly ExtensionCryptageStore _extensionCryptageStore;

        ///méthode qui permet d'ouvrir les parametres
        ///method to open settings
        public OpenSettingsCommand(ModalNavigationStore modalNavigationStore, ProcessusStore processusStore)
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
