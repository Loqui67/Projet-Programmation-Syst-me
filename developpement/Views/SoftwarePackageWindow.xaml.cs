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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppWPF.developpement.View
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class SoftwarePackageWindow : Window
    {
        public SoftwarePackageWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Process[] processList = Process.GetProcesses();
            ListViewItem item = new ListViewItem();
            foreach (var process in processList)
            {
                listViewProcess.Items.Add(process);
            }
        }
    }
}
