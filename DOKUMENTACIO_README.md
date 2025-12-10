# 📚 Szenzorhalózat - Dokumentáció

Teljes dokumentáció a Szenzorhalózat projekthez fejlesztőknek és felhasználóknak.

## 📋 Dokumentáció Fájlok

### 1. **Fejlesztői Útmutató**

#### Markdown Verzió
- **Fájl:** `FEJLESZTO_UTMUTATO.md`
- **Formátum:** Markdown
- **Méret:** ~7 KB
- **Tartalom:**
  - Projekt áttekintése
  - Projekt struktúra
  - Főbb komponensek (Sensor, Database, Szenzorhalozat)
  - Fejlesztési workflow
  - Adatáramlás
  - Hibakeresés
  - Kódolási irányelvek

#### LaTeX Verzió
- **Fájl:** `FEJLESZTO_UTMUTATO.tex`
- **Formátum:** LaTeX (PDF konvertálható)
- **Méret:** ~8 KB
- **Azonos tartalom, profi formázással**

### 2. **Felhasználói Útmutató**

#### Markdown Verzió
- **Fájl:** `FELHASZNALO_UTMUTATO.md`
- **Formátum:** Markdown
- **Méret:** ~7.3 KB
- **Tartalom:**
  - Telepítési útmutató
  - Szenzortípusok leírása
  - Adatbázis szerkezete
  - Gyakorlati forgatókönyvek
  - Hibakezelés
  - FAQ

#### LaTeX Verzió
- **Fájl:** `FELHASZNALO_UTMUTATO.tex`
- **Formátum:** LaTeX (PDF konvertálható)
- **Méret:** ~10.5 KB
- **Azonos tartalom, profi formázással**

### 3. **PDF Generálási Útmutató**

- **Fájl:** `PDF_GENERALS_UTMUTATO.md`
- **Tartalom:**
  - Különböző platformokon (Windows, macOS, Linux) a PDF generálás módja
  - Online konverziós lehetőségek
  - Docker telepítés
  - Hibakeresés

### 4. **PDF Generator Script**

- **Fájl:** `generate_pdf.py`
- **Típus:** Python 3 script
- **Funkció:** Automatikus PDF generálás
- **Előfeltételek:** pdflatex vagy pandoc

## 🚀 Gyors Indítás

### Dokumentáció Olvasása

**Online (GitHub/GitLab-en):**
- `FEJLESZTO_UTMUTATO.md` - Fejlesztőknek
- `FELHASZNALO_UTMUTATO.md` - Végfelhasználóknak

**Lokálisan:**
```bash
cat FEJLESZTO_UTMUTATO.md
cat FELHASZNALO_UTMUTATO.md
```

### PDF Generálás

#### Módszer 1: Python Script (Ajánlott)

```bash
python3 generate_pdf.py
```

Automatikusan detektálja az elérhető eszközöket és generál PDF-eket.

#### Módszer 2: pdflatex (Linux/macOS)

```bash
pdflatex -interaction=nonstopmode FEJLESZTO_UTMUTATO.tex
pdflatex -interaction=nonstopmode FELHASZNALO_UTMUTATO.tex
```

#### Módszer 3: Online (Overleaf)

1. Nyisd meg: https://www.overleaf.com/
2. Hozz létre egy új projektet
3. Másold be a `.tex` fájl tartalmát
4. Az Overleaf automatikusan PDF-et generál

#### Módszer 4: Pandoc (Markdown-ból)

```bash
pandoc FEJLESZTO_UTMUTATO.md -o FEJLESZTO_UTMUTATO.pdf
pandoc FELHASZNALO_UTMUTATO.md -o FELHASZNALO_UTMUTATO.pdf
```

## 📊 Dokumentáció Statisztika

| Dokumentum | Markdown | LaTeX | Oldal | Méret |
|-----------|----------|-------|-------|-------|
| Fejlesztői Útmutató | ✓ | ✓ | ~8 | 6.8 + 8.0 KB |
| Felhasználói Útmutató | ✓ | ✓ | ~10 | 7.3 + 10.5 KB |
| **Összesen** | | | **~18** | **~32 KB** |

## 🎯 Kik számára készült?

### Fejlesztői Útmutató
- Szoftver fejlesztők
- Rendszertervezők
- Rendszergabdálozók
- Új szenzortípusok hozzáadásához

### Felhasználói Útmutató
- Végfelhasználók
- Adminisztrátorok
- Teszt csapatok
- Projekt menedzserek

## 📝 Formátumok Összehasonlítása

| Jellemző | Markdown | LaTeX |
|----------|----------|-------|
| Szerkesztés | Könnyű | Összetett |
| Olvashatóság | Jó | Profi |
| PDF Formázás | Alap | Haladó |
| Online Nézet | Kitűnő | Gyenge |
| Nyomtatás | Jó | Excellent |
| Verziókövetés | Jó | Jó |

## 🔄 Verziókezelés

### Markdown Fájlok
- Jó verziókövetéshez
- Könnyen szerkeszthetőek
- GitHub-on szép megjelenítéssel rendelkeznek

### LaTeX Fájlok
- Profi, nyomtatható PDF-ek
- Közvetlen PDF generálás
- Formázás és stílusvezérlés

## 💡 Tanácsok

### PDF-ek Legenerálása Nélkül

Ha nem akarod a PDF-eket generálni:
1. Olvasd a `.md` fájlokat közvetlenül
2. GitHub/GitLab automata megjelenítést használ
3. Más Markdown nézegető eszközöket lehet használni

### PDF-ek Megosztása

PDF-ek megosztása javasolt:
- **Email-ben:** Kompakt formátum
- **Nyomtatás:** Profi megjelenés
- **ArchívUM:** Hosszú tárolt megőrzés
- **PDF Reader:** Kommentezés lehetőség

## 🐛 Hibajelentés

Ha hibákat találsz a dokumentációban:
1. Jegyezd fel a hibát (sor, szöveg)
2. Nyisd meg az issue-t a projektben
3. Vagy küldj egy pull request javítással

## 📞 Támogatás

Dokumentációs kérdésekhez:
1. Ellenőrizd az FAQ-ot a Felhasználói Útmutatóban
2. Nézd meg a Fejlesztői Útmutató hibakeresési részét
3. Lépj kapcsolatba a projekt menedzserrel

## 📦 Fájlok Listája

```
Szenzorhalozat/
├── FEJLESZTO_UTMUTATO.md         # Fejlesztői útmutató (Markdown)
├── FEJLESZTO_UTMUTATO.tex        # Fejlesztői útmutató (LaTeX)
├── FELHASZNALO_UTMUTATO.md       # Felhasználói útmutató (Markdown)
├── FELHASZNALO_UTMUTATO.tex      # Felhasználói útmutató (LaTeX)
├── PDF_GENERALS_UTMUTATO.md      # PDF generálási útmutató
├── generate_pdf.py               # PDF generáló Python script
└── DOKUMENTACIO_README.md        # Ez a fájl
```

## ✨ Jellemzők

- ✅ Fejlesztőknek és felhasználóknak szóló dokumentáció
- ✅ Markdown és LaTeX formátumok
- ✅ Automatikus PDF generátor script
- ✅ Teljes szenzor-típus leírások
- ✅ Gyakorlati forgatókönyvek
- ✅ Hibakeresési útmutatók
- ✅ Online konverziós lehetőségek

## 🎓 Tanulási Út

1. **Kezdő:** Olvasd a `FELHASZNALO_UTMUTATO.md`-t
2. **Fejlesztő:** Tanulmányozd a `FEJLESZTO_UTMUTATO.md`-t
3. **Szakértő:** Dolgozz a forráskóddal
4. **Bővítés:** Adj hozzá új szenzortípusokat

## 📞 Verzió Információ

- **Dokumentáció Verzió:** 1.0
- **Projekt Verzió:** 1.0
- **Legutóbbi Frissítés:** 2025. december 10.
- **Szerzők:** Szenzorhalózat Csapat

---

**Jó tanulást és fejlesztést!** 🚀

Kérdések? Olvasd a dokumentációt vagy nyisd meg az issue-t!
