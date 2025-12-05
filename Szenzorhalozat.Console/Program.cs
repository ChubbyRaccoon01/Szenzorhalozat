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
            List<Sensors()> szenzorok = new List<Sensors>();
            szenzorok.Add(new Szenzor("Szenzor1", 10.5, "Celsius"));
            szenzorok.Add(new Szenzor("Szenzor2", 22.3, "Celsius"));
            
            foreach (var szenzor in szenzorok)
            {
                Console.WriteLine(szenzor);
            }
        }
    }
}
