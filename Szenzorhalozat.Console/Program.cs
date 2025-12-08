using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szenzorhalozat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Sensor> szenzorok = new List<Sensor>();
            szenzorok.Add(new TemperatureSensor());
            szenzorok.Add(new TemperatureSensor());

            foreach (var szenzor in szenzorok)
            {
                Console.WriteLine(szenzor.ToString());
            }
        }
    }
}
