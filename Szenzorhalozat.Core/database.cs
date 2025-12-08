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
            // Hozz létre egy új táblát az aktuális idővel
            currentTableName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
        }

        public void Dispose()
        {
            db?.Dispose();
        }

        public void AddSensor(Sensor sensor)
        {
            var col = db.GetCollection<Sensor>(currentTableName);
            col.Insert(sensor);
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

    }
}

