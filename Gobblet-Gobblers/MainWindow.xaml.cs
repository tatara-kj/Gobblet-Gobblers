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
        private int pionkiMalyGracz1 = 4;
        private int pionkiSredniGracz1 = 4;
        private int pionkiDuzyGracz1 = 4;

        private int pionkiMalyGracz2 = 4;
        private int pionkiSredniGracz2 = 4;
        private int pionkiDuzyGracz2 = 4;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrzyciskStart_Click(object sender, RoutedEventArgs e)
        {
            GenerujPlansze();
            menu.Visibility = Visibility.Collapsed;
            UstawNazwyGraczy();



            UstawKolorButtona(gracz1_przycisk1, ColorComboBox);
            UstawKolorButtona(gracz1_przycisk2, ColorComboBox);
            UstawKolorButtona(gracz1_przycisk3, ColorComboBox);

            UstawKolorButtona(gracz2_przycisk1, ColorComboBox_Kopiuj);
            UstawKolorButtona(gracz2_przycisk2, ColorComboBox_Kopiuj);
            UstawKolorButtona(gracz2_przycisk3, ColorComboBox_Kopiuj);




            AktualizujPionki();
        }

        private void GenerujPlansze()
        {
            Plansza.Children.Clear();
            Plansza.RowDefinitions.Clear();
            Plansza.ColumnDefinitions.Clear();
            rundaLabel.Content = "Runda gracza " + gracz1.Text;

            if (!int.TryParse(PoleRozmiar.Text, out rozmiarPlanszy) || rozmiarPlanszy < 3)
                rozmiarPlanszy = 3;


            int liczbaPol = rozmiarPlanszy * rozmiarPlanszy;


            int pionkiNaPole = (liczbaPol / 6) * 6;


            pionkiDuzyGracz1 = (int)Math.Round(pionkiNaPole * 3.0 / 6);
            pionkiSredniGracz1 = (int)Math.Round(pionkiNaPole * 2.0 / 6);
            pionkiMalyGracz1 = (int)Math.Round(pionkiNaPole * 1.0 / 6);

            pionkiMalyGracz2 = (int)Math.Round(pionkiNaPole * 3.0 / 6);
            pionkiSredniGracz2 = (int)Math.Round(pionkiNaPole * 2.0 / 6);
            pionkiDuzyGracz2 = (int)Math.Round(pionkiNaPole * 1.0 / 6);


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

                    pole.MouseLeftButtonDown += PolePlanszy_Click;

                    Grid.SetRow(pole, wiersz);
                    Grid.SetColumn(pole, kolumna);
                    Plansza.Children.Add(pole);
                }
            }


            AktualizujPionki();
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
            if (czyRundaGracza1)
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
            else
            {
                MessageBox.Show("To nie Twoja kolej, poczekaj na swoją rundę!");
            }
        }

        private void AktualizujPionki()
        {

            pionki_duzy_gracz1.Content = $"x {pionkiMalyGracz1}";
            pionki_sredni_gracz1.Content = $"x {pionkiSredniGracz1}";
            pionki_maly_gracz1.Content = $"x {pionkiDuzyGracz1}";

            pionki_maly_gracz2.Content = $"x {pionkiMalyGracz2}";
            pionki_sredni_gracz2.Content = $"x {pionkiSredniGracz2}";
            pionki_duzy_gracz2.Content = $"x {pionkiDuzyGracz2}";
        }

        private void PostawPionek(string rodzajPionka, int gracz)
        {
            if (gracz == 1)
            {
                if (rodzajPionka == "Maly" && pionkiMalyGracz1 > 0)
                {
                    pionkiDuzyGracz1--;
                    if (pionkiMalyGracz1 == 0)
                    {
                        gracz1_przycisk3.IsEnabled = false;
                    }
                }
                else if (rodzajPionka == "Sredni" && pionkiSredniGracz1 > 0)
                {
                    pionkiSredniGracz1--;
                    if (pionkiSredniGracz1 == 0)
                    {
                        gracz1_przycisk2.IsEnabled = false;
                    }
                }
                else if (rodzajPionka == "Duzy" && pionkiDuzyGracz1 > 0)
                {
                    pionkiMalyGracz1--;
                    if (pionkiDuzyGracz1 == 0)
                    {
                        gracz1_przycisk1.IsEnabled = false;
                    }
                }
            }
            else if (gracz == 2)
            {
                if (rodzajPionka == "Maly" && pionkiMalyGracz2 > 0)
                {
                    pionkiMalyGracz2--;
                    if (pionkiMalyGracz2 == 0)
                    {
                        gracz2_przycisk1.IsEnabled = false;
                    }
                }
                else if (rodzajPionka == "Sredni" && pionkiSredniGracz2 > 0)
                {
                    pionkiSredniGracz2--;
                    if (pionkiSredniGracz2 == 0)
                    {
                        gracz2_przycisk2.IsEnabled = false;
                    }
                }
                else if (rodzajPionka == "Duzy" && pionkiDuzyGracz2 > 0)
                {
                    pionkiDuzyGracz2--;
                    if (pionkiDuzyGracz2 == 0)
                    {
                        gracz2_przycisk3.IsEnabled = false;
                    }
                }
            }

            AktualizujPionki();
        }

        private void mieczGracz2_click(object sender, RoutedEventArgs e)
        {
            if (!czyRundaGracza1)
            {
                czyWybrany = true;
                gracz1_przycisk1.IsEnabled = false;
                gracz1_przycisk2.IsEnabled = false;
                gracz1_przycisk3.IsEnabled = false;

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

                rundaLabel.Content = "Runda gracza " + gracz1_Kopiuj1.Text;
            }
            else
            {
                MessageBox.Show("To nie Twoja kolej, poczekaj na swoją rundę!");
            }
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
                    if (wybranyPrzyciskGracz1 == gracz1_przycisk3)
                        PostawPionek("Maly", 1);
                    else if (wybranyPrzyciskGracz1 == gracz1_przycisk2)
                        PostawPionek("Sredni", 1);
                    else if (wybranyPrzyciskGracz1 == gracz1_przycisk1)
                        PostawPionek("Duzy", 1);

                }
                else
                {
                    pole.Background = kolorGracza2;
                    if (wybranyPrzyciskGracz1 == gracz2_przycisk1)
                        PostawPionek("Maly", 2);
                    else if (wybranyPrzyciskGracz1 == gracz2_przycisk2)
                        PostawPionek("Sredni", 2);
                    else if (wybranyPrzyciskGracz1 == gracz2_przycisk3)
                        PostawPionek("Duzy", 2);
                }

                czyWybrany = false;
                wybranyObraz = null;

                czyRundaGracza1 = !czyRundaGracza1;
                UstawStanRundy();

            }
            else
            {
                MessageBox.Show("Wybierz najpierw rozmiar miecza!");
            }
        }

        private void UstawStanRundy()
        {
            if (czyRundaGracza1)
            {
                gracz1_przycisk1.IsEnabled = pionkiMalyGracz1 > 0;
                gracz1_przycisk2.IsEnabled = pionkiSredniGracz1 > 0;
                gracz1_przycisk3.IsEnabled = pionkiDuzyGracz1 > 0;

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

                gracz2_przycisk1.IsEnabled = pionkiMalyGracz2 > 0;
                gracz2_przycisk2.IsEnabled = pionkiSredniGracz2 > 0;
                gracz2_przycisk3.IsEnabled = pionkiDuzyGracz2 > 0;

                rundaLabel.Content = "Runda gracza " + gracz1_Kopiuj1.Text;
            }
        }


    }
}