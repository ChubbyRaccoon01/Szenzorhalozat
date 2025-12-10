using LiteDB;
using System;

namespace Szenzorhalozat
{
    public class Database : System.IDisposable
    {
        private LiteDatabase db;
        private string currentTableName;

        public Database()
        {
            db = new LiteDatabase("Meres.db");
            // Hozz létre egy új táblát az aktuális idővel (csak alfanumerikus karakterek)
            currentTableName = "T" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public void Dispose()
        {
            db?.Dispose();
        }

        public void AddSensor(Sensor sensor)
        {
            var col = db.GetCollection<Sensor>(currentTableName);
            col.Insert(sensor);
            // Az ID után frissítjük a CompositeID-t
            sensor.UpdateCompositeID();
            col.Update(sensor);
        }

        public void GetAllSensors()
        {
            var col = db.GetCollection<Sensor>(currentTableName);
            var sensors = col.FindAll();
            foreach (var sensor in sensors)
            {
                Console.WriteLine(sensor);
            }
        }

        public void GetAllTables()
        {
            Console.WriteLine("Adatbázis táblái:");
            foreach (var tableName in db.GetCollectionNames())
            {
                var col = db.GetCollection(tableName);
                Console.WriteLine($"  {tableName} - {col.Count()} elem");
            }
        }

        public void DeleteDB()
        {
            // Az aktuális tábla törlése
            db.DropCollection(currentTableName);
        }

        public void ClearAllData()
        {
            // Összes táblát törlünk az adatbázisból
            foreach (var collectionName in db.GetCollectionNames())
            {
                db.DropCollection(collectionName);
            }
        }

        public void DeleteAllSensors()
        {
            var col = db.GetCollection<Sensor>(currentTableName);
            col.DeleteAll();
        }

        public void AddMeresiAdat(MeresiAdat adat)
        {
            var col = db.GetCollection<MeresiAdat>(currentTableName + "_Adatok");
            col.Insert(adat);
        }

        public void GetAllMeresiAdatok()
        {
            var col = db.GetCollection<MeresiAdat>(currentTableName + "_Adatok");
            var adatok = col.FindAll();
            foreach (var adat in adatok)
            {
                Console.WriteLine($"Szenzor ID: {adat.SzenzorId}, Meres ideje: {adat.MeresIdeje}, Homerseklet: {adat.Adat}");
            }
        }

    }
}

