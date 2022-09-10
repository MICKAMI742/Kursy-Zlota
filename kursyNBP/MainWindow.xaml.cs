using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursyNBP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetGoldPrice();
            /*GetEuroPrice();*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // inicjuję obiekt HistoriaWalut
            HistoriaWalut historiaWalut = new();

            // wyświetlam go jako okno dialogowe
            historiaWalut.ShowDialog();
        }

        private void GetGoldPrice()
        {
            DaneWaluty waluta = new();
            HttpClient clientZloto = new();

            // Create new URL
            clientZloto.BaseAddress = new Uri("http://api.nbp.pl/api/cenyzlota");
            clientZloto.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response
            HttpResponseMessage response = clientZloto.GetAsync(clientZloto.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                // Write data from API to the list
                var valueObject = response.Content.ReadAsAsync<IEnumerable<DaneWaluty>>().Result;
                foreach (var data in valueObject)
                {
                    zloto.AppendText(data.Cena.ToString());
                }
            }
        }

      /*  private void GetEuroPrice()
        {
            DaneWaluty waluta = new();
            HttpClient clientEuro = new();

            // Create new URL
            clientEuro.BaseAddress = new Uri("https://api.nbp.pl/api/exchangerates/rates/a/eur/");
            clientEuro.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response
            HttpResponseMessage response = clientEuro.GetAsync(clientEuro.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                // Write data from API to the list
                var valueObject = response.Content.ReadAsAsync<IEnumerable<DaneWaluty>>().Result;
                foreach (var data in valueObject)
                {
                    euro.AppendText(data.Cena.ToString());
                }
            }
        }*/
    }
}
