using System;
using System.Collections.Generic;
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
        public static LanguageControl languageControl = new LanguageControl();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Create_Backup_Job(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Switch_FR(object sender, RoutedEventArgs e)
        {
            languageControl.ChangeLanguageInterface("fr");
        }

        private void Button_Switch_EN(object sender, RoutedEventArgs e)
        {
            languageControl.ChangeLanguageInterface("en");
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
    }
}
