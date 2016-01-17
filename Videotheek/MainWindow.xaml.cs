using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
        List<Film> oudeFilms = new List<Film>();
        List<Film> gewijzigdeFilms = new List<Film>();
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
            gridDetail.DataContext = filmViewSource; // Datacontext wordt hier ingevuld, om initiële validatie tegen te gaan.

            System.Windows.Data.CollectionViewSource genreViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("genreViewSource")));
            genres = gManager.GetAlleGenres();
            genreViewSource.Source = genres;
        }


        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (btnToevoegen.Content.ToString() == "Toevoegen")
            {
                ControlsNaarBevestigModus();

                nieuweFilm.InVoorraad = null;
                nieuweFilm.UitVoorraad = null;
                nieuweFilm.Prijs = null;
                nieuweFilm.TotaalVerhuurd = null;
                nieuweFilm.Changed = false;

                films.Add(nieuweFilm);
                lstFilms.SelectedItem = nieuweFilm;
                cboGenre.SelectedIndex = 0;
            }
            else //Als de knop op "bevestigen" staat
            {
                if (NieuweFilmValideren())
                    e.Handled = true;

            }
        }
        private void ControlsNaarBevestigModus()
        {
            btnToevoegen.Content = "Bevestigen";
            btnVerwijderen.Content = "Annuleren";
            btnOpslaan.IsEnabled = false;
            btnVerhuur.IsEnabled = false;
            lstFilms.IsEnabled = false;
            btnToevoegen.Tag = "true";
        }
        private void TerugNaarOrigineleControls()
        {

            btnToevoegen.Content = "Toevoegen";
            btnVerwijderen.Content = "Verwijderen";
            btnOpslaan.IsEnabled = true;
            btnVerhuur.IsEnabled = true;
            lstFilms.IsEnabled = true;
            btnToevoegen.Tag = "false";
                        lstFilms.ScrollIntoView(lstFilms.SelectedItem);
        }

        private bool NieuweFilmValideren()
        {
            bool bevatFouten = false;
            string fouten = "Kan film niet toevoegen. Corrigeer de fout in veld(en): \n\n";

            foreach (var control in gridDetail.Children)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    if (Validation.GetHasError(txt))
                    {

                        foreach (ValidationError err in Validation.GetErrors(txt))
                        {
                            fouten += (txt).Tag + "\n";
                            bevatFouten = true;
                        }
                    }
                }
            }
            if (bevatFouten)
            {
                MessageBox.Show(fouten, "Fout", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
                return true;
            }
            else
            {

                nieuweFilm.Titel = nieuweFilm.Titel.ToUpper();
                lstFilms.Items.SortDescriptions.Add(new SortDescription("Titel", ListSortDirection.Ascending));
                TerugNaarOrigineleControls();
                return false;
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (btnVerwijderen.Content.ToString() == "Annuleren")
            {
                films.Remove(nieuweFilm);
                nieuweFilms.Clear();
                TerugNaarOrigineleControls();

            }
            else
            {
                FilmVerwijderen((Film)lstFilms.SelectedItem);

            }
        }

        private void FilmVerwijderen(Film f)
        {
            if (MessageBox.Show("Ben je zeker dat je deze film wil verwijderen?", "Verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)

                films.Remove(f);
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
            if (e.OldItems != null)
            {
                foreach (Film f in e.OldItems)
                    oudeFilms.Add(f);
            }
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wilt u alles wegschrijven naar de database?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                WijzigingenOpslaan();
        }

        private void WijzigingenOpslaan()
        {
            fManager.VoegFilmsToe(nieuweFilms);
            fManager.VerwijderFilms(oudeFilms);

            foreach (Film f in films)
            {
                if (f.Changed == true)
                    gewijzigdeFilms.Add(f);
            }
            fManager.UpdateVoorraad(gewijzigdeFilms);
        }

        private void lstFilms_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Delete) && (lstFilms.SelectedItem != null))
            {
                FilmVerwijderen((Film)lstFilms.SelectedItem);
            }
        }

        private void btnVerhuur_Click(object sender, RoutedEventArgs e)
        {

            Film f = (Film)lstFilms.SelectedItem;
             if (f.InVoorraad > 0)
            {
                f.InVoorraad -= 1;
                f.UitVoorraad += 1;
                f.TotaalVerhuurd += 1;
                
            }
            else MessageBox.Show("Alle films zijn verhuurd!", "Verhuur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

     
    }
}
