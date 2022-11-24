using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Programmation_Système.developpement
{
    public class MainMenuManager
    {
        public MainMenuManager()
        {
            DisplayMainMenu();
        }

        private void DisplayMainMenu()
        {
            bool isValid = false;
            while (!isValid)
            {
                ConsoleManager.DisplayArrayLanguage(new string[] { "MainMenu", "MainMenu1", "MainMenu2", "MainMenu3", "MainMenu4" });
                switch (ConsoleManager.GetInput())
                {
                    case "1":
                        ConsoleManager.DisplayLanguage("MainMenu1");
                        isValid = true;
                        break;

                    case "2":
                        ConsoleManager.DisplayLanguage("MainMenu2");
                        isValid = true;
                        break;

                    case "3":
                        ConsoleManager.DisplayLanguage("MainMenu3");
                        isValid = true;
                        break;

                    case "4":
                        ConsoleManager.DisplayLanguage("MainMenu4");
                        isValid = true;
                        break;

                    default:
                        ConsoleManager.DisplayLanguage("InvalidInput");
                        break;
                }
            }
        }
    }
}
