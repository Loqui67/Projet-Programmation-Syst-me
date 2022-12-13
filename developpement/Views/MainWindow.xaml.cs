using System;
using System.Windows;

namespace AppWPF.developpement.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static LanguageControl languageControl = new();
        //private List<BackupJob>? BackupJobsList { get; set; }
        //public List<BackupJob> BackupJobs { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show((string)Application.Current.FindResource("AskBeforeQuit"), (string)Application.Current.FindResource("Quit"), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
            }
            Environment.Exit(0);
        }
    }
}