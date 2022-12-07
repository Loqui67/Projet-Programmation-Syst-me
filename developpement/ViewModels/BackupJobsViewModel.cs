using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobsViewModel : ViewModelBase
    {
        public static Config config;
        public BackupJobsListingViewModel BackupJobsListingViewModel { get; }

        private Thread _thread;
        

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
       
        

        public ICommand AddBackupJobCommand { get; }

        public ICommand DeleteAllBackupJobsCommand { get; }
        public ICommand SaveAllBackupJobsCommand { get; }

        public ICommand LoadBackupJobsCommand { get; }

        public ICommand SwitchLanguageFrCommand { get; }
        public ICommand SwitchLanguageEnCommand { get; }

        public ICommand OpenSettingsCommand { get; }

        private void CheckForProcessus()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Process? processus = Process.GetProcesses().FirstOrDefault(p => config.AllProcessus.Select(x => x.Name).Contains(p.ProcessName), null);
                if (processus != null) { BackupJobsListingViewModel.IsProcessusDetected = false; }
                else BackupJobsListingViewModel.IsProcessusDetected = true;
            }
        }

        public BackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore, ProcessusStore processusStore, ExtensionCryptageStore extensionCryptageStore)
        {
            _thread = new Thread(CheckForProcessus);
            _thread.Start();
            BackupJobsListingViewModel = new BackupJobsListingViewModel(modalNavigationStore, backupJobsStore);
            LoadBackupJobsCommand = new LoadBackupJobsCommand(this, backupJobsStore);
            AddBackupJobCommand = new OpenAddBackupJobCommand(modalNavigationStore, backupJobsStore);
            DeleteAllBackupJobsCommand = new DeleteAllBackupJobsCommand(backupJobsStore);
            SaveAllBackupJobsCommand = new OpenSaveAllBackupJobsCommand(modalNavigationStore, backupJobsStore);
            SwitchLanguageFrCommand = new SwitchLanguageCommand("fr");
            SwitchLanguageEnCommand = new SwitchLanguageCommand("en");
            OpenSettingsCommand = new OpenSettingsCommand(modalNavigationStore, processusStore, extensionCryptageStore);
        }

        public static BackupJobsViewModel LoadViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore, ProcessusStore processusStore, ExtensionCryptageStore extensionCryptageStore)
        {
            BackupJobsViewModel viewModel = new BackupJobsViewModel(modalNavigationStore, backupJobsStore, processusStore, extensionCryptageStore);
            config = FileManager.LoadConfig();
            viewModel.LoadBackupJobsCommand.Execute(null);
            if (config.DefaultLanguage == "fr") viewModel.SwitchLanguageFrCommand.Execute(null);
            else viewModel.SwitchLanguageEnCommand.Execute(null);
            BackupJob.LogExtension = config.LogExtension;
            return viewModel;
        }
    }
}
