using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Videotheek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FilmManager fManager = new FilmManager();
        GenreManager gManager = new GenreManager();
        ObservableCollection<Film> films = new ObservableCollection<Film>();
        List<Film> nieuweFilms = new List<Film>();
        List<Genre> genres = new List<Genre>();
        Film nieuweFilm = new Film();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource filmViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("filmViewSource")));
            films = fManager.GetAlleFilms();
            filmViewSource.Source = films;
            films.CollectionChanged += this.OnCollectionChanged;
            System.Windows.Data.CollectionViewSource genreViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("genreViewSource")));
            genres = gManager.GetAlleGenres();
            genreViewSource.Source = genres;
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (btnToevoegen.Content.ToString() == "Toevoegen")
            {
                btnToevoegen.Content = "Bevestigen";
                btnVerwijderen.Content = "Annuleren";
                btnOpslaan.IsEnabled = false;
                btnVerhuur.IsEnabled = false;
                lstFilms.IsEnabled = false;
                btnToevoegen.Tag = "true";
films.Add(nieuweFilm);
                lstFilms.SelectedItem = nieuweFilm;
            }
            else
            {
                TerugNaarOrigineleControls();
            }


                
        }

        private void TerugNaarOrigineleControls()
        {
            btnToevoegen.Content = "Toevoegen";
            btnVerwijderen.Content = "Verwijderen";
            btnOpslaan.IsEnabled = true;
            btnVerhuur.IsEnabled = true;
            lstFilms.IsEnabled = true;
            btnToevoegen.Tag = "false";
            lstFilms.SelectedIndex = 0;
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (btnVerwijderen.Content.ToString() == "Annuleren")
            {
                films.Remove(nieuweFilm);
                nieuweFilms.Clear();
                TerugNaarOrigineleControls();
               
            }
        }


        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Film f in e.NewItems)
                {
                    nieuweFilms.Add(f);
                }
            }
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
//VALIDATIEVOORWAARDEN CHECKEN!!
            fManager.VoegFilmsToe(nieuweFilms);
        }
    }
}
