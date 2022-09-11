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
        public string waluta = "";
        public MainWindow()
        {
            InitializeComponent();
            GetGoldPrice();
            GetEuroPrice();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(waluta == "zloto")
            {
                // Iniciation of HistoriaWalut object
                HistoriaZloto historiaZloto = new();

                // Creating the view historiaWalut
                historiaZloto.ShowDialog();
            }else if(waluta == "euro")
            {
                // Iniciation of HistoriaWalut object
                HistoriaEuro historiaEuro = new();

                // Creating the view historiaWalut
                historiaEuro.ShowDialog();
            }
        }

        private void GetGoldPrice()
        {
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

        private void zlotoRadio_Checked(object sender, RoutedEventArgs e)
        {
            waluta = "zloto";
        }

        private void euroRadio_Checked(object sender, RoutedEventArgs e)
        {
            waluta = "euro";
        }

        private void GetEuroPrice()
        {
            HttpClient clientEuro = new();

            // Create new URL
            clientEuro.BaseAddress = new Uri("https://api.nbp.pl/api/exchangerates/rates/c/eur/");
            clientEuro.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response
            HttpResponseMessage response = clientEuro.GetAsync(clientEuro.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                // Write data from API to the list
                var valueObject = response.Content.ReadAsAsync<TablicaDanychEuro>().Result;
                foreach (var data in valueObject.rates)
                {
                    euro.AppendText(data.ask.ToString());
                }
            }
        }
    }
}
