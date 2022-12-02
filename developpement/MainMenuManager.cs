using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_Programmation_Système.developpement.ConsoleManager;

namespace Projet_Programmation_Système.developpement
{

    //Création d'une class qui permet de gérer le menu principal.
    //Creation of a class which allows to manage the main menu.
    public static class MainMenuManager
    {
        public static string formatLogs = "json";


        //Création d'une méthode qui permet de gérer l'affichage du menu de gestion des sauvegardes.
        //Creation of a method that manages the display of the backup management menu.
        public static void Save(bool restore, string name)
        {
            if (restore) DisplayLanguage("Restore");
            else DisplayLanguage("Save");
            if (AskForConfirmation()) BackupJobsManager.LaunchAllSave(restore);
            else
            {
                if (restore) DisplayLanguage("ChooseSlotToRestore");
                else DisplayLanguage("ChooseSlotToSave");

                BackupJobsManager.LaunchSave(restore, name);
            }
        }

        //Création d'une méthode qui permet de gérer l'affichage du menu de création des sauvegardes.
        //Creation of a method that manages the display of the save creation menu.
        public static void Create()
        {
            DisplayLanguage("ChooseSlotToCreate");
            BackupJobsManager.CreateBackupJob();
        }

        //Création d'une méthode qui permet de gérer l'affichage du menu de modification des sauvegardes.
        //Creation of a method that manages the display of the save modification menu.
        public static void Modify(string name)
        {
            DisplayLanguage("ChooseSlotToModify");
            BackupJobsManager.ModifyBackupJob(name);
        }

        //Création d'une méthode qui permet de gérer l'affichage du menu de suppression des sauvegardes.
        //Creation of a method that manages the display of the backup deletion menu.
        public static void Delete(string name)
        {
            //DisplayLanguage("ChooseSlotToDelete");
            BackupJobsManager.DeleteBackupJob(name);
        }

        public static void ChooseJsonOrXml()
        {
            {
                DisplayLanguage("ChooseExtension");
                bool isValid = false;
                while (!isValid)
                {
                    isValid = true;
                    switch (GetInput())
                    {
                        case "1":
                            formatLogs = "json";
                            break;

                        case "2":
                            formatLogs = "xml";
                            break;

                        default:
                            DisplayLanguage("InvalidInput");
                            isValid = false;
                            break;
                    }
                    DisplayEmptyLine();
                }
            }
        }
    }
}
