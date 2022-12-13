using IHM_Client.Commands;
using IHM_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHM_Client.Command
{
    public class ResumeCommand : CommandBase
    {

        public ResumeCommand()
        {
        }

        public override void Execute(object parameter)
        {
            try
            {
                ServerCommunication.Send("resume");
            }
            catch (Exception) { }
        }
    }
}
