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
        public static LanguageControl languageControl;
        public static void DisplayLanguage(string message)
        {
            Console.WriteLine(languageControl.GetString(message));
        }

        public static void Display(string? message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayArrayLanguage(string[] array)
        {
            foreach (string s in array)
            {
                Console.WriteLine(languageControl.GetString(s));
            }
        }
        public static void DisplayArray(string?[] array)
        {
            foreach (string? s in array)
            {
                Console.WriteLine(s);
            }
        }

        public static string? GetInput()
        {
            return Console.ReadLine();
        }
    }
}
