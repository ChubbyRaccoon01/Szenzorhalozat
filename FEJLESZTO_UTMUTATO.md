# Szenzorhalózat - Fejlesztői Útmutató

## Projekt Áttekintése

A Szenzorhalózat projekt egy C# alapú szenzoradatgyűjtési rendszer, amely LiteDB-t használ az adatok persistent tárolásához. A projekt szenzorméréseket kezel és azokat időpecsételt adatbázis-gyűjteményekben tárárolja.

## Projekt Struktúra

```
Szenzorhalozat/
├── Szenzorhalozat.Core/          # Alaposztályok és üzleti logika
│   ├── Sensors.cs                # Szenzor alapklasszok és implementációk
│   ├── Szenzorhalozat.cs         # Szenzorrendszer vezérlő
│   ├── database.cs               # LiteDB adatbáziskezelés
│   ├── AdatgyujtoAllomas.cs      # Mérési adat gyűjtő
│   └── MeresiAdat.cs             # Mérési adat model
├── Szenzorhalozat.Console/       # Konzol alkalmazás
│   └── Program.cs                # Belépési pont
└── Szenzorhalozat.sln            # Solution fájl
```

## Főbb Komponensek

### 1. `Sensor` - Absztrakt Szenzor Osztály

**Fájl:** `Szenzorhalozat.Core/Sensors.cs`

```csharp
public abstract class Sensor
{
    [BsonId]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; protected set; }
    public string Unit { get; protected set; }
    public double CurrentValue { get; protected set; }
    public string Status { get; set; }
    public string CompositeID { get; set; }
    public double[] MinMax = new double[2];
    
    public event System.Action<MeresiAdat>? MeresiAdatKeszult;
    
    public void Meres()           // Mérés trigger
    public void ValueUpd()         // Érték frissítés
    public void UpdateCompositeID() // Composite ID generálás
    protected abstract string StatusUpdate();
}
```

**Kulcsfontosságú Tulajdonságok:**
- `[BsonId]`: LiteDB by automatikus ID-t rendel hozzá
- `MeresiAdatKeszult`: Event, amely mérési adatok rendelkezésre állása esetén aktiválódik
- `MinMax`: Min-max tartomány az értékek generálásához

### 2. Szenzor Implementációk

**Támogatott Szenzortípusok:**

| Típus | Osztály | Egység | Tartomány |
|-------|---------|--------|-----------|
| Hőmérséklet | `TemperatureSensor` | °C | 60-120 |
| Forgás | `RotationSensor` | RPM | 600-3600 |
| Vibráció | `VibrationSensor` | m/s² | 0.5-10 |
| CO₂ | `CO2Sensor` | PPM | 400-6000 |
| Nyomás | `PressureSensor` | bar | 0.5-3 |

**Új szenzortípus hozzáadása:**

```csharp
public class CustomSensor : Sensor
{
    public CustomSensor()
    {
        MinMax[0] = 0;      // Min érték
        MinMax[1] = 100;    // Max érték
        
        Name = "Custom Sensor";
        Type = "CUSTOM";
        Unit = "unit";
        CurrentValue = Generate(MinMax[0], MinMax[1]);
        Status = StatusUpdate();
        CompositeID = $"S-{Type.ToUpper()}-{Id:D3}";
    }

    protected override string StatusUpdate()
    {
        // Implementáld az állapot logikát
        if (CurrentValue < 50)
            return "Normal";
        else
            return "Alert";
    }
}
```

### 3. `Database` - Adatbáziskezelés

**Fájl:** `Szenzorhalozat.Core/database.cs`

```csharp
public class Database : IDisposable
{
    private LiteDatabase db;
    private string currentTableName;  // Időpecsétes gyűjtemény név
    
    public void AddSensor(Sensor sensor)        // Szenzor mentés
    public void GetAllSensors()                 // Összes szenzor lekérés
    public void AddMeresiAdat(MeresiAdat adat)  // Mérési adat mentés
    public void GetAllMeresiAdatok()            // Összes mérési adat lekérés
    public void GetAllTables()                  // Adatbázis statisztika
}
```

**Kollekciónevezési konvenció:**
- Szenzor adatok: `T20251210155924` (időpecsétel)
- Mérési adatok: `T20251210155924_Adatok` (időpecsétel + `_Adatok`)

### 4. `Szenzorhalozat` - Rendszer Vezérlő

**Fájl:** `Szenzorhalozat.Core/Szenzorhalozat.cs`

```csharp
public class Szenzorhalozat
{
    public List<Sensor> Szenzorok { get; set; }
    public AdatgyujtoAllomas AdatgyujtoAllomas { get; set; }
    public Database Database { get; set; }
    public event MeresTriggerDelegate? MeresTrigger;
    
    public void SzenzorHozzaadas(Sensor szenzor)  // Szenzor hozzáadás
    public void MeresInditas()                     // Mérések indítása (5x)
}
```

## Program.cs - Interaktív Alkalmazás

**Fájl:** `Szenzorhalozat.Console/Program.cs`

Az alkalmazás egy interaktív menürendszert tartalmaz LINQ-alapú szenzó szűréssel:

```csharp
// Hovedmenü lehetőségek
1. List Sensors       // Szenzorok listázása típus szerint
2. Export JSON        // Szenzorok JSON exportálása
3. List Database      // Adatbázis tartalmának megtekintése
4. Exit               // Kilépés

// Szenzó listázás LINQ-val
var sensors = system.Szenzorok ?? Enumerable.Empty<Sensor>();

// Összes szenzor
sensors.Select(s => s.ToString()).ToList()

// Szűrés típus szerint
sensors.OfType<TemperatureSensor>().Select(s => s.ToString()).ToList()
sensors.OfType<RotationSensor>().Select(s => s.ToString()).ToList()
sensors.OfType<VibrationSensor>().Select(s => s.ToString()).ToList()
sensors.OfType<CO2Sensor>().Select(s => s.ToString()).ToList()
sensors.OfType<PressureSensor>().Select(s => s.ToString()).ToList()
```

**Főbb LINQ Módszerek:**
- `OfType<T>()`: Típusspecifikus szűrés
- `Select()`: Szenzor string reprezentációra konvertálás
- `ToList()`: Eredmények gyűjteménnyé alakítása
- `?? Enumerable.Empty()`: Null-kezelés biztonságosan

### AdatgyujtoAllomas - Adat Gyűjtés

**Fájl:** `Szenzorhalozat.Core/AdatgyujtoAllomas.cs`

```csharp
public class AdatgyujtoAllomas
{
    public List<MeresiAdat> Adatok { get; set; }  // Gyűjtött adatok
    private Database? Database { get; set; }
    
    public void MeresiAdatFogadas(MeresiAdat adat)
    {
        Adatok.Add(adat);                          // Memóriabeli gyűjtés
        Database?.AddMeresiAdat(adat);             // Adatbázis tárolás
    }
}
```

## Fejlesztési Workflow

### 1. Projekt Felépítése

```bash
cd /home/dev/mnt/szofi/beadando/Szenzorhalozat
dotnet build
```

### 2. Függőségek

**LiteDB NuGet csomag:**
```xml
<PackageReference Include="LiteDB" Version="5.0.17" />
```

### 3. Hibakeresés és Futtatás

```bash
# Debug mód
dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj

# Release build
dotnet publish -c Release -o ./publish
./publish/Szenzorhalozat.Console
```

**Alkalmazás futása után:**
- 6 szenzor automatikus hozzáadása
- 5 mérési ciklus lebonyolítása
- Interaktív menü megjelenítése:
  ```
  === Main Menu ===
  1. List Sensors
  2. Export JSON
  3. List Database Content
  4. Exit
  ```

### 4. Adatbázis Vizsgálata

Az `Meres.db` fájl egy bináris LiteDB fájl. Lekérdezéshez:
- Használd a `Database` osztály metódusait (`GetAllMeresiAdatok()`)
- Vagy a LiteDB Studio-t bináris megjelenítéshez
- Vagy az alkalmazás menüjét ("List Database Content")

## Adatáramlás

```
SzenzorHozzaadas()
    ↓
Database.AddSensor()  [Szenzor tárolás]
    ↓
MeresTrigger event handler regisztrálás
    ↓
MeresInditas() → Meres() hívás [5x]
    ↓
Sensor.MeresiAdatKeszult event
    ↓
AdatgyujtoAllomas.MeresiAdatFogadas()
    ↓
Database.AddMeresiAdat()  [Mérési adatok tárolása]
```

## Tesztelés

### Unit Tesztek Futtatása

```bash
dotnet test
```

### Integrációs Teszt

```bash
# Adatbázis törlése és tiszta futtatás
rm -f Meres.db
dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj
```

## Közös Hibák és Megoldások

| Hiba | Ok | Megoldás |
|------|-----|----------|
| `'LiteDB' could not be found` | Hiányzó NuGet csomag | `dotnet restore` |
| `Cannot insert duplicate key in unique index '_id'` | ID ütközés | Statikus ID számlálóval volt probléma (másolódás) |
| `MinMax null reference` | Nincs inicializálva | `MinMax = new double[2]` az ősosztályban |
| `CurrentValue = 0` | MaxMin helyett MinMax-ot használták | Szenzor konstruktorban `MinMax[0]` és `MinMax[1]` beállítás |

## Bővítési Lehetőségek

1. **Adatbázis Migrációk:** Régebbi adatbázis formátumokhoz támogatás hozzáadása
2. **Valós Szenzor Integráció:** Fizikai szenzorművek összekötése
3. **REST API:** Web interfész a szenzoradatokhoz
4. **Grafikonok:** Mérési adatok vizualizációja
5. **Riasztások:** Küszöbérték túllépés esetén értesítések
6. **Több Felhasználó:** Összes szenzor és futtatás kezelése

## Kódolási Irányelvek

- **Naming:** Magyarul, PascalCase az osztályok és metódusok számára
- **Error Handling:** `using` utasítás az `IDisposable` objektumokhoz
- **Events:** Nullable event handlers (`event Type? EventName`)
- **Database:** Mindig `Dispose()` meghívása vagy `using` statement

## Verziózás

- **Cél Framework:** .NET 9.0
- **C# verzió:** Latest
- **LiteDB verzió:** 5.0.17

## Kapcsolattartás

Fejlesztési kérdésekhez konzultálj a projektvezetővel.
