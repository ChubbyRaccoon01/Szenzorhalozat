using System.Data;
using LiteDB;

namespace Szenzorhalozat
{

    public abstract class Sensor
    {
        [BsonId]
        public int Id { get; set; }                      // DB azonosító (LiteDB auto-increment)
        public string Name { get; set; }                    // Pl. Hőmérséklet
        public string Type { get; protected set; }          // Pl. Temperature
        public string Unit { get; protected set; }          // Pl. °C
        public double CurrentValue { get; protected set; }  // Mért (generált) adat
        public string Status { get; set; }                  // Típus alapján pl. - Alapjárat, terhelés, túlmelegedés
        public string CompositeID { get; set; }   // Szenzor típusa alapján generált összetett id: S-TEMP-001
        
        public double[] MinMax = new double[2];             // Min és max értékek tárolására

        // Event fired when a measurement is ready
        public event System.Action<MeresiAdat>? MeresiAdatKeszult;

        public Sensor()
        {
            Name = "Generic Sensor";
            Type = "GENERIC";
            Unit = "N/A";
            CurrentValue = Generate(MinMax[0], MinMax[1]);
            Status = StatusUpdate();
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";

        }

        public void ValueUpd() { CurrentValue = Generate(MinMax[0], MinMax[1]); }

        public void UpdateCompositeID()
        {
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
        }

        // Trigger a measurement: update value, status and raise event with MeresiAdat
        public void Meres()
        {
            ValueUpd();
            Status = StatusUpdate();

            var adat = new MeresiAdat
            {
                SzenzorId = this.Id,
                MeresIdeje = DateTime.Now,
                Adat = this.CurrentValue
            };

            MeresiAdatKeszult?.Invoke(adat);
        }

        public double Generate(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;

        }

        protected abstract string StatusUpdate();

        public override string ToString()
        {
            return $"Name: {Name}, Type: {Type}, Value: {CurrentValue} {Unit}, CompID: {CompositeID}";
        }

    }



    public class TemperatureSensor : Sensor
    {
        public TemperatureSensor()
        {
            MinMax[0] = 60;
            MinMax[1] = 120;
            
            Name = "Temperature Sensor";
            Type = "TEMP";
            Unit = "°C";
            CurrentValue = Generate(MinMax[0], MinMax[1]);
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
        public RotationSensor()
        {
            MinMax[0] = 600;
            MinMax[1] = 3600;
            
            Name = "Rotation Sensor";
            Type = "ROT";
            Unit = "RPM";
            CurrentValue = Generate(MinMax[0], MinMax[1]);
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
        public VibrationSensor()
        {
            MinMax[0] = 0.5;
            MinMax[1] = 10;
            
            Name = "Vibration Sensor";
            Type = "VIB";
            Unit = "m/s²";
            CurrentValue = Generate(MinMax[0], MinMax[1]);
            Status = StatusUpdate();
            CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
        }

        protected override string StatusUpdate()
        {
            if (CurrentValue < 3)
                return "Normál";
            else if (CurrentValue < 6)
                return "Magas";
            else
                return "Kritikus";
        }
    }

    public class CO2Sensor : Sensor
    {
        public CO2Sensor()
        {
            MinMax[0] = 400;
            MinMax[1] = 6000;
            
            Name = "CO2 Sensor";
            Type = "CO2";
            Unit = "PPM";
            CurrentValue = Generate(MinMax[0], MinMax[1]);
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
        public PressureSensor()
        {
            MinMax[0] = 0.5;
            MinMax[1] = 3;
            
            Name = "Pressure Sensor";
            Type = "PRES";
            Unit = "bar";
            CurrentValue = Generate(MinMax[0], MinMax[1]);
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
