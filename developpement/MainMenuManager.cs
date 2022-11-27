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
            ConsoleManager.DisplayArrayLanguage(new string[] { "MainMenu", "MainMenu1", "MainMenu2", "MainMenu3", "MainMenu4", "MainMenu5", "MainMenu6" });
            ConsoleManager.DisplayEmptyLine();
        }

        public void ChooseItem()
        {
            bool isValid = false;
            while (!isValid)
            {
                DisplayMainMenu();
                isValid = true;
                switch (ConsoleManager.GetInput())
                {
                    case "1":
                        BackupJobsManager.DisplayBackupJobs();
                        break;

                    case "2":
                        BackupJobsManager.DisplayBackupJobs();
                        BackupJobsManager.WriteBackupJob(BackupJobsManager.CreateBackupJob());
                        break;

                    case "3":
                        break;

                    case "4":
                        break;

                    case "5":
                        ConsoleManager.languageControl.ChangeLanguage();
                        break;
                        
                    case "6":
                        Main.Exit();
                        break;

                    default:
                        ConsoleManager.DisplayLanguage("InvalidInput");
                        isValid = false;
                        break;
                }
                ConsoleManager.DisplayEmptyLine();
            }
        }
    }
}
