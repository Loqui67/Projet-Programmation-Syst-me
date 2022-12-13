using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;
using EasySave.developpement.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace EasySave
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
        private readonly ExtensionCryptageStore _extensionCryptageStore;
        private readonly ExtensionPriorityStore _extensionPriorityStore;

        public App()
        {
            _modalNavigationStore = new ModalNavigationStore();
            _backupJobStore = new BackupJobsStore();
            _processusStore = new ProcessusStore();
            _extensionCryptageStore = new ExtensionCryptageStore();
            _extensionPriorityStore = new ExtensionPriorityStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            BackupJobsViewModel backupJobsViewModel = BackupJobsViewModel.LoadViewModel(_modalNavigationStore, _backupJobStore, _processusStore, _extensionCryptageStore, _extensionPriorityStore);
            MainWindow mainWindow = new()
            {
                DataContext = new MainViewModel(_modalNavigationStore, backupJobsViewModel)
            };
            
            Process proc = Process.GetCurrentProcess();
            int count = Process.GetProcesses().Where(p =>
                p.ProcessName == proc.ProcessName).Count();

            if (count > 1)
            {
                MessageBox.Show((string)Current.FindResource("AlreadyRunning"), (string)Current.FindResource("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            else
            {
                mainWindow.Show();
            }

            base.OnStartup(e);
        }
    }
}
