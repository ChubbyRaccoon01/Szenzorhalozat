namespace Szenzorhalozat
{

    public class Szenzorhalozat
    {
        public delegate void MeresTriggerDelegate();
        public List<Sensor> Szenzorok { get; set; }
        public AdatgyujtoAllomas AdatgyujtoAllomas { get; set; }
        public Database Database { get; set; }
        public event MeresTriggerDelegate? MeresTrigger;

        public Szenzorhalozat()
        {
            Szenzorok = new List<Sensor>();
            Database = new Database();
            AdatgyujtoAllomas = new AdatgyujtoAllomas(Database);
        }

        public void MeresInditas()
        {
            for (int i = 0; i < 5; i++)
            {
                System.Console.WriteLine("Meres inditasa...");
                MeresTrigger?.Invoke();
                Thread.Sleep(1000);
            }
        }

        public void SzenzorHozzaadas(Sensor szenzor)
        {
            Szenzorok.Add(szenzor);
            // Az adatbázisba mentés és ID generálás
            Database.AddSensor(szenzor);
            // Majd az esemény-hozzárendelés
            szenzor.MeresiAdatKeszult += AdatgyujtoAllomas.MeresiAdatFogadas;
            MeresTrigger += szenzor.Meres;
        }
    }
}
