using System;
using System.Collections.Generic;
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

            Console.WriteLine("\nAz adatbázisban tárolt mérési adatok:");
            szenzorhalo.Database.GetAllMeresiAdatok();

            Console.WriteLine("\nAz adatbázis statisztikája:");
            szenzorhalo.Database.GetAllTables();

            szenzorhalo.Database.Dispose();
        }
    }
}

