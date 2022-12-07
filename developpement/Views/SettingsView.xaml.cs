using System.Diagnostics;
using System.Windows.Controls;

namespace AppWPF.developpement.Views
{
    /// <summary>
    /// Logique d'interaction pour SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Process[] processList = Process.GetProcesses();
            ListViewItem item = new ListViewItem();
            foreach (var process in processList)
            {
                //listViewProcess.Items.Add(process);
            }
        }
    }
}
