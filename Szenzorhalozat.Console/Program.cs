using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sensors;

namespace Szenzorhalozat
{
    internal class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            List<Sensor> szenzorok = new List<Sensor>();
            szenzorok.Add(new TemperatureSensor());
            szenzorok.Add(new TemperatureSensor());
            
            foreach (var szenzor in szenzorok)
            {
                Console.WriteLine(szenzor);
            }
=======
            TemperatureSensor sensor = new TemperatureSensor();
            Console.WriteLine(sensor.ValueUpd());
>>>>>>> 3db0e45 (?)
        }
    }
}
