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
                

                while (true)
                {
                    Console.WriteLine("\n1) List sensors  2) Export JSON  3) Show DB  4) Exit");
                    var key = Console.ReadKey(intercept: true).KeyChar;
                    Console.WriteLine();
                    if (key == '1')
                    {
                        foreach (var s in szenzorhalo.Szenzorok) Console.WriteLine(s);
                    }
                    else if (key == '3')
                    {
                        Console.WriteLine("DB contents:");
                        db.GetAllSensors(); 
                    }
                    else if (key == '4')
                    {
                        break;
                    }
                    else if (key == '2')
                    {
                        string fileName = "sensors_export.json";

                        Console.WriteLine("Beginning serialization...");
                        string jsonString = JsonSerializer.Serialize(szenzorhalo.Szenzorok, new JsonSerializerOptions { WriteIndented = true });
                        Console.WriteLine("Serialization Complete.");
                        using (var writer = new System.IO.StreamWriter(fileName))
                        {
                            writer.Write(jsonString);
                        }
                        Console.WriteLine($"Export complete. Filename:{fileName}");
                    }
                    else
                    {
                        Console.WriteLine("Unknown option.");
                    }
                }
            }

            
            
            
            



            Console.WriteLine("\nAz adatbázisban tárolt mérési adatok:");
            szenzorhalo.Database.GetAllMeresiAdatok();

            Console.WriteLine("\nAz adatbázis statisztikája:");
            szenzorhalo.Database.GetAllTables();

            szenzorhalo.Database.Dispose();
        }
    }
}

