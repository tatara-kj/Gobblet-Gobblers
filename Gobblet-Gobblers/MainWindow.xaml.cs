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
                Plansza.RowDefinitions.Add(new RowDefinition());
                Plansza.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int wiersz = 0; wiersz < rozmiarPlanszy; wiersz++)
            {
                for (int kolumna = 0; kolumna < rozmiarPlanszy; kolumna++)
                {
                    Label pole = new Label
                    {
                        Background = Brushes.LightCyan,
                        BorderBrush = Brushes.Maroon,
                        BorderThickness = new Thickness(1),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 16,
                        Margin = new Thickness(1),
                        Content = ""
                    };
                    Grid.SetRow(pole, wiersz);
                    Grid.SetColumn(pole, kolumna);
                    Plansza.Children.Add(pole);
                }
            }
        }
    }
}