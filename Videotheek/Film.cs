using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videotheek
{
   public class Film
    {
       public bool Changed;
        private int bandNrValue;

        public int BandNr
        {
            get { return bandNrValue; }
            set { bandNrValue = value; Changed = true; }
        }

        private string titelValue;

        public string Titel
        {
            get { return titelValue; }
            set { titelValue = value; Changed = true; }
        }

        private int genreNrValue;

        public int GenreNr
        {
            get { return genreNrValue; }
            set { genreNrValue = value; Changed = true; }
        }

        private int inVoorraadValue;

        public int InVoorraad
        {
            get { return inVoorraadValue; }
            set { inVoorraadValue = value; Changed = true; }
        }

        private int uitVoorraadValue;

        public int UitVoorraad
        {
            get { return uitVoorraadValue; }
            set { uitVoorraadValue = value; Changed = true; }
        }

        private decimal prijsValue;

        public decimal Prijs
        {
            get { return prijsValue; }
            set { prijsValue = value; Changed = true; }
        }

        private int totaalVerhuurdValue;

        public int TotaalVerhuurd
        {
            get { return totaalVerhuurdValue; }
            set { totaalVerhuurdValue = value; Changed = true; }
        }

        public Film()
        {
            this.Changed = false;
        }

       public Film(int bandnr, string titel, int genrenr, int invoorraad, int uitvoorraad, decimal prijs, int totaalverhuurd)
        {
            BandNr = bandnr;
            Titel = titel;
            GenreNr = genrenr;
            InVoorraad = invoorraad;
            UitVoorraad = uitvoorraad;
            Prijs = prijs;
            TotaalVerhuurd = totaalverhuurd;
            this.Changed = false;
        }
        
        
        
        
        
        
        
    }
}
