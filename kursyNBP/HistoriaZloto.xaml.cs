using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kursyNBP
{
    /// <summary>
    /// Logika interakcji dla klasy HistoriaWalut.xaml
    /// </summary>
    public partial class HistoriaZloto : Window
    {
        public HistoriaZloto()
        {
            InitializeComponent();
            dataOd.SelectedDate = DateTime.Now;
            dataDo.SelectedDate = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetGoldPriceHistory();
        }

        private void GetGoldPriceHistory()
        {
            tabelaDanych.Items.Clear();
            srednia.Document.Blocks.Clear();
            mediana.Document.Blocks.Clear();

            string? DataOd = dataOd.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
            string? DataDo = dataDo.SelectedDate.Value.Date.ToString("yyyy-MM-dd");

            HttpClient clientEuro = new();

            // Create new URL
            clientEuro.BaseAddress = new Uri($"http://api.nbp.pl/api/cenyzlota/{DataOd}/{DataDo}");
            clientEuro.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = clientEuro.GetAsync(clientEuro.BaseAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                var valueObject = response.Content.ReadAsAsync<IEnumerable<DaneWaluty>>().Result;
                foreach (var data in valueObject)
                {
                    tabelaDanych.Items.Add(data);
                }

                List<double?> daneZlota = new List<double?>();
                double? suma = 0;

                foreach(var data in valueObject)
                {
                    suma += data.Cena;
                    daneZlota.Add(data.Cena);
                }

                daneZlota.Sort();

                if (valueObject.Count() % 2 == 0)
                {
                    double? medianaParzysta = (daneZlota[(valueObject.Count() / 2) - 1] + daneZlota[(valueObject.Count() / 2)]) / 2;
                    mediana.AppendText(medianaParzysta.ToString());
                }else if (valueObject.Count() == 1)
                {
                    foreach (var data in valueObject)
                    {
                        mediana.AppendText(data.Cena.ToString());
                    }
                }
                else
                {
                    mediana.AppendText(daneZlota[(valueObject.Count() / 2) - 1].ToString());
                }

                srednia.AppendText((suma / valueObject.Count()).ToString());
            }
        }
    }
}
