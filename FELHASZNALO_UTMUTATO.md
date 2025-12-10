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
   Name: Rotation Sensor, Type: ROT, Value: 2800.45 RPM, CompID: S-ROT-003
   Name: Vibration Sensor, Type: VIB, Value: 4.23 m/s², CompID: S-VIB-004
   Name: CO2 Sensor, Type: CO2, Value: 3500 PPM, CompID: S-CO2-005
   Name: Pressure Sensor, Type: PRES, Value: 1.8 bar, CompID: S-PRES-006
   ```

2. **Mérések Indítása**
   - Az alkalmazás 5 alkalommal indít méréseket
   - Minden mérés között 1 másodperc szünet
   - Az összes szenzor mér (6 szenzor × 5 mérés = 30 mérési adat)

3. **Mérési Adatok Megjelenítése**
   ```
   Meres inditasa...
   Szenzor ID: 1, Meres ideje: 12/10/2025 15:59:25, Adat: 93.01
   Szenzor ID: 2, Meres ideje: 12/10/2025 15:59:25, Adat: 61.23
   ...
   ```

4. **Adatbázis Statisztikája**
   ```
   Adatbázis táblái:
     T20251210155924 - 6 elem
     T20251210155924_Adatok - 30 elem
   ```

### Interaktív Menü

A mérések befejezése után az alkalmazás egy interaktív menüt mutat:

```
=== Main Menu ===
1. List Sensors
2. Export JSON
3. List Database Content
4. Exit
Enter your choice: 
```

#### 1. List Sensors (Szenzorok Listázása)

Ez a menüpont lehetővé teszi a szenzorokat típus szerint szűrni és megjeleníteni:

```
===Sensor Listing===
1. List All
====================
List by Type: 
2. Temperature Sensors
3. Rotation Sensors
4. Vibration Sensors
5. CO2 Sensors
6. Pressure Sensors
====================
7. Return to Main Menu
Enter your choice: 
```

**Opciók:**
- **1. List All**: Minden szenzor megjelenítése
- **2-6**: Adott típusú szenzor szűrése (LINQ `OfType<>()` használatával)
- **7**: Vissza a főmenüre

**Kimenet Példa (Temperature szenzor):**
```
Temperature Sensors:
Name: Temperature Sensor, Type: TEMP, Value: 116.13 °C, CompID: S-TEMP-001
Name: Temperature Sensor, Type: TEMP, Value: 112.33 °C, CompID: S-TEMP-002
```

#### 2. Export JSON (JSON Exportálás)

A szenzorokat JSON formátumban exportálja a `sensors_export.json` fájlba.

```bash
# Létrehozódott fájl
sensors_export.json

# Tartalma:
[
  {
    "id": 1,
    "name": "Temperature Sensor",
    "type": "TEMP",
    "unit": "°C",
    "currentValue": 116.13,
    "status": "Terhelés",
    "compositeID": "S-TEMP-001",
    "minMax": [60, 120]
  },
  ...
]
```

#### 3. List Database Content (Adatbázis Tartalom)

Megjeleníti az összes mérési adatot az adatbázisból:

```
Szenzor ID: 1, Meres ideje: 12/10/2025 15:59:25, Érték: 93.00813511586887
Szenzor ID: 2, Meres ideje: 12/10/2025 15:59:25, Érték: 61.227329681256315
...
```

#### 4. Exit (Kilépés)

Az alkalmazás bezárása és az adatbázis korrekt lezárása.

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
Adat:           93.00813511586887  (tárolt érték)
```

## Közös Forgatókönyvek

### Forgatókönyv 1: Szenzor Listázása Típus Szerint

1. Futtasd az alkalmazást
2. Válaszd az "1. List Sensors" opciót a főmenüből
3. Válaszd a kívánt szenzor típust (pl. "2. Temperature Sensors")
4. Az alkalmazás megjeleníti az adott típusú szenzorokat

**Kimenet:**
```
All Sensors:
Name: Temperature Sensor, Type: TEMP, Value: 116.13 °C, CompID: S-TEMP-001
Name: Temperature Sensor, Type: TEMP, Value: 112.33 °C, CompID: S-TEMP-002
Name: Rotation Sensor, Type: ROT, Value: 2800.45 RPM, CompID: S-ROT-003
...
```

### Forgatókönyv 2: Szenzor Adatok JSON Exportálása

1. Futtasd az alkalmazást
2. Válaszd az "2. Export JSON" opciót a főmenüből
3. Az alkalmazás létrehozza a `sensors_export.json` fájlt
4. A fájl megnyitható szövegszerkesztővel vagy JSON viewerrel

### Forgatókönyv 3: Mérési Adatok Megtekintése

1. Futtasd az alkalmazást
2. Válaszd a "3. List Database Content" opciót a főmenüből
3. Az alkalmazás megjeleníti az összes tárolt mérési adatot

**Kimenet:**
```
DB contents:
Szenzor ID: 1, Meres ideje: 12/10/2025 15:59:25, Érték: 93.00813511586887
Szenzor ID: 2, Meres ideje: 12/10/2025 15:59:25, Érték: 61.227329681256315
...
```

### Forgatókönyv 4: Újabb Futtatás (új adatok)

```bash
dotnet run --project Szenzorhalozat.Console/Szenzorhalozat.Console.csproj
```

Az új futtatás:
- Egy új időpecsétel-alapú gyűjteményt hoz létre
- Az előző futtatások adatai megmaradnak az adatbázisban
- Összes futtatás adatai elérhetőek

### Forgatókönyv 5: Adatbázis Törlése (tiszta indítás)

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
Szenzor ID: 1, Meres ideje: 12/10/2025 15:59:25, Adat: 93.01
Szenzor ID: 2, Meres ideje: 12/10/2025 15:59:25, Adat: 61.23
```

- `Meres inditasa...`: Mérési ciklus kezdete
- `Szenzor ID`: A szenzor azonosítója az adatbázisban
- `Meres ideje`: A mérés időpontja (dátum és idő)
- `Adat`: A mért érték (minden szenzortípusra vonatkozik)

### Adatbázis Statisztika

```
Adatbázis táblái:
  T20251210155924 - 6 elem
  T20251210155924_Adatok - 30 elem
```

- `T20251210155924`: Az aktuális futtatás szenzor gyűjteménye (6 szenzor)
- `T20251210155924_Adatok`: Az aktuális futtatás mérési adatok gyűjteménye (30 mérés)

## Kérdések és Válaszok

### K: Mit történik az előző futtatások adataival?

V: Az adatbázisban maradnak. Új futtatások minden alkalommal egy új gyűjteményt hoznak létre az aktuális időzónának megfelelő névvel.

### K: Miért vannak 30 mérési adat, ha 5 mérés van?

V: Mert 6 szenzor van, és mind a hatuk mér minden ciklus alatt: 5 mérés × 6 szenzor = 30 adat.

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
