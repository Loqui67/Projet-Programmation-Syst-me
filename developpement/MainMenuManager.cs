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

        }

        private void DisplayMainMenu()
        {
            ConsoleManager.DisplayArrayLanguage(new string[] { "MainMenu", "MainMenu1", "MainMenu2", "MainMenu3", "MainMenu4" });
            ConsoleManager.DisplayEmptyLine();
        }

        public void ChooseItem()
        {
            bool isValid = false;
            while (!isValid)
            {
                DisplayMainMenu();
                switch (ConsoleManager.GetInput())
                {
                    case "1":
                        ConsoleManager.DisplayEmptyLine();
                        isValid = true;
                        break;

                    case "2":
                        ConsoleManager.DisplayEmptyLine();
                        ConsoleManager.languageControl.ChangeLanguage();
                        isValid = true;
                        break;

                    case "3":
                        ConsoleManager.DisplayEmptyLine();
                        isValid = true;
                        break;

                    case "4":
                        ConsoleManager.DisplayEmptyLine();
                        isValid = true;
                        break;

                    default:
                        ConsoleManager.DisplayEmptyLine();
                        ConsoleManager.DisplayLanguage("InvalidInput");
                        break;
                }
            }
        }
    }
}
