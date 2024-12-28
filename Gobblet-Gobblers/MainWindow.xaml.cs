using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

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

            //kolory obramowania graczy

            UstawKolorButtona(gracz1_przycisk1, ColorComboBox);
            UstawKolorButtona(gracz1_przycisk2, ColorComboBox);
            UstawKolorButtona(gracz1_przycisk3, ColorComboBox);

            UstawKolorButtona(gracz2_przycisk1, ColorComboBox_Kopiuj);
            UstawKolorButtona(gracz2_przycisk2, ColorComboBox_Kopiuj);
            UstawKolorButtona(gracz2_przycisk3, ColorComboBox_Kopiuj);


        }

        private void GenerujPlansze()
        {
            Plansza.Children.Clear();
            Plansza.RowDefinitions.Clear();
            Plansza.ColumnDefinitions.Clear();

            if (!int.TryParse(PoleRozmiar.Text, out rozmiarPlanszy) || rozmiarPlanszy < 3)
                rozmiarPlanszy = 3;

            for (int i = 0; i < rozmiarPlanszy; i++)
            {
                Plansza.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                Plansza.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

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


                    pole.MouseLeftButtonUp += PolePlanszy_Click;

                    Grid.SetRow(pole, wiersz);
                    Grid.SetColumn(pole, kolumna);
                    Plansza.Children.Add(pole);
                }
            }
        }

        private Brush kolorGracza1 = Brushes.Black;
        private Brush kolorGracza2 = Brushes.Black;

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == ColorComboBox)
            {
                if (ColorComboBox.SelectedItem is ComboBoxItem wybranyElement)
                {
                    string kolorNazwa = wybranyElement.Content.ToString();
                    kolorGracza1 = kolorNazwa switch
                    {
                        "Czerwony" => Brushes.Red,
                        "Zielony" => Brushes.Green,
                        "Niebieski" => Brushes.Blue,
                        _ => Brushes.Black
                    };
                }
            }
            else if (sender == ColorComboBox_Kopiuj)
            {
                if (ColorComboBox_Kopiuj.SelectedItem is ComboBoxItem wybranyElement)
                {
                    string kolorNazwa = wybranyElement.Content.ToString();
                    kolorGracza2 = kolorNazwa switch
                    {
                        "Czerwony" => Brushes.Red,
                        "Zielony" => Brushes.Green,
                        "Niebieski" => Brushes.Blue,
                        _ => Brushes.Black
                    };
                }
            }
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

            //kolorgraczy es


        }

        private void UstawKolorButtona(Button przycisk, ComboBox comboBox)
        {
            if (comboBox.SelectedItem is ComboBoxItem wybranyElement)
            {
                string kolorNazwa = wybranyElement.Content.ToString();
                Brush nowyKolor = kolorNazwa switch
                {
                    "Czerwony" => Brushes.Red,
                    "Zielony" => Brushes.Green,
                    "Niebieski" => Brushes.Blue,
                    _ => Brushes.Black
                };
                przycisk.BorderBrush = nowyKolor;
            }
        }

        private bool czyWybrany = false;
        private Button wybranyPrzyciskGracz1 = null;
        private ImageSource wybranyObraz = null;
        private bool czyRundaGracza1 = true;

        private void mieczGracz1_click(object sender, RoutedEventArgs e)
        {
            czyWybrany = true;
            gracz2_przycisk1.IsEnabled = false;
            gracz2_przycisk2.IsEnabled = false;
            gracz2_przycisk3.IsEnabled = false;

            if (wybranyPrzyciskGracz1 != null)
            {
                wybranyPrzyciskGracz1.Effect = null;
            }

            var przycisk = sender as Button;
            if (przycisk != null)
            {
                przycisk.Effect = new DropShadowEffect
                {
                    Color = Colors.LightGoldenrodYellow,
                    BlurRadius = 20,
                    ShadowDepth = 5
                };

                wybranyPrzyciskGracz1 = przycisk;
                wybranyObraz = ((Image)przycisk.Content).Source;
            }

            rundaLabel.Content = "Runda gracza " + gracz1.Text;
        }



        private void mieczGracz2_click(object sender, RoutedEventArgs e)
        {
            czyWybrany = true;
           

            if (wybranyPrzyciskGracz1 != null)
            {
                wybranyPrzyciskGracz1.Effect = null;
            }

            var przycisk = sender as Button;
            if (przycisk != null)
            {
                przycisk.Effect = new DropShadowEffect
                {
                    Color = Colors.LightGoldenrodYellow,
                    BlurRadius = 20,
                    ShadowDepth = 5
                };

                wybranyPrzyciskGracz1 = przycisk;
                wybranyObraz = ((Image)przycisk.Content).Source;
            }

            rundaLabel.Content = "Runda gracza " + gracz1.Text;
        }

        private void PolePlanszy_Click(object sender, RoutedEventArgs e)
        {
            if (czyWybrany && wybranyObraz != null)
            {
                
                Label pole = sender as Label;
                
                   
                    pole.Content = new Image { Source = wybranyObraz, Stretch = Stretch.Uniform };

                   
                    if (czyRundaGracza1)
                    {
                        pole.Background = kolorGracza1;
                    }
                    else
                    {
                        pole.Background = kolorGracza2;
                    }

                   
                    czyWybrany = false;
                    wybranyObraz = null;

                 
                    czyRundaGracza1 = !czyRundaGracza1;

                    if (czyRundaGracza1)
                    {
                    
                        gracz1_przycisk1.IsEnabled = true;
                        gracz1_przycisk2.IsEnabled = true;
                        gracz1_przycisk3.IsEnabled = true;

                        gracz2_przycisk1.IsEnabled = false;
                        gracz2_przycisk2.IsEnabled = false;
                        gracz2_przycisk3.IsEnabled = false;

                        rundaLabel.Content = "Runda gracza " + gracz1.Text;
                    }
                    else
                    {
                        
                        gracz1_przycisk1.IsEnabled = false;
                        gracz1_przycisk2.IsEnabled = false;
                        gracz1_przycisk3.IsEnabled = false;

                        gracz2_przycisk1.IsEnabled = true;
                        gracz2_przycisk2.IsEnabled = true;
                        gracz2_przycisk3.IsEnabled = true;

                        rundaLabel.Content = "Runda gracza " + gracz1_Kopiuj1.Text;
                    }
                
                
            }
            else
            {
                MessageBox.Show("Wybierz najpierw rozmiar miecza!");
            }
        }

    }
}
