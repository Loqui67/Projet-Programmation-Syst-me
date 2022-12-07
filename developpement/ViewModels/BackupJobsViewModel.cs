using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobsViewModel : ViewModelBase
    {
        public static Config config;
        public BackupJobsListingViewModel BackupJobsListingViewModel { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddBackupJobCommand { get; }

        public ICommand DeleteAllBackupJobsCommand { get; }
        public ICommand SaveAllBackupJobsCommand { get; }

        public ICommand LoadBackupJobsCommand { get; }

        public ICommand SwitchLanguageFrCommand { get; }
        public ICommand SwitchLanguageEnCommand { get; }

        public ICommand OpenSettingsCommand { get; }

        public BackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore, ProcessusStore processusStore)
        {
            BackupJobsListingViewModel = new BackupJobsListingViewModel(modalNavigationStore, backupJobsStore);
            LoadBackupJobsCommand = new LoadBackupJobsCommand(this, backupJobsStore);
            AddBackupJobCommand = new OpenAddBackupJobCommand(modalNavigationStore, backupJobsStore);
            DeleteAllBackupJobsCommand = new DeleteAllBackupJobsCommand(backupJobsStore);
            SaveAllBackupJobsCommand = new SaveAllBackupJobsCommand(backupJobsStore);
            SwitchLanguageFrCommand = new SwitchLanguageCommand("fr");
            SwitchLanguageEnCommand = new SwitchLanguageCommand("en");
            OpenSettingsCommand = new OpenSettingsCommand(modalNavigationStore, processusStore);
        }

        public static BackupJobsViewModel LoadViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore, ProcessusStore processusStore)
        {
            BackupJobsViewModel viewModel = new BackupJobsViewModel(modalNavigationStore, backupJobsStore, processusStore);
            config = FileManager.LoadConfig();
            viewModel.LoadBackupJobsCommand.Execute(null);
            if (config.DefaultLanguage == "fr") viewModel.SwitchLanguageFrCommand.Execute(null);
            else viewModel.SwitchLanguageEnCommand.Execute(null);
            BackupJob.LogExtension = config.LogExtension;
            return viewModel;
        }
    }
}
