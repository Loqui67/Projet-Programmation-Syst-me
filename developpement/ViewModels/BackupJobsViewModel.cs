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
    ///Création d'une classe héritant de ViewModelBase qui permet "d'envoyé" les données à la vue
    ///Creation of a class inheriting from ViewModelBase which allows to "send" the data to the view
    public class BackupJobsViewModel : ViewModelBase
    {
        ///Création de plusieurs variables pour la vue
        ///Creating multiple variables for the view
        public static Config config;
        public BackupJobsListingViewModel BackupJobsListingViewModel { get; }

        private Thread _thread;

        ///Variable permettant de savoir si le chargement est en cours
        ///Variable allowing to know if the loading is in progress
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
        ///Variables permettant de srocket le résultats des commandes qui disent si les travaux de sauvegardes sont :
        ///Variables allowing to srocket the results of the commands which say if the backup jobs are:
        ///Ajouter
        ///Add
        public ICommand AddBackupJobCommand { get; }

        ///Supprimé
        ///Deleted
        public ICommand DeleteAllBackupJobsCommand { get; }
        ///Sauvegardé
        ///Saved
        public ICommand SaveAllBackupJobsCommand { get; }
        ///Charger
        ///loaded
        public ICommand LoadBackupJobsCommand { get; }
        ///Variables permettant de savoir si on change de language
        ///Variable allowing to know if we change language
        public ICommand SwitchLanguageFrCommand { get; }
        public ICommand SwitchLanguageEnCommand { get; }
        ///Variable servant à ouvrir les paramètres
        ///Variable used to open parameters
        public ICommand OpenSettingsCommand { get; }

        ///Méthodes permettant de regarder si un processus est en cours
        ///Methods to see if a process is running
        private void CheckForProcessus()
        {
            while (true)
            {
                Trace.WriteLine("a");
                Thread.Sleep(1000);
                Process? processus = Process.GetProcesses().FirstOrDefault(p => config.AllProcessus.Select(x => x.Name).Contains(p.ProcessName), null);
                if (processus != null) { Trace.WriteLine(processus.ProcessName); BackupJobsListingViewModel.IsProcessusDetected = false; }
                else BackupJobsListingViewModel.IsProcessusDetected = true;
            }
        }

        ///Méthode permettant de changer de langue
        ///Method to change language
        public BackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore, ProcessusStore processusStore)
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
