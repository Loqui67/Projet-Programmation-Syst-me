using AppWPF.developpement.Commands;
using AppWPF.developpement.Stores;
using Projet_Programmation_Système.developpement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobsViewModel : ViewModelBase
    {
        public static readonly Config config = FileManager.LoadConfig();
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

        public ICommand SwitchLanguageFr { get; }
        public ICommand SwitchLanguageEn { get; }

        public BackupJobsViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            BackupJobsListingViewModel = new BackupJobsListingViewModel(modalNavigationStore, backupJobsStore);
            LoadBackupJobsCommand = new LoadBackupJobsCommand(this, backupJobsStore);
            AddBackupJobCommand = new OpenAddBackupJobCommand(modalNavigationStore, backupJobsStore);
            DeleteAllBackupJobsCommand = new DeleteAllBackupJobsCommand(backupJobsStore);
            SaveAllBackupJobsCommand = new SaveAllBackupJobsCommand(backupJobsStore);
            SwitchLanguageFr = new SwitchLanguageCommand("fr");
            SwitchLanguageEn = new SwitchLanguageCommand("en");
        }

        public static BackupJobsViewModel LoadViewModel(ModalNavigationStore modalNavigationStore, BackupJobsStore backupJobsStore)
        {
            BackupJobsViewModel viewModel = new BackupJobsViewModel(modalNavigationStore, backupJobsStore); 
            viewModel.LoadBackupJobsCommand.Execute(null);
            if (config.DefaultLanguage == "fr") viewModel.SwitchLanguageFr.Execute(null);
            else viewModel.SwitchLanguageEn.Execute(null);

            return viewModel;
        }
    }
}
