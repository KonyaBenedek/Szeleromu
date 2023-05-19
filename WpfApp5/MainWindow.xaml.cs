using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Szeleromu> szeleromuk;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Betöltés("szeleromu.txt");
            dataGridSzeleromuk.ItemsSource = szeleromuk;
        }

        private void Betöltés(string fileNev)
        {
            szeleromuk = new List<Szeleromu>();
            foreach (var item in File.ReadAllLines(fileNev, Encoding.UTF8))
            {
                szeleromuk.Add(new Szeleromu(item));
            }
            dataGridSzeleromuk.ItemsSource = szeleromuk;
        }

        private void btnEroEgyseg_Click(object sender, RoutedEventArgs e)
        {
            string helyszin = txtHelyszin.Text;
            var eroEgyseg = szeleromuk.FindAll(s => s.nev == helyszin);
            listBoxEroEgysegek.ItemsSource = eroEgyseg;
            lblOsszesEroEgyseg.Content = $"Összes egység: {eroEgyseg.Count}";
        }

        private void btnÖsszes_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Szélerőművek száma: {szeleromuk.Count}");
        }

        private void btnKategorizal_Click(object sender, RoutedEventArgs e)
        {
            var kategóriák = new List<char>();
            foreach (var item in szeleromuk)
            {
                char category = item.GetCategory();
                kategóriák.Add(category);
            }
            string message = string.Join(", ", kategóriák);
            MessageBox.Show(message);
        }

        private void btnAtlagTeljesitmeny_Click(object sender, RoutedEventArgs e)
        {
            double osszeg = 0;
            int darab = 0;
            foreach (var item in szeleromuk)
            {
                if (item.mukodesEv == 2010)
                {
                    osszeg += item.teljesitmeny;
                    darab++;
                }
            }
            MessageBox.Show($"Átlagos teljesítmény (2010): {osszeg / darab:F2} W");
        }

        private void btnMaxTeljesitmeny_Click(object sender, RoutedEventArgs e)
        {
            //Nem tudtam
        }

        private void btnJelentesGeneralasa_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder jelentés = new StringBuilder();
            foreach (var item in szeleromuk)
            {
                string kategória = item.GetCategory().ToString();
                jelentés.AppendLine($"{item.nev}, {item.szam}, {item.teljesitmeny}, {kategória}");
            }

            File.WriteAllText("jelentes.txt", jelentés.ToString());
            MessageBox.Show("Sikeres generálás a következő fileba: jelentes.txt");
        }
    }
}
