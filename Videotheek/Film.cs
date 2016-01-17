using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videotheek
{
   public class Film: INotifyPropertyChanged
    {
       public bool Changed;
        private int bandNrValue;

        public int BandNr
        {
            get { return bandNrValue; }
            set { bandNrValue = value;}
        }

        private string titelValue;

        public string Titel
        {
            get { return titelValue; }
            set { titelValue = value; }
        }

        private int genreNrValue;

        public int GenreNr
        {
            get { return genreNrValue; }
            set { genreNrValue = value; }
        }

        private int? inVoorraadValue;

        public int? InVoorraad
        {
            get { return inVoorraadValue; }
            set { inVoorraadValue = value; Changed = true; NotifyPropertyChanged("InVoorraad"); }
        }

        private int? uitVoorraadValue;

        public int? UitVoorraad
        {
            get { return uitVoorraadValue; }
            set { uitVoorraadValue = value; Changed = true; NotifyPropertyChanged("UitVoorraad"); }
        }

        private decimal? prijsValue;

        public decimal? Prijs
        {
            get { return prijsValue; }
            set { prijsValue = value;}
        }

        private int? totaalVerhuurdValue;

        

        public int? TotaalVerhuurd
        {
            get { return totaalVerhuurdValue; }
            set { totaalVerhuurdValue = value; Changed = true; NotifyPropertyChanged("TotaalVerhuurd"); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}
