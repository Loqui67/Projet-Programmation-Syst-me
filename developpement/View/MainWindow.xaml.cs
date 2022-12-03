using AppWPF.developpement;
using AppWPF.developpement.View;
using Projet_Programmation_Système.developpement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static LanguageControl languageControl = new();
        private List<BackupJob>? backupJobs = new List<BackupJob>();
        ModalWindowBackupJobInfos modalWindow = new ModalWindowBackupJobInfos();

        public MainWindow()
        {
            InitializeComponent();
            SwitchLanguage("en");
            RefreshList();
        }

        private void RefreshList()
        {
            {
                backupJobs = BackupJobsManager.GetBackupJobs();
                BackupJobList.ItemsSource = backupJobs;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Create_Backup_Job(object sender, RoutedEventArgs e)
        {
            modalWindow.ShowDialog();
        }

        private void Button_Switch_FR(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("fr");
            foreach(var a in Process.GetProcesses())
                if (a.ProcessName == "Calculator")
                {
                    MessageBox.Show("A business software " + a.ProcessName + " is running on your computer. Please close it before continuing.");
                }
        }

        private void Button_Switch_EN(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("en");
        }
        private void SwitchLanguage(string languageCode)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (languageCode)
            {
                case "en":
                    dictionary.Source = new Uri("../../Ressources/Language/DictionaryResources.en.xaml", UriKind.Relative);
                    break;
                case "fr":
                    dictionary.Source = new Uri("../../Ressources/Language/DictionaryResources.fr.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("../../Ressources/Language/DictionaryResources.en.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);
                
        }

        private void Button_Delete_All_Backup_Job(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Execute_All_Backup_Job(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Search_Backup_Job(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_Search_Backup_Job(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Delete_Backup_Job(object sender, EventArgs e)
        {
            MainMenuManager.Delete((sender as Button).DataContext.ToString());
            RefreshList();
        }
    }
}