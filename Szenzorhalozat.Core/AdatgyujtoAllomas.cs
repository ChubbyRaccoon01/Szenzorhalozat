namespace Szenzorhalozat
{
    public class AdatgyujtoAllomas
    {
        public List<MeresiAdat> Adatok { get; set; }

        public AdatgyujtoAllomas()
        {
            Adatok = new List<MeresiAdat>();
        }

        public void MeresiAdatFogadas(MeresiAdat adat)
        {
            Adatok.Add(adat);
            System.Console.WriteLine($"Szenzor ID: {adat.SzenzorId}, Meres ideje: {adat.MeresIdeje}, Adat: {adat.Adat}");
        }

        public void ElsoKiertekeles()
        {
            System.Console.WriteLine("Elso kiertekeles kezdete...");
            var atlagAdat = Adatok.Average(a => a.Adat);
            System.Console.WriteLine($"Atlagos homerseklet: {atlagAdat}");
        }
    }
}
