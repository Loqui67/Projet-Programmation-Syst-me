using IHM_Client.Commands;
using IHM_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHM_Client.Command
{
    public class StopCommand : CommandBase
    {

        public StopCommand()
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                ServerCommunication.Send("stop");
            }
            catch (Exception) { }
        }
    }
}
