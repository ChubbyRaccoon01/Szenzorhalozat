#!/usr/bin/env python3
"""
Szenzorhalózat PDF Generator
Markdown és LaTeX fájlok konvertálása PDF-vé
"""

import subprocess
import os
import sys
from pathlib import Path

class PDFGenerator:
    def __init__(self):
        self.project_dir = Path(__file__).parent.absolute()
        self.tex_files = [
            "FEJLESZTO_UTMUTATO.tex",
            "FELHASZNALO_UTMUTATO.tex"
        ]
        self.md_files = [
            "FEJLESZTO_UTMUTATO.md",
            "FELHASZNALO_UTMUTATO.md"
        ]
    
    def check_pdflatex(self):
        """Ellenőrizd, hogy pdflatex telepített-e"""
        try:
            subprocess.run(["pdflatex", "--version"], 
                         capture_output=True, 
                         check=True)
            return True
        except (subprocess.CalledProcessError, FileNotFoundError):
            return False
    
    def check_pandoc(self):
        """Ellenőrizd, hogy pandoc telepített-e"""
        try:
            subprocess.run(["pandoc", "--version"], 
                         capture_output=True, 
                         check=True)
            return True
        except (subprocess.CalledProcessError, FileNotFoundError):
            return False
    
    def generate_with_pdflatex(self):
        """PDF generálás pdflatex-szel"""
        print("📄 PDF generálás pdflatex-szel...")
        os.chdir(self.project_dir)
        
        for tex_file in self.tex_files:
            if not Path(tex_file).exists():
                print(f"⚠️  Fájl nem létezik: {tex_file}")
                continue
            
            print(f"\n📝 Feldolgozás: {tex_file}")
            try:
                # Első futtatás
                result = subprocess.run(
                    ["pdflatex", "-interaction=nonstopmode", "-shell-escape", tex_file],
                    capture_output=True,
                    text=True,
                    timeout=60
                )
                
                if result.returncode == 0:
                    pdf_file = tex_file.replace(".tex", ".pdf")
                    if Path(pdf_file).exists():
                        print(f"✅ Sikeres: {pdf_file}")
                    else:
                        print(f"❌ PDF fájl nem jött létre")
                else:
                    print(f"❌ Hiba: {result.stdout[-200:]}")
            
            except subprocess.TimeoutExpired:
                print("❌ Timeout: pdflatex túl sokáig futott")
            except Exception as e:
                print(f"❌ Hiba: {e}")
    
    def generate_with_pandoc(self):
        """PDF generálás pandoc-kal (Markdown-ból)"""
        print("\n📄 PDF generálás pandoc-kal...")
        os.chdir(self.project_dir)
        
        for md_file in self.md_files:
            if not Path(md_file).exists():
                print(f"⚠️  Fájl nem létezik: {md_file}")
                continue
            
            pdf_file = md_file.replace(".md", ".pdf")
            print(f"\n📝 Feldolgozás: {md_file} → {pdf_file}")
            
            try:
                result = subprocess.run([
                    "pandoc",
                    md_file,
                    "-o", pdf_file,
                    "-f", "markdown",
                    "-t", "pdf",
                    "--pdf-engine=xelatex",
                    "-V", "lang=hu",
                    "-V", "geometry:margin=1in"
                ], capture_output=True, text=True, timeout=60)
                
                if result.returncode == 0 and Path(pdf_file).exists():
                    print(f"✅ Sikeres: {pdf_file}")
                else:
                    print(f"❌ Hiba: {result.stderr}")
            
            except subprocess.TimeoutExpired:
                print("❌ Timeout: pandoc túl sokáig futott")
            except Exception as e:
                print(f"❌ Hiba: {e}")
    
    def cleanup(self):
        """Segédfájlok törlése"""
        print("\n🧹 Segédfájlok törlése...")
        extensions = [".aux", ".log", ".out", ".fls", ".fdb_latexmk"]
        
        for ext in extensions:
            for file in self.project_dir.glob(f"*{ext}"):
                try:
                    file.unlink()
                    print(f"Törölve: {file.name}")
                except Exception as e:
                    print(f"Nem sikerült törölni {file.name}: {e}")
    
    def list_pdfs(self):
        """Listázd az elkészült PDF-eket"""
        print("\n📊 Elkészült PDF-ek:")
        pdfs = list(self.project_dir.glob("FEJLESZTO_UTMUTATO*.pdf")) + \
               list(self.project_dir.glob("FELHASZNALO_UTMUTATO*.pdf"))
        
        if pdfs:
            for pdf in sorted(pdfs):
                size = pdf.stat().st_size / 1024  # KB-ban
                print(f"  ✓ {pdf.name} ({size:.1f} KB)")
        else:
            print("  Nincs PDF fájl")
    
    def run(self):
        """Fő futtatás"""
        print("=" * 60)
        print("🚀 Szenzorhalózat PDF Generator")
        print("=" * 60)
        
        # Ellenőrzés
        has_pdflatex = self.check_pdflatex()
        has_pandoc = self.check_pandoc()
        
        print(f"\n📋 Elérhetőségek:")
        print(f"  pdflatex: {'✅' if has_pdflatex else '❌'}")
        print(f"  pandoc: {'✅' if has_pandoc else '❌'}")
        
        if not has_pdflatex and not has_pandoc:
            print("\n⚠️  Hiba: wagyis pdflatex vagy pandoc szükséges!")
            print("\nTelepítési útmutató:")
            print("  Windows: Töltsd le a MiKTeX-et (https://miktex.org)")
            print("  macOS: Töltsd le a MacTeX-et")
            print("  Linux: sudo apt-get install texlive-latex-extra")
            print("  Linux (pandoc): sudo apt-get install pandoc")
            sys.exit(1)
        
        # Konvertálás
        if has_pdflatex:
            self.generate_with_pdflatex()
        elif has_pandoc:
            self.generate_with_pandoc()
        
        # Megtisztítás
        self.cleanup()
        
        # Listázás
        self.list_pdfs()
        
        print("\n" + "=" * 60)
        print("✨ Kész!")
        print("=" * 60)

if __name__ == "__main__":
    generator = PDFGenerator()
    generator.run()
