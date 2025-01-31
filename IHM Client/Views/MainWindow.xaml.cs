﻿using IHM_Client.Command;
using IHM_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IHM_Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ServerCommunication.Send("disconnect");
            Thread.Sleep(200);
            try
            {
                ServerCommunication.Close();
            } catch (Exception) { }
            Thread.Sleep(200);
            Environment.Exit(0);
        }
    }
}
