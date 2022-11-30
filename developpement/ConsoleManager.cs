using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Projet_Programmation_Système.developpement
{
    //Création d'une classe pour gérer la console. Cette classe est statique car elle ne contient que des méthodes statiques.
    //Creation of a class to manage the console. This class is static because it contains only static methods.
    public static class ConsoleManager
    {
        //Création d'une variable pour gérer la langue. Cette variable est statique car elle est commune à toutes les instances de la classe.
        //Creation of a variable to manage the language. This variable is static because it is common to all instances of the class.
        public static LanguageControl languageControl = new LanguageControl();
        //Création d'une variable pour afficher la langue. Cette variable est statique car elle est commune à toutes les instances de la classe.
        //Creation of a variable to display the language. This variable is static because it is common to all instances of the class.
        public static void DisplayLanguage(string message)
        {
            Console.WriteLine(languageControl.GetLanguageString(message));
        }

        //Création d'une méthode qui affiche une ligne vide. 
        //Creation of a méthod to display an empty line. 
        public static void DisplayEmptyLine()
        {
            Console.WriteLine();
        }

        //Création d'une méthode qui affiche un string sur l'invité de commande. 
        //Creation of a method to display a string on the command prompt. 
        public static void Display(string? message)
        {
            Console.WriteLine(message);
        }

        //Création d'une méthode qui affiche un tableau de string en rapport avec la langue. 
        //Create a method to display an array of strings related to the language. 
        public static void DisplayArrayLanguage(string[] array)
        {
            foreach (string s in array)
            {
                Console.WriteLine(languageControl.GetLanguageString(s));
            }
        }

        //Création d'une méthode qui affiche un tableau de string.
        //Creation of a method to display an array of strings. 
        public static void DisplayArray(string?[] array)
        {
            foreach (string? s in array)
            {
                Console.WriteLine(s);
            }
        }

        //Création d'une méthode qui affiche un tableau de string qui ne saute pas de ligne à la fin. 
        //Create a method to display an array of strings that doesn't jump a line at the end. 
        public static void DisplayArrayInLine(string?[] array)
        {
            foreach (string? s in array)
            {
                Console.Write(s);
            }
        }

        //Création d'une méthode qui demande à l'utilisateur d'entrer une valeur.
        //Creation of a method that asks the user to enter a value.
        public static string GetInput()
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                DisplayLanguage("InvalidInput");
                return GetInputNotEmpty();
            }
            return input;
        }

        //Création d'une méthode qui demande à l'utilisateur d'entrer une valeur qui ne soit pas vide.
        //Creation of a method that asks the user to enter a value that is not empty.
        public static string GetInputNotEmpty()
        {
            string? input = Console.ReadLine();
            if (input == null || input == "")
            {
                DisplayLanguage("InvalidInput");
                return GetInputNotEmpty();
            }
            return input;
        }

        //Création d'une méthode qui demande à l'utilisateur de confirmer son choix.
        //Creation of a method that asks the user to confirm his choice.
        public static bool AskForConfirmation()
        {
            string input = GetInputNotEmpty();
            if (input == "y" || input == "o") return true;
            if (input == "n") return false;
            DisplayLanguage("InvalidInput");
            return AskForConfirmation();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
