using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Projet_Programmation_Système.developpement
{
    public static class ConsoleManager
    {
        public static LanguageControl languageControl = new LanguageControl();
        public static void DisplayLanguage(string message)
        {
            Console.WriteLine(languageControl.GetLanguageString(message));
        }

        public static void DisplayEmptyLine()
        {
            Console.WriteLine();
        }
        

        public static void Display(string? message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayArrayLanguage(string[] array)
        {
            foreach (string s in array)
            {
                Console.WriteLine(languageControl.GetLanguageString(s));
            }
        }
        public static void DisplayArray(string?[] array)
        {
            foreach (string? s in array)
            {
                Console.WriteLine(s);
            }
        }

        public static void DisplayArrayInLine(string?[] array)
        {
            foreach (string? s in array)
            {
                Console.Write(s);
            }
        }

        public static string? GetInput()
        {
            return Console.ReadLine();
        }

        public static string GetInputNotNull()
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                DisplayLanguage("InvalidInput");
                return GetInputNotNull();
            }
            return input;
        }

        public static bool AskForConfirmation()
        {
            string? input = GetInput();
            if (input == null)
            {
                DisplayLanguage("InvalidInput");
                return AskForConfirmation();
            }
            if (input == "y" || input == "o") return true;
            if (input == "n") return false;
            DisplayLanguage("InvalidInput");
            return AskForConfirmation();
        }
    }
}
