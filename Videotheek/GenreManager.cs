using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Videotheek
{
    public class GenreManager
    {
        VideotheekDBManager manager = new VideotheekDBManager();

        public List<Genre> GetAlleGenres()
        {
            List<Genre> genres = new List<Genre>();
            using (var conVideo = manager.GetConnection())
            {
                using (var comGenres = conVideo.CreateCommand())
                {
                    comGenres.CommandType = CommandType.Text;
                    comGenres.CommandText = "SELECT * FROM GENRES";

                    conVideo.Open();
                    
                    using(var rdrGenres = comGenres.ExecuteReader())
                    {
                        while(rdrGenres.Read())
                        {
                            Genre g = new Genre();
                            g.GenreNr = rdrGenres.GetInt32(rdrGenres.GetOrdinal("GenreNr"));
                            g.GenreNaam = rdrGenres.GetString(rdrGenres.GetOrdinal("Genre"));

                            genres.Add(g);
                            
                        }
                    }
                }
            }
            return genres;
        }
    }
}
