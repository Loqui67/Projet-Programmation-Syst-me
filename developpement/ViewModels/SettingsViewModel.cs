using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    ///Classe utilisé pour les paramétres du ViewModel
    ///Class used for ViewModel parameters
    public class SettingsViewModel : ViewModelBase
    {
        ///Variable privée utilisé pour stocker les extensions de log
        ///Private variable used to store log extensions
        private string _logExtension = BackupJobsViewModel.config.LogExtension;

        ///Variable utilisé pour stocker la liste des processus
        ///Variable used to store the list of processes
        public ProcessusListingViewModel ProcessusListingViewModel { get; }

        public ExtensionCryptageListingViewModel ExtensionCryptageListingViewModel { get; }
        ///Variable publique utilisé pour stocker les extensions de log
        ///Public variable used to store log extensions


        public string LogExtension
        {
            get { return _logExtension; }
            set
            {
                _logExtension = value;
                OnPropertyChanged(nameof(LogExtension));
            }
        }

        ///Variable privé utilisé pour stocker les processus à ajouter
        ///Private variable used to store the processes to add
        private string _processToAdd = "";

        ///Variable publique utilisé pour stocker les processus à ajouter
        ///Public variable used to store the processes to add
        public string ProcessToAdd
        {
            get { return _processToAdd; }
            set
            {
                _processToAdd = value;
                OnPropertyChanged(nameof(ProcessToAdd));
            }
        }

        private string _extensionCryptageToAdd = "";

        public string ExtensionCryptageToAdd
        {
            get { return _extensionCryptageToAdd; }
            set
            {
                _extensionCryptageToAdd = value;
                OnPropertyChanged(nameof(ExtensionCryptageToAdd));
            }
        }


        public ICommand SetLogExtensionCommand { get; }
        public ICommand AddProcessCommand { get; }
        public ICommand AddExtensionCryptageCommand { get; }
        public ICommand SubmitSettingsCommand { get; }
        public ICommand LoadProcessusCommand { get; }
        public ICommand LoadExtensionCryptageCommand { get; }

        ///Méthode utilisé pour les paramétrés le ViewModel
        ///Method used to parameter the ViewModel
        public SettingsViewModel(ModalNavigationStore modalNavigationStore, ProcessusStore processusStore, ExtensionCryptageStore extensionCryptageStore)
        {
            ProcessusListingViewModel = new ProcessusListingViewModel(processusStore);
            ExtensionCryptageListingViewModel = new ExtensionCryptageListingViewModel(extensionCryptageStore);
            AddProcessCommand = new AddProcessCommand(this, processusStore);
            SubmitSettingsCommand = new SubmitSettingsCommand(modalNavigationStore, this, processusStore);
            LoadProcessusCommand = new LoadProcessusCommand(processusStore);
            LoadExtensionCryptageCommand = new LoadExtensionCryptageCommand(extensionCryptageStore);
            AddExtensionCryptageCommand = new AddExtensionCryptageCommand(this, extensionCryptageStore);
        }

        public static SettingsViewModel LoadViewModel(ModalNavigationStore modalNavigationStore, ProcessusStore processusStore, ExtensionCryptageStore extensionCryptageStore)
        {
            SettingsViewModel viewModel = new SettingsViewModel(modalNavigationStore, processusStore, extensionCryptageStore);
            viewModel.LoadProcessusCommand.Execute(null);
            viewModel.LoadExtensionCryptageCommand.Execute(null);
            return viewModel;
        }
    }
}
