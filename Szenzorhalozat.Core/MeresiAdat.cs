namespace Szenzorhalozat
{
    public record MeresiAdat
    {
        public int SzenzorId { get; set; }
        public DateTime MeresIdeje { get; set; }
        public double Adat { get; set; }
    }
}
