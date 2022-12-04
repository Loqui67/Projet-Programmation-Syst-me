using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

        public App()
        {
            _modalNavigationStore = new ModalNavigationStore();
            _backupJobStore = new BackupJobsStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            BackupJobsViewModel backupJobsViewModel = BackupJobsViewModel.LoadViewModel(_modalNavigationStore, _backupJobStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, backupJobsViewModel)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
