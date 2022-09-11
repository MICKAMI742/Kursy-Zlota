using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public partial class HistoriaEuro : Window
    {
        public HistoriaEuro()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetEuroPriceHistory();
        }

        private void GetEuroPriceHistory()
        {
            tabelaDanych.Items.Clear();
            string? DataOd = dataOd.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
            string? DataDo = dataDo.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
            HttpClient clientEuro = new();

            // Create new URL
            clientEuro.BaseAddress = new Uri($"https://api.nbp.pl/api/exchangerates/rates/c/eur/{DataOd}/{DataDo}");
            clientEuro.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = clientEuro.GetAsync(clientEuro.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var valueObject = response.Content.ReadAsAsync<TablicaDanychEuro>().Result;
                foreach(var data in valueObject.rates)
                {
                    tabelaDanych.Items.Add(data);
                }

                double? suma = 0;

                foreach(var data in valueObject.rates)
                {
                    suma += data.ask;
                }
                srednia.AppendText((suma / valueObject.rates.Length).ToString());
            }
        }
    }
}
