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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetGoldPriceHistory();
        }

        private void GetGoldPriceHistory()
        {
            tabelaDanych.Items.Clear();
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
            }
        }
    }
}
