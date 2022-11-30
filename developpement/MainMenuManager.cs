using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projet_Programmation_Système.developpement.ConsoleManager;

namespace Projet_Programmation_Système.developpement
{

    //Création d'une class qui permet de gérer le menu principal.
    //Creation of a class which allows to manage the main menu.
    public class MainMenuManager
    {
        //Création d'une méthode qui permet de gérer l'affichage de menu principal.
        //Creation of a method which allows to manage the display of the main menu.
        private void DisplayMainMenu()
        {
            DisplayArrayLanguage(new string[] { "MainMenu", "MainMenu1", "MainMenu2", "MainMenu3", "MainMenu4", "MainMenu5", "MainMenu6", "MainMenu7" });
            DisplayEmptyLine();
        }

        //Création d'une méthode qui permet de choisir l'item sur le menue principal.
        //Creation of a method which allows to choose the item on the main menu.
        public void ChooseItem()
        {
            bool isValid = false;
            while (!isValid)
            {
                DisplayMainMenu();
                isValid = true;
                switch (GetInput())
                {
                    //Si l'utilisateur choisi l'item 1, il va être redirigé vers le menu de gestion des sauvegardes.
                    //If the user chooses item 1, he will be redirected to the save management menu.
                    case "1":
                        Save(false);
                        break;

                    //Si l'utilisateur choisi l'item 2, il va être redirigé vers le menu de création des sauvegardes.
                    //If the user chooses item 2, he will be redirected to the save creation menu.
                    case "2":
                        Create();
                        break;

                    //Si l'utilisateur choisi l'item 3, il va être redirigé vers le menu de modification des sauvegardes.
                    //If the user chooses item 3, he will be redirected to the save modification menu.
                    case "3":
                        Modify();
                        break;

                    //Si l'utilisateur choisi l'item 4, il va être redirigé vers le menu de suppression des sauvegardes.
                    //If the user chooses item 4, he will be redirected to the backup deletion menu.
                    case "4":
                        Delete();
                        break;

                    case "5":
                        Save(true);
                        break;

                    //Si l'utilisateur choisi l'item 5, il va être redirigé vers le menu de gestion des languages.
                    //If the user chooses item 5, he will be redirected to the language management menu.
                    case "6":
                        languageControl.ChangeLanguage();
                        break;

                    //Si l'utilisateur choisi l'item 6, l'application se ferme en demandant de confirmer son choix.
                    //If the user chooses item 6, the application closes by asking to confirm his choice.
                    case "7":
                        DisplayLanguage("AskQuit");
                        if (AskForConfirmation()) Main.Exit();
                        break;

                    //Si l'utilisateur choisi un autre item, une erreur vas s'afficher puis revenir au menue principal.
                    //If the user chooses another item, an error will be displayed then return to the main menu.
                    default:
                        DisplayLanguage("InvalidInput");
                        isValid = false;
                        break;
                }
                DisplayEmptyLine();
            }
        }

        //Création d'une méthode qui permet de gérer l'affichage du menu de gestion des sauvegardes.
        //Creation of a method that manages the display of the backup management menu.
        private void Save(bool restore)
        {
            if (restore) DisplayLanguage("Restore");
            else DisplayLanguage("Save");
            if (AskForConfirmation()) BackupJobsManager.LaunchAllSave(restore);
            else
            {
                if (restore) DisplayLanguage("ChooseSlotToRestore");
                else DisplayLanguage("ChooseSlotToSave");

                BackupJobsManager.DisplayBackupJobs();
                BackupJobsManager.LaunchSave(restore);
            }
        }

        //Création d'une méthode qui permet de gérer l'affichage du menu de création des sauvegardes.
        //Creation of a method that manages the display of the save creation menu.
        private void Create()
        {
            DisplayLanguage("ChooseSlotToCreate");
            BackupJobsManager.DisplayBackupJobs();
            BackupJobsManager.WriteBackupJob(BackupJobsManager.CreateBackupJob());
        }

        //Création d'une méthode qui permet de gérer l'affichage du menu de modification des sauvegardes.
        //Creation of a method that manages the display of the save modification menu.
        private void Modify()
        {
            DisplayLanguage("ChooseSlotToModify");
            BackupJobsManager.DisplayBackupJobs();
            BackupJobsManager.WriteBackupJob(BackupJobsManager.ModifyBackupJob());
        }

        //Création d'une méthode qui permet de gérer l'affichage du menu de suppression des sauvegardes.
        //Creation of a method that manages the display of the backup deletion menu.
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
