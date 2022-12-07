﻿using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System.Windows;

namespace AppWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly BackupJobsStore _backupJobStore;
        private readonly ProcessusStore _processusStore;

        public App()
        {
            _modalNavigationStore = new ModalNavigationStore();
            _backupJobStore = new BackupJobsStore();
            _processusStore = new ProcessusStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            BackupJobsViewModel backupJobsViewModel = BackupJobsViewModel.LoadViewModel(_modalNavigationStore, _backupJobStore, _processusStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, backupJobsViewModel)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
