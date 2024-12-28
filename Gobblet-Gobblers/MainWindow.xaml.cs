using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pozeracze
{
    public partial class MainWindow : Window
    {
        int rozmiarPlanszy = 3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrzyciskStart_Click(object sender, RoutedEventArgs e)
        {
            GenerujPlansze();
            menu.Visibility = Visibility.Collapsed;
            UstawNazwyGraczy();
        }

        private void GenerujPlansze()
        {
            Plansza.Children.Clear();
            Plansza.RowDefinitions.Clear();
            Plansza.ColumnDefinitions.Clear();

            if (!int.TryParse(PoleRozmiar.Text, out rozmiarPlanszy) || rozmiarPlanszy < 3)
                rozmiarPlanszy = 3;

            // Dodajemy RowDefinitions i ColumnDefinitions z rozmiarami * (dostosowanie automatyczne)
            for (int i = 0; i < rozmiarPlanszy; i++)
            {
                Plansza.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                Plansza.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Tworzymy etykiety w siatce
            for (int wiersz = 0; wiersz < rozmiarPlanszy; wiersz++)
            {
                for (int kolumna = 0; kolumna < rozmiarPlanszy; kolumna++)
                {
                    Label pole = new Label
                    {
                        Background = Brushes.LightSkyBlue,
                        BorderBrush = Brushes.LightCoral,
                        BorderThickness = new Thickness(2),
                        Margin = new Thickness(3),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 16,
                        Content = ""
                    };

                    // Dodajemy etykiety do siatki
                    Grid.SetRow(pole, wiersz);
                    Grid.SetColumn(pole, kolumna);
                    Plansza.Children.Add(pole);
                }
            }

            // Wymuszamy, by wysokość i szerokość komórek były równe
            Plansza.SizeChanged += (s, e) =>
            {
                double width = Plansza.ActualWidth;
                double height = Plansza.ActualHeight;
                double size = Math.Min(width, height);
                foreach (var row in Plansza.RowDefinitions)
                {
                    row.Height = new GridLength(size / rozmiarPlanszy);
                }
                foreach (var col in Plansza.ColumnDefinitions)
                {
                    col.Width = new GridLength(size / rozmiarPlanszy);
                }
            };
        }

        private Brush kolorGracza1 = Brushes.Black; 

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            ComboBoxItem selectedItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            string nazwa_koloru = selectedItem.Content.ToString();

           
            kolorGracza1 = nazwa_koloru switch
            {
                "Czerwony" => Brushes.Red,
                "Zielony" => Brushes.Green,
                "Niebieski" => Brushes.Blue,
                _ => Brushes.Black
            };

            
            UstawKolorPrzyciskow();
        }

        private void UstawKolorPrzyciskow()
        {
            
            foreach (Button button in Plansza.Children.OfType<Button>())
            {
                button.BorderBrush = kolorGracza1;
            }
        }

        private void UstawNazwyGraczy()
        {
            imie1.Content = gracz1.Text;   
            imie2.Content = gracz1_Kopiuj1.Text;
        }

    }
}
