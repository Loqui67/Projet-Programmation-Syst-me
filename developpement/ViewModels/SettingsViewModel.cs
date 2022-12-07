using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _logExtension = BackupJobsViewModel.config.LogExtension;

        public ProcessusListingViewModel ProcessusListingViewModel { get; }
        public ExtensionCryptageListingViewModel ExtensionCryptageListingViewModel { get; }

        public string LogExtension
        {
            get { return _logExtension; }
            set
            {
                _logExtension = value;
                OnPropertyChanged(nameof(LogExtension));
            }
        }

        private string _processToAdd = "";

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
