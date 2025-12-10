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

            using (var db = szenzorhalo.Database)
            {
                bool exitRequested = false;

                while (!exitRequested)
                {
                    try
                    {
                        DisplayMainMenu();
                        int choice = GetUserChoice();
                        ProcessChoice(choice, szenzorhalo, db);
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
                    SensorListing(system); // Pass the instance here
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
        }

        static void SensorListing(Szenzorhalozat system)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===Sensor Listing===");
                Console.WriteLine("1. List All");
                Console.WriteLine("====================");
                Console.WriteLine("List by Type: ");
                Console.WriteLine("2. Temperature Sensors");
                Console.WriteLine("3. Rotation Sensors");
                Console.WriteLine("4. Vibration Sensors");
                Console.WriteLine("5. CO2 Sensors");
                Console.WriteLine("6. Pressure Sensors");
                Console.WriteLine("====================");
                Console.WriteLine("7. Return to Main Menu");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();

                try
                {
                    int choice = int.Parse(input);
                    Console.Clear();

                    // null-safe source
                    var sensors = system.Szenzorok ?? Enumerable.Empty<Sensor>();

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("All Sensors:");
                            var all = sensors.Select(s => s.ToString()).ToList();
                            Console.WriteLine(all.Count > 0 ? string.Join(Environment.NewLine, all) : "No sensors found.");
                            WaitForUser();
                            break;
                        case 2:
                            Console.WriteLine("Temperature Sensors:");
                            var temps = sensors.OfType<TemperatureSensor>().Select(s => s.ToString()).ToList();
                            Console.WriteLine(temps.Count > 0 ? string.Join(Environment.NewLine, temps) : "No temperature sensors found.");
                            WaitForUser();
                            break;
                        case 3:
                            Console.WriteLine("Rotation Sensors:");
                            var rotations = sensors.OfType<RotationSensor>().Select(s => s.ToString()).ToList();
                            Console.WriteLine(rotations.Count > 0 ? string.Join(Environment.NewLine, rotations) : "No rotation sensors found.");
                            WaitForUser();
                            break;
                        case 4:
                            Console.WriteLine("Vibration Sensors:");
                            var vibrations = sensors.OfType<VibrationSensor>().Select(s => s.ToString()).ToList();
                            Console.WriteLine(vibrations.Count > 0 ? string.Join(Environment.NewLine, vibrations) : "No vibration sensors found.");
                            WaitForUser();
                            break;
                        case 5:
                            Console.WriteLine("CO2 Sensors:");
                            var co2s = sensors.OfType<CO2Sensor>().Select(s => s.ToString()).ToList();
                            Console.WriteLine(co2s.Count > 0 ? string.Join(Environment.NewLine, co2s) : "No CO2 sensors found.");
                            WaitForUser();
                            break;
                        case 6:
                            Console.WriteLine("Pressure Sensors:");
                            var pressures = sensors.OfType<PressureSensor>().Select(s => s.ToString()).ToList();
                            Console.WriteLine(pressures.Count > 0 ? string.Join(Environment.NewLine, pressures) : "No pressure sensors found.");
                            WaitForUser();
                            break;
                        case 7:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Unknown option.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Please enter a valid number.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Error: The number is too large or too small.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
    } 
}

