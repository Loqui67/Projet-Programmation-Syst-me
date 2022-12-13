using IHM_Client.Commands;
using IHM_Client.Models;
using IHM_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHM_Client.Command
{
    public class ConnectCommand : CommandBase
    {
        private MainViewModel _viewModel;

        public ConnectCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public override void Execute(object parameter)
        {
            try
            {
                ServerCommunication.Connect();
                _viewModel.IsConnected = ServerCommunication.isConnect;
            }
            catch (Exception) { }
        }
    }
}
