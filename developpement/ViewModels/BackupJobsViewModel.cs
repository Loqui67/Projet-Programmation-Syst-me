using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    ///Création d'une classe héritant de ViewModelBase qui permet "d'envoyé" les données à la vue
    ///Creation of a class inheriting from ViewModelBase which allows to "send" the data to the view
    public class BackupJobsViewModel : ViewModelBase
    {
        public static Config config;
        public BackupJobsListingViewModel BackupJobsListingViewModel { get; }

        private Thread _threadProcess;
        private Thread _threadSocket;

        ///Variable permettant de savoir si le chargement est en cours
        ///Variable allowing to know if the loading is in progress
        private bool _isLoading;
        private readonly ModalNavigationStore _modalNavigationStore;

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

        ///Méthodes permettant de regarder si un processus est en cours
        ///Methods to see if a process is running
        private void CheckForProcessus()
        {
            bool boxAlreadyPrinted = false;
            while (true)
            {
                Thread.Sleep(1000);
                Process? processus = Process.GetProcesses().FirstOrDefault(p => config.AllProcessus.Select(x => x.Name).Contains(p.ProcessName), null);
                if (processus != null)
                {
                    if (!boxAlreadyPrinted)
                    {
                        MessageBox.Show((string)Application.Current.FindResource("BusinessSoftware"), (string)Application.Current.FindResource("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                        boxAlreadyPrinted = true;
                    }

                    BackupJobsListingViewModel.IsProcessusNotDetected = false;
                    BackupJobSaver.PauseSave();
                }
                else
                {
                    boxAlreadyPrinted = false;
                    BackupJobsListingViewModel.IsProcessusNotDetected = true;
                }
            }
        }

        private void SocketConnection()
        {
            PauseSaveCommand pauseSaveCommand = new();
            ResumeSaveCommand resumeSaveCommand = new();
            StopSaveCommand stopSaveCommand = new(_modalNavigationStore);
            Server.Start();
            Server.AcceptConnection();
            while (true)
            {
                string receive = Server.Receive();
                switch (receive)
                {
                    case "pause":
                        pauseSaveCommand.Execute(null);
                        break;
                            
                    case "resume":
                        resumeSaveCommand.Execute(null);
                        break;
                            
                    case "stop":
                        stopSaveCommand.Execute(null);
                        break;

                    default:
                        break;
                }
            }
        }

        public BackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore, ProcessusStore processusStore, ExtensionCryptageStore extensionCryptageStore, ExtensionPriorityStore extensionPriorityStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _threadProcess = new Thread(CheckForProcessus);
            _threadProcess.Start();
            _threadSocket = new Thread(SocketConnection);
            _threadSocket.Start();
            BackupJobsListingViewModel = new BackupJobsListingViewModel(modalNavigationStore, backupJobsStore);
            LoadBackupJobsCommand = new LoadBackupJobsCommand(this, backupJobsStore);
            AddBackupJobCommand = new OpenAddBackupJobCommand(modalNavigationStore, backupJobsStore);
            DeleteAllBackupJobsCommand = new DeleteAllBackupJobsCommand(backupJobsStore);
            SaveAllBackupJobsCommand = new OpenSaveAllBackupJobsCommand(modalNavigationStore, backupJobsStore);
            SwitchLanguageFrCommand = new SwitchLanguageCommand("fr");
            SwitchLanguageEnCommand = new SwitchLanguageCommand("en");
            OpenSettingsCommand = new OpenSettingsCommand(modalNavigationStore, processusStore, extensionCryptageStore, extensionPriorityStore);
            
        }

        public static BackupJobsViewModel LoadViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore, ProcessusStore processusStore, ExtensionCryptageStore extensionCryptageStore, ExtensionPriorityStore extensionPriorityStore)
        {
            BackupJobsViewModel viewModel = new BackupJobsViewModel(modalNavigationStore, backupJobsStore, processusStore, extensionCryptageStore, extensionPriorityStore);
            config = FileManager.LoadConfig();
            viewModel.LoadBackupJobsCommand.Execute(null);
            if (config.DefaultLanguage == "fr") viewModel.SwitchLanguageFrCommand.Execute(null);
            else viewModel.SwitchLanguageEnCommand.Execute(null);
            BackupJob.LogExtension = config.LogExtension;
            return viewModel;
        }
    }
}
