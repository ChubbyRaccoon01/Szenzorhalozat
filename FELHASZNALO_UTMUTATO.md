# Szenzorhalózat - Felhasználói Útmutató

## Bevezetés

A Szenzorhalózat egy szenzoradatgyűjtési alkalmazás, amely különböző típusú szenzorokat kezel és azok mérési adatait egy adatbázisban tároja. Az alkalmazás automatikusan gyűjti és tároja a mérési adatokat.

## Telepítés és Futtatás

### Előfeltételek

- **Operációs rendszer:** Windows, macOS vagy Linux
- **.NET Runtime:** .NET 9.0 vagy újabb
- **Szabad hely:** Legalább 100 MB

### Telepítés Lépésein

1. **Projekt Letöltése**
   ```bash
   cd /home/dev/mnt/szofi/beadando/Szenzorhalozat
   ```

2. **Függőségek Telepítése**
   ```bash
   dotnet restore
   ```

3. **Fordítás**
   ```bash
   dotnet build
   ```

4. **Futtatás**
   ```bash
   dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj
   ```

### Gyors Indítás

```bash
cd /home/dev/mnt/szofi/beadando/Szenzorhalozat
rm -f Meres.db  # Opcionális: előző adatok törlése
dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj
```

## Használat

### A Program Futtatása

Az alkalmazás indításakor az alábbi lépések történnek:

1. **Szenzor Inicializálása**
   ```
   Name: Temperature Sensor, Type: TEMP, Value: 116.13 °C, CompID: S-TEMP-001
   Name: Temperature Sensor, Type: TEMP, Value: 112.33 °C, CompID: S-TEMP-002
   ```

2. **Mérések Indítása**
   - Az alkalmazás 5 alkalommal indít méréseket
   - Minden mérés között 1 másodperc szünet

3. **Mérési Adatok Megjelenítése**
   ```
   Szenzor ID: 1, Meres ideje: 12/10/2025 15:59:25, Homerseklet: 93.01
   Szenzor ID: 2, Meres ideje: 12/10/2025 15:59:25, Homerseklet: 61.23
   ```

4. **Adatbázis Statisztikája**
   ```
   Adatbázis táblái:
     T20251210155924 - 2 elem
     T20251210155924_Adatok - 10 elem
   ```

## Szenzor Típusok

Az alkalmazás az alábbi szenzortípusokat támogatja:

### 1. Hőmérséklet Szenzor (TemperatureSensor)

- **Típusazonosító:** `TEMP`
- **Mértékegység:** °C (Celsius-fok)
- **Mérési Tartomány:** 60 - 120 °C
- **Állapotok:**
  - Alapjárat: < 90 °C
  - Terhelés: 90 - 110 °C
  - Túlmelegedés: > 110 °C

### 2. Forgászszenzor (RotationSensor)

- **Típusazonosító:** `ROT`
- **Mértékegység:** RPM (fordulat/perc)
- **Mérési Tartomány:** 600 - 3600 RPM
- **Állapotok:**
  - Alapjárat: < 900 RPM
  - Terhelés alatt: 900 - 3000 RPM
  - Kritikus: > 3000 RPM

### 3. Vibráció Szenzor (VibrationSensor)

- **Típusazonosító:** `VIB`
- **Mértékegység:** m/s² (méter/másodperc²)
- **Mérési Tartomány:** 0.5 - 10 m/s²
- **Állapotok:**
  - Normál: < 3 m/s²
  - Magas: 3 - 6 m/s²
  - Kritikus: > 6 m/s²

### 4. CO₂ Szenzor (CO2Sensor)

- **Típusazonosító:** `CO2`
- **Mértékegység:** PPM (részecske millióban)
- **Mérési Tartomány:** 400 - 6000 PPM
- **Állapotok:**
  - Normál: < 5000 PPM
  - Kritikus: > 5000 PPM

### 5. Nyomás Szenzor (PressureSensor)

- **Típusazonosító:** `PRES`
- **Mértékegység:** bar
- **Mérési Tartomány:** 0.5 - 3 bar
- **Állapotok:**
  - Normál: < 1.5 bar
  - Terhelés: 1.5 - 3 bar
  - Kritikus: > 3 bar

## Adatbázis

### Adatfájl

- **Helye:** `Meres.db` (a projekt gyökerében)
- **Formátum:** LiteDB (bináris adatbázis)
- **Méret:** A mérési adatok számától függően növekszik

### Adatbázis Szerkezete

Az alkalmazás két típusú gyűjteményt használ:

1. **Szenzor Gyűjtemény** (pl. `T20251210155924`)
   - Szenzor objektumok
   - Elemek: 2 (ebben a futtatásban)

2. **Mérési Adatok Gyűjtemény** (pl. `T20251210155924_Adatok`)
   - Mérési eredmények
   - Elemek: 10 (5 mérés × 2 szenzor)

### Adatok Tartalmazza

Minden mérési adat az alábbi információkat tartalmazza:

```
Szenzor ID:     1
Meres ideje:    12/10/2025 15:59:25
Homerseklet:    93.00813511586887  (tárolt érték)
```

## Közös Forgatókönyvek

### Forgatókönyv 1: Egy Futtatás Adatainak Megtekintése

1. Futtasd az alkalmazást: `dotnet run ...`
2. Az alkalmazás automatikusan megjeleníti:
   - Szenzor adatokat
   - Mérési eredményeket
   - Adatbázis statisztikáját

### Forgatókönyv 2: Újabb Futtatás (új adatok)

```bash
dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj
```

Az új futtatás:
- Egy új időpecsétel-alapú gyűjteményt hoz létre
- Az előző futtatások adatai megmaradnak az adatbázisban
- Összes futtatás adatai elérhetőek

### Forgatókönyv 3: Adatbázis Törlése (tiszta indítás)

```bash
rm -f Meres.db
dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj
```

## Hibakezelés

### Hiba: "Cannot run..."

**Ok:** A projekt nem fordul le.

**Megoldás:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### Hiba: "Meres.db már létezik"

**Ok:** Az előző futtatás adatai még az adatbázisban vannak.

**Megoldás:** Az új adatok automatikusan hozzáadódnak az meglévő adatokhoz. Ez az elvárt viselkedés!

### Hiba: Nincs kimenete az alkalmazásnak

**Ok:** Az alkalmazás háttérben fut vagy nem indul el.

**Megoldás:**
```bash
# Teljes újrafordítás
dotnet clean
dotnet build
dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj
```

## Kimenete Értelmezése

### Szenzor Információ

```
Name: Temperature Sensor, Type: TEMP, Value: 116.13 °C, CompID: S-TEMP-001
```

- `Name`: Szenzor neve
- `Type`: Szenzor típusa
- `Value`: Aktuális érték
- `CompID`: Szenzor azonosító (S-[TYP]-[ID])

### Mérési Kimenet

```
Meres inditasa...
Szenzor ID: 1, Meres ideje: 12/10/2025 15:59:25, Homerseklet: 93.01
Szenzor ID: 2, Meres ideje: 12/10/2025 15:59:25, Homerseklet: 61.23
```

- `Meres inditasa...`: Mérési ciklus kezdete
- `Szenzor ID`: A szenzor azonosítója az adatbázisban
- `Meres ideje`: A mérés időpontja (dátum és idő)
- `Homerseklet`: A mért érték (általánosan minden típusú szenzorra)

### Adatbázis Statisztika

```
Adatbázis táblái:
  T20251210155924 - 2 elem
  T20251210155924_Adatok - 10 elem
```

- `T20251210155924`: Az aktuális futtatás szenzor gyűjteménye (2 szenzor)
- `T20251210155924_Adatok`: Az aktuális futtatás mérési adatok gyűjteménye (10 mérés)

## Kérdések és Válaszok

### K: Mit történik az előző futtatások adataival?

V: Az adatbázisban maradnak. Új futtatások minden alkalommal egy új gyűjteményt hoznak létre az aktuális időzónának megfelelő névvel.

### K: Miért vannak 10 mérési adat, ha 5 mérés van?

V: Mert 2 szenzor van, és mind a kettő mér minden ciklus alatt: 5 mérés × 2 szenzor = 10 adat.

### K: Lehet-e összes szenzort egyidőben futtatni?

V: Igen! A program adott időpontban mind a szenzorokat mér és azok értékeit tároja.

### K: Miért más érték az első szenzor értéke minden futtatáskor?

V: Mert az értékek véletlenszerűen generálódnak a megadott tartomány (min-max) között. Ez szimulál szenzor viselkedést.

## Támogatás

Ha problémájaid vannak:

1. **Nézd meg a konzol kimenetet** - Vannak-e hibaüzenetek?
2. **Követeld a dokumentációt** - `FEJLESZTO_UTMUTATO.md`
3. **Tisztítsd az adatbázist** - `rm -f Meres.db`
4. **Fordítsd újra** - `dotnet clean && dotnet build`

## Verzió Információ

- **Alkalmazás Verzió:** 1.0
- **.NET Verzió:** 9.0
- **Adatbázis Formátum:** LiteDB 5.0.17
- **Legutóbbi Frissítés:** 2025. december 10.

---

**Jó használatot!** 🚀
