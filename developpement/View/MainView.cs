using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Programmation_Système.developpement.View
{
    public class MainView
    {
        public MainView()
        {
            
        }

        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayArray(string[] array)
        {
            foreach (string s in array)
            {
                Console.WriteLine(s);
            }
        }

        public string? GetInput()
        {
            return Console.ReadLine();
        }
    }
}
