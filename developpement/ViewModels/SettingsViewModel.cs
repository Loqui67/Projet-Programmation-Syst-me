using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _logExtension = BackupJobsViewModel.config.LogExtension;

        public ProcessusListingViewModel ProcessusListingViewModel { get; }
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


        public ICommand SetLogExtensionCommand { get; }
        public ICommand AddProcessCommand { get; }
        public ICommand SubmitSettingsCommand { get; }
        public ICommand LoadProcessusCommand { get; }

        public SettingsViewModel(ModalNavigationStore modalNavigationStore, ProcessusStore processusStore)
        {
            ProcessusListingViewModel = new ProcessusListingViewModel(processusStore);
            AddProcessCommand = new AddProcessCommand(this, processusStore);
            SubmitSettingsCommand = new SubmitSettingsCommand(modalNavigationStore, this, processusStore);
            LoadProcessusCommand = new LoadProcessusCommand(this, processusStore);
        }

        public static SettingsViewModel LoadViewModel(ModalNavigationStore modalNavigationStore, ProcessusStore processusStore)
        {
            SettingsViewModel viewModel = new SettingsViewModel(modalNavigationStore, processusStore);
            viewModel.LoadProcessusCommand.Execute(null);
            return viewModel;
        }
    }
}
