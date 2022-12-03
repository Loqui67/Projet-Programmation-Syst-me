using AppWPF.developpement;
using AppWPF.developpement.Views;
using Projet_Programmation_Système.developpement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //public static LanguageControl languageControl = new();
        //private List<BackupJob>? BackupJobsList { get; set; }
        //public List<BackupJob> BackupJobs { get; set; }

        public MainWindow()
        {
            InitializeComponent();
             

        }

        private void RefreshList()
        {
            {
                //BackupJobsList = BackupJobsManager.GetBackupJobs();
                //BackupJobList.ItemsSource = BackupJobsList;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Create_Backup_Job(object sender, RoutedEventArgs e)
        {
            
        }



        private void Button_Switch_FR(object sender, RoutedEventArgs e)
        {
            // languageControl.ChangeLanguageInterface("fr");
        }

        private void Button_Switch_EN(object sender, RoutedEventArgs e)
        {
            //languageControl.ChangeLanguageInterface("en");
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

        private void Button_Delete_Backup_Job(object sender, EventArgs e)
        {
            MainMenuManager.Delete((sender as Button).DataContext.ToString());
            RefreshList();
        }
        
        private void Button_Modify_Backup_Job(object sender, RoutedEventArgs e)
        {
            //foreach (BackupJob backupJob in backupJobs)
            //{
            //    if (backupJob.Name == (sender as Button).DataContext.ToString())
            //    {
            //       ModalWindowBackupJobInfos modalWindow = new(backupJob);
            //        modalWindow.ShowDialog();
            //    }
            //}
        }
    }
}