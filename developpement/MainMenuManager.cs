using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_Programmation_Système.developpement.ConsoleManager;

namespace Projet_Programmation_Système.developpement
{
    public class MainMenuManager
    {
        private void DisplayMainMenu()
        {
            DisplayArrayLanguage(new string[] { "MainMenu", "MainMenu1", "MainMenu2", "MainMenu3", "MainMenu4", "MainMenu5", "MainMenu6" });
            DisplayEmptyLine();
        }

        public void ChooseItem()
        {
            bool isValid = false;
            while (!isValid)
            {
                DisplayMainMenu();
                isValid = true;
                switch (GetInput())
                {
                    case "1":
                        Save();
                        break;

                    case "2":
                        Create();
                        break;

                    case "3":
                        Modify();
                        break;

                    case "4":
                        Delete();
                        break;

                    case "5":
                        languageControl.ChangeLanguage();
                        break;
                        
                    case "6":
                        DisplayLanguage("AskQuit");
                        if (AskForConfirmation()) Main.Exit();
                        break;

                    default:
                        DisplayLanguage("InvalidInput");
                        isValid = false;
                        break;
                }
                DisplayEmptyLine();
            }
        }

        private void Save()
        {
            DisplayLanguage("Save");
            if (AskForConfirmation()) BackupJobsManager.LaunchAllSave();

            DisplayLanguage("ChooseSlotToSave");
            BackupJobsManager.DisplayBackupJobs();
            BackupJobsManager.LaunchSave();
        }

        private void Create()
        {
            DisplayLanguage("ChooseSlotToCreate");
            BackupJobsManager.DisplayBackupJobs();
            BackupJobsManager.WriteBackupJob(BackupJobsManager.CreateBackupJob());
        }

        private void Modify()
        {
            DisplayLanguage("ChooseSlotToModify");
            BackupJobsManager.DisplayBackupJobs();
            BackupJobsManager.WriteBackupJob(BackupJobsManager.ModifyBackupJob());
        }
        private void Delete()
        {
            DisplayLanguage("ChooseSlotToDelete");
            BackupJobsManager.DisplayBackupJobs();
            var a = BackupJobsManager.DeleteBackupJob();
            DisplayLanguage("AreYouSureDelete");
            if (AskForConfirmation()) BackupJobsManager.WriteBackupJob(a);
        }


    }
}
