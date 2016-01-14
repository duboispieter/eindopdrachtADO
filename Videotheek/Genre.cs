using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videotheek
{
    public class Genre
    {
        private int genreNrValue;

        public int GenreNr
        {
            get { return genreNrValue; }
            set { genreNrValue = value; }
        }

        private string genreNaamValue;

        public string GenreNaam
        {
            get { return genreNaamValue; }
            set { genreNaamValue = value; }
        }
        
        
    }
}
