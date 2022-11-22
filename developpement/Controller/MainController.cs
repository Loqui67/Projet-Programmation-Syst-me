using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programmation_Système.developpement.View;
using Programmation_Système.developpement.Model;
using System.Collections.Concurrent;
using System.Globalization;

namespace Programmation_Système.developpement.Controller
{
    public class MainController
    {
        public MainController()
        {
            new MainView();
            new MainModel();
        }
    }
}
