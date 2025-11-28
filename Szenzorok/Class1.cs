namespace Szenzorok
{
    public abstract class Sensor
    {
        public string Id { get; set; }                      // DB azonosító
        public string Name { get; set; }                    // Pl. Hőmérséklet
        public string Type { get; protected set; }          // Pl. Temperature
        public string Unit { get; protected set; }          // Pl. °C
        public double CurrentValue { get; protected set; }  // Mért (generált) adat
        public string Status { get; set; }                  // Típus alapján pl. - Alapjárat, terhelés, túlmelegedés
        public string CompositeID { get; private set; }     // Szenzor típusa alapján generált összetett id:


    }

    public interface IValueGenerate
    {
        double Generate();
    }

    public class TemperatureSensor
    { 
    
    }
}
