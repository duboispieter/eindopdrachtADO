using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Videotheek
{
    public class FilmManager
    {
        VideotheekDBManager manager = new VideotheekDBManager();

        public ObservableCollection<Film> GetAlleFilms()
        {
            ObservableCollection<Film> films = new ObservableCollection<Film>();
            using (var conVideo = manager.GetConnection())
            {
                using (var comGetFilms = conVideo.CreateCommand())
                {
                    comGetFilms.CommandText = "SELECT * FROM FILMS";
                    comGetFilms.CommandType = CommandType.Text;

                    conVideo.Open();

                    using (var rdrFilms = comGetFilms.ExecuteReader())
                    {
                        while (rdrFilms.Read())
                        {
                            int bandnrPos = rdrFilms.GetOrdinal("BandNr");
                            int titelPos = rdrFilms.GetOrdinal("Titel");
                            int genrenrPos = rdrFilms.GetOrdinal("GenreNr");
                            int invPos = rdrFilms.GetOrdinal("InVoorraad");
                            int uitvPos = rdrFilms.GetOrdinal("UitVoorraad");
                            int prijsPos = rdrFilms.GetOrdinal("Prijs");
                            int totverhPos = rdrFilms.GetOrdinal("TotaalVerhuurd");

                            Film f = new Film();

                            f.BandNr = rdrFilms.GetInt32(bandnrPos);
                            f.Titel = rdrFilms.GetString(titelPos);
                            f.GenreNr = rdrFilms.GetInt32(genrenrPos);
                            f.InVoorraad = rdrFilms.GetInt32(invPos);
                            f.UitVoorraad = rdrFilms.GetInt32(uitvPos);
                            f.Prijs = rdrFilms.GetDecimal(prijsPos);
                            f.TotaalVerhuurd = rdrFilms.GetInt32(totverhPos);

                            films.Add(f);
                        }
                    }
                }
            }
            return films;
        }

        public void VoegFilmsToe(List<Film> films)
        {
            using (var conVideo = manager.GetConnection())
            {
                using (var filmToevoegen = conVideo.CreateCommand())
                {
                    filmToevoegen.CommandText = "INSERT INTO Films(Titel, GenreNr, InVoorraad, UitVoorraad, Prijs, TotaalVerhuurd) VALUES(@titel, @genrenr, @invoorraad, @uitvoorraad, @prijs, @totaalverhuurd)";
                    filmToevoegen.CommandType = CommandType.Text;

                    var parTitel = filmToevoegen.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    filmToevoegen.Parameters.Add(parTitel);

                    var parGenreNr = filmToevoegen.CreateParameter();
                    parGenreNr.ParameterName = "@genrenr";
                    filmToevoegen.Parameters.Add(parGenreNr);

                    var parInV = filmToevoegen.CreateParameter();
                    parInV.ParameterName = "@invoorraad";
                    filmToevoegen.Parameters.Add(parInV);

                    var parUitV = filmToevoegen.CreateParameter();
                    parUitV.ParameterName = "@uitvoorraad";
                    filmToevoegen.Parameters.Add(parUitV);

                    var parPrijs = filmToevoegen.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    filmToevoegen.Parameters.Add(parPrijs);

                    var parTotV = filmToevoegen.CreateParameter();
                    parTotV.ParameterName = "@totaalverhuurd";
                    filmToevoegen.Parameters.Add(parTotV);
                    conVideo.Open();
                    foreach (Film f in films)
                    {
                        parTitel.Value = f.Titel;
                        parGenreNr.Value = f.GenreNr;
                        parInV.Value = f.InVoorraad;
                        parUitV.Value = f.UitVoorraad;
                        parPrijs.Value = f.Prijs;
                        parTotV.Value = f.TotaalVerhuurd;

                        filmToevoegen.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
