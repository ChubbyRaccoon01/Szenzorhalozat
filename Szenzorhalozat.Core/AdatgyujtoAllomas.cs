namespace Szenzorhalozat
{
    public class AdatgyujtoAllomas
    {
        public List<MeresiAdat> Adatok { get; set; }
        private Database? Database { get; set; }

        public AdatgyujtoAllomas(Database? database = null)
        {
            Adatok = new List<MeresiAdat>();
            Database = database;
        }

        public void MeresiAdatFogadas(MeresiAdat adat)
        {
            Adatok.Add(adat);
            System.Console.WriteLine($"Szenzor ID: {adat.SzenzorId}, Meres ideje: {adat.MeresIdeje}, Adat: {adat.Adat}");
            
            // Mentés az adatbázisba, ha Database elérhető
            Database?.AddMeresiAdat(adat);
        }

        public void ElsoKiertekeles()
        {
            System.Console.WriteLine("Elso kiertekeles kezdete...");
            var atlagAdat = Adatok.Average(a => a.Adat);
            System.Console.WriteLine($"Atlagos homerseklet: {atlagAdat}");
        }
    }
}
