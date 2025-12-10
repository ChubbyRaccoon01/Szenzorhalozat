# PDF Generálás Útmutató

## Bevezetés

Az alábbi fájlok LaTeX formátumban vannak elkészítve és PDF-vé alakíthatók:

- **FEJLESZTO_UTMUTATO.tex** - Fejlesztői útmutató
- **FELHASZNALO_UTMUTATO.tex** - Felhasználói útmutató

## PDF Generálás Helyi Gépen

### Windows és macOS

1. **MiKTeX vagy MacTeX telepítése**
   - MiKTeX (Windows): https://miktex.org/download
   - MacTeX (macOS): https://www.tug.org/mactex/

2. **PDF Generálás**
   ```bash
   cd /path/to/Szenzorhalozat
   pdflatex FEJLESZTO_UTMUTATO.tex
   pdflatex FELHASZNALO_UTMUTATO.tex
   ```

3. **Kimenet**
   ```
   FEJLESZTO_UTMUTATO.pdf
   FELHASZNALO_UTMUTATO.pdf
   ```

### Linux (Debian/Ubuntu)

```bash
# Szükséges csomagok telepítése
sudo apt-get install texlive-latex-base texlive-latex-extra texlive-fonts-recommended

# PDF Generálás
cd /path/to/Szenzorhalozat
pdflatex FEJLESZTO_UTMUTATO.tex
pdflatex FELHASZNALO_UTMUTATO.tex
```

### Linux (Alpine - Docker/Container)

```bash
# Szükséges csomagok telepítése
apk add --no-cache texlive-latex texlive-latex-extra texlive-fonts-recommended

# PDF Generálás
cd /path/to/Szenzorhalozat
pdflatex FEJLESZTO_UTMUTATO.tex
pdflatex FELHASZNALO_UTMUTATO.tex
```

## Online Konverziós Lehetőségek

Ha nem akarod telepíteni a LaTeX-et, használhatsz online eszközöket:

### 1. Overleaf (www.overleaf.com)

1. Látogass az Overleaf weboldalra
2. Hozz létre egy új projektet
3. Másold be a `.tex` fájl tartalmát
4. Az Overleaf automatikusan PDF-et generál
5. Töltsd le a PDF-et

### 2. Online LaTeX Compiler (www.tutorialspoint.com/online_compiler.php?lang=latex)

1. Nyisd meg az online LaTeX fordítót
2. Másold be a `.tex` fájl tartalmát
3. Kattints a "Compile" gombra
4. Letöltés PDF-ként

### 3. Pandoc (Alternatív eszköz)

```bash
# Pandoc telepítése
sudo apt-get install pandoc

# LaTeX-ből PDF-be konvertálás
pandoc -f latex -t pdf FEJLESZTO_UTMUTATO.tex -o FEJLESZTO_UTMUTATO.pdf
pandoc -f latex -t pdf FELHASZNALO_UTMUTATO.tex -o FELHASZNALO_UTMUTATO.pdf
```

## Docker Containerben

Ha Docker-ben vagy, és nincs LaTeX telepítve:

```dockerfile
FROM ubuntu:latest

RUN apt-get update && apt-get install -y \
    texlive-latex-base \
    texlive-latex-extra \
    texlive-fonts-recommended

WORKDIR /workspace
COPY FEJLESZTO_UTMUTATO.tex .
COPY FELHASZNALO_UTMUTATO.tex .

CMD ["sh", "-c", "pdflatex FEJLESZTO_UTMUTATO.tex && pdflatex FELHASZNALO_UTMUTATO.tex"]
```

Futtatás:
```bash
docker build -t latex-builder .
docker run -v /path/to/output:/workspace latex-builder
```

## LaTeX Fordítási Hibák

### Hiba: "File not found"

**Ok:** A babel magyar támogatása nincs telepítve.

**Megoldás:** Telepítsd az `texlive-lang-cyrillic` vagy `texlive-lang-european` csomagot.

### Hiba: "Undefined control sequence"

**Ok:** Hiányzó LaTeX csomag (pl. `geometry`, `babel`).

**Megoldás:** Telepítsd a `texlive-latex-extra` csomagot.

### Hiba: "Accented characters not working"

**Ok:** A fájl kódolása nem UTF-8.

**Megoldás:** Konvertáld UTF-8-ra:
```bash
iconv -f ISO-8859-1 -t UTF-8 FEJLESZTO_UTMUTATO.tex -o FEJLESZTO_UTMUTATO_UTF8.tex
```

## Kimenet

Sikeres fordítás után az alábbi fájlok jönnek létre:

```
FEJLESZTO_UTMUTATO.pdf      (~200KB)
FEJLESZTO_UTMUTATO.aux      (segédfájl)
FEJLESZTO_UTMUTATO.log      (naplófájl)
FEJLESZTO_UTMUTATO.out      (kimeneti fájl)

FELHASZNALO_UTMUTATO.pdf    (~200KB)
FELHASZNALO_UTMUTATO.aux    (segédfájl)
FELHASZNALO_UTMUTATO.log    (naplófájl)
FELHASZNALO_UTMUTATO.out    (kimeneti fájl)
```

Az `.aux`, `.log` és `.out` fájlok törölhetőek.

## Ajánlott Eszközök

| Eszköz | Előnyök | Hátrányok |
|--------|---------|----------|
| **MiKTeX/MacTeX** | Teljes telepítés, offline | Hosszú telepítési idő |
| **Overleaf** | Egyszerű, online, nem kell telepítés | Internet szükséges |
| **Pandoc** | Könnyű, sok formátum támogatása | Limitált LaTeX features |
| **Docker** | Reprodukálható, izolált | Összetettebb setup |

## Hasznos LinkEk

- **LaTeX Dokumentáció:** https://www.latex-project.org/
- **Overleaf Tutorials:** https://www.overleaf.com/learn
- **CTAN Packages:** https://www.ctan.org/
- **MiKTeX:** https://miktex.org/
- **TeX Live:** https://www.tug.org/texlive/

## Támogatás

Ha problémád van a PDF generálásával, írj egy issue-t a GitHub repositoryn vagy konzultálj a LaTeX dokumentációval.

---

**Sikeres PDF generálást!** 📄
