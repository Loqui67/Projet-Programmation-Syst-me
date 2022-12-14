using IHM_Client.Commands;
using IHM_Client.Models;
using IHM_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IHM_Client.Command
{
    public class DisconnectCommand : CommandBase
    {
        private MainViewModel _viewModel;

        public DisconnectCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public override void Execute(object parameter)
        {
            try
            {
                ServerCommunication.Send("disconnect");
                Thread.Sleep(200);
                ServerCommunication.Close();
                _viewModel.IsConnected = ServerCommunication.isConnect;
            }
            catch (Exception) { }
        }
    }
}
