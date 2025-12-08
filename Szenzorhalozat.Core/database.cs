using LiteDB;

namespace Szenzorhalozat
{
    public class Database : System.IDisposable
    {
        private LiteDatabase db;

        public Database()
        {
            db = new LiteDatabase("Meres.db");
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}

