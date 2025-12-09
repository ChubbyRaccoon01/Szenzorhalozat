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

        public void AddSensor(Sensor sensor)
        {
            var col = db.GetCollection<Sensor>("sensors");
            col.Insert(sensor);
        }

        public void GetAllSensors()
        {
            var col = db.GetCollection<Sensor>("sensors");
            var sensors = col.FindAll();
            foreach (var sensor in sensors)
            {
                Console.WriteLine(sensor);
            }
        }

        public void DeleteDB()
        {
            // Delete all collections
            db.DropCollection("sensors");
        }

        public void ClearAllData()
        {
            // Clear all collections in the database
            foreach (var collectionName in db.GetCollectionNames())
            {
                db.DropCollection(collectionName);
            }
        }

        public void DeleteAllSensors()
        {
            var col = db.GetCollection<Sensor>("sensors");
            col.DeleteAll();
        }

    }
}

