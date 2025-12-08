using System.Data;

namespace Szenzorhalozat
{
    public abstract class Sensor
    {
        private static int sensorCounter = 1;
        public int Id { get; set; }                      // DB azonosító
        public string Name { get; set; }                    // Pl. Hőmérséklet
        public string Type { get; protected set; }          // Pl. Temperature
        public string Unit { get; protected set; }          // Pl. °C
        public double CurrentValue { get; protected set; }  // Mért (generált) adat
        public string Status { get; set; }                  // Típus alapján pl. - Alapjárat, terhelés, túlmelegedés
        public string CompositeID { get; protected set; }   // Szenzor típusa alapján generált összetett id: S-TEMP-001
        
        public double[] MinMax = new double[2];             // Min és max értékek tárolására
        public Sensor()
        {
            Id = sensorCounter++;
            Name = "Generic Sensor";
            Type = "GENERIC";
            Unit = "N/A";
            CurrentValue = Generate(MinMax[0], MinMax[1]);
            Status = StatusUpdate();
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";

        }

        public void ValueUpd() { CurrentValue = Generate(MinMax[0], MinMax[1]); }

        public double Generate(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;

        }
        protected abstract string StatusUpdate();

        public override string ToString()
        {
            return $"Name: {Name}, Type: {Type}, Value: {CurrentValue} {Unit}";//, CompID: {CompositeID}";
        }

    }



    public class TemperatureSensor: Sensor
    {
        private double[] MaxMin = {60,120};  
        public TemperatureSensor()
        {
            Name = "Temperature Sensor";
            Type = "TEMP";
            Unit = "°C";
            CurrentValue = Generate(MaxMin[0], MaxMin[1]);
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
        }
        
        protected override string StatusUpdate()
        {
            if (CurrentValue < 90)
                return "Alapjárat";
            else if (CurrentValue >= 90 && CurrentValue < 110)
                return "Terhelés";
            else
                return "Túlmelegedés";
        }
 
    }


    public class RotationSensor : Sensor
    {
        private double[] MaxMin = { 600, 3600 };
        public RotationSensor()
        {
            Name = "Rotation Sensor";
            Type = "ROT";
            Unit = "RPM";
            CurrentValue = Generate(MaxMin[0], MaxMin[1]);
            Status = StatusUpdate();
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
        }

        protected override string StatusUpdate()
        {
            if (CurrentValue < 900)
                return "Alapjárat";
            else if (CurrentValue < 3000)
                return "Terhelés alatt";
            else
                return "Kritikus";
        }
    }

    public class VibrationSensor : Sensor
    {
        private double[] MaxMin = { 0.5, 10 };
        public VibrationSensor()
        {
            Name = "Vibration Sensor";
            Type = "VIB";
            Unit = "m/s²";
            CurrentValue = Generate(MaxMin[0], MaxMin[1]);
            Status = StatusUpdate();
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
        }

        protected override string StatusUpdate()
        {
            if (CurrentValue < 3)
                return "Normál";
            else if (CurrentValue< 6)
                return "Magas";
            else
                return "Kritikus";
        }
    }

    public class CO2Sensor : Sensor
    {
        private double[] MaxMin = { 400, 6000 };
        public CO2Sensor()
        {
            Name = "CO2 Sensor";
            Type = "CO2";
            Unit = "PPM";
            CurrentValue = Generate(MaxMin[0], MaxMin[1]);
            Status = StatusUpdate();
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
        }

        protected override string StatusUpdate()
        {
            if (CurrentValue < 5000)
                return "Normál";
            else
                return "Kritikus";
        }
    }

    public class PressureSensor : Sensor
    {
        private double[] MaxMin = { 0.5, 3 };
        public PressureSensor()
        {
            Name = "Pressure Sensor";
            Type = "PRES";
            Unit = "bar";
            CurrentValue = Generate(MaxMin[0], MaxMin[1]);
            Status = StatusUpdate();
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
        }

        protected override string StatusUpdate()
        {
            if (CurrentValue < 1.5)
                return "Normál";
            else if (CurrentValue < 3)
                return "Terhelés";
            else
                return "Kritikus";
        }
    }
}
