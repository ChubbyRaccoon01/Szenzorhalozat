using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szenzorhalozat;

namespace Szenzorhalozat
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var szenzorhalo = new Szenzorhalozat();
            

            szenzorhalo.SzenzorHozzaadas(new TemperatureSensor());
            szenzorhalo.SzenzorHozzaadas(new TemperatureSensor());
            szenzorhalo.SzenzorHozzaadas(new RotationSensor());
            szenzorhalo.SzenzorHozzaadas(new VibrationSensor());
            szenzorhalo.SzenzorHozzaadas(new CO2Sensor());
            szenzorhalo.SzenzorHozzaadas(new PressureSensor());

            szenzorhalo.MeresInditas();

            using (var db = new Database())
            {
                bool exitRequested = false;

                while (!exitRequested)
                {
                    try
                    {
                        DisplayMainMenu();
                        int choice = GetUserChoice();
                        ProcessChoice(choice, system, db);
                        exitRequested = choice == 4;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Error: Please enter a valid number.");
                        WaitForUser();
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Error: The number is too large or too small.");
                        WaitForUser();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error: {ex.Message}");
                        WaitForUser();
                    }
                }
            }
        }

        private static void ProcessChoice(int choice, Szenzorhalozat system, Database db)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("List of Sensors:");
                    foreach (var s in system.Szenzorok)
                    {
                        Console.WriteLine(s);
                    }
                    break;
                case 2:
                    Console.WriteLine("Exporting JSON...");
                    string fileName = "sensors_export.json";
                    Console.WriteLine("Beginning serialization...");
                    string jsonString = JsonSerializer.Serialize(system.Szenzorok, new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine("Serialization Complete.");
                    System.IO.File.WriteAllText(fileName, jsonString);
                    Console.WriteLine($"Export complete. Filename:{fileName}");
                    break;
                case 3:
                    Console.WriteLine("DB contents:");
                    db.GetAllSensors();
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Unknown option.");
                    break;
            }
            WaitForUser();
        }

        private static void WaitForUser()
        {
            Console.WriteLine("Please press any key to continue...");
            Console.ReadKey(intercept: true);
        }

        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. List Sensors");
            Console.WriteLine("2. Export JSON");
            Console.WriteLine("3. List Database Content");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
        }

        static int GetUserChoice()
        { 
            string input = Console.ReadLine();
            return int.Parse(input);

            
            
            
            



            Console.WriteLine("\nAz adatbázisban tárolt mérési adatok:");
            szenzorhalo.Database.GetAllMeresiAdatok();

            Console.WriteLine("\nAz adatbázis statisztikája:");
            szenzorhalo.Database.GetAllTables();

            szenzorhalo.Database.Dispose();
        }
    }
}

