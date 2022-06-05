using Accord.Statistics.Models.Regression.Linear;
using System;
using System.Collections.Generic;
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

namespace WeatherNETBrows
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string todayCurrencyapi = APIcallback.getCurrencyapi();
            string todayOpenexchangerates = APIcallback.getOpenexchangerates();
            string todayExchangerate = APIcallback.getExchangerate();
            string todayCurrencyfreaks = APIcallback.getCurrencyfreaks();
            Exchangerate.Content = "Exchangerate: " + todayExchangerate;
            Currencyfreaks.Content = "Currencyfreaks: " + todayCurrencyfreaks;
            Currencyapi.Content = "Currencyapi: " + todayCurrencyapi;
            Openexchangerates.Content = "Openexchangerates: " + todayOpenexchangerates;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string todayCurrencyapi = APIcallback.getCurrencyapi();
            string todayOpenexchangerates = APIcallback.getOpenexchangerates();
            string todayExchangerate = APIcallback.getExchangerate();
            string todayCurrencyfreaks = APIcallback.getCurrencyfreaks();
            Exchangerate.Content = "Exchangerate: " + todayExchangerate;
            Currencyfreaks.Content = "Currencyfreaks: " + todayCurrencyfreaks;
            Currencyapi.Content = "Currencyapi: " + todayCurrencyapi;
            Openexchangerates.Content = "Openexchangerates: " + todayOpenexchangerates;
        }
        private void Load_Plot(string todayCurrencyapi, string todayOpenexchangerates, string todayExchangerate, string todayCurrencyfreaks)
        {
            string[] regres1 = APIcallback.currencyapiDataForRegression();
            string[] regres2 = APIcallback.openexchangeratesDataForRegression();
            double[] y = new double[16];
            double[] x = new double[16];
            y[6] = Convert.ToDouble(todayCurrencyapi);
            y[13] = Convert.ToDouble(todayOpenexchangerates);
            y[14] = Convert.ToDouble(todayExchangerate);
            y[15] = Convert.ToDouble(todayCurrencyfreaks);
            x[6] = 7;
            x[13] = 7;
            x[14] = 7;
            x[15] = 7;
            for (int i = 0; i < 6; i++)
            {
                x[i] = i + 1;
                x[i + 7] = i + 1;
                y[i] = Convert.ToDouble(regres1[i]);
                y[i + 7] = Convert.ToDouble(regres2[i]);
            }
            var ls = new PolynomialLeastSquares()
            {
                Degree = 2
            };
            PolynomialRegression poly = ls.Learn(x, y);
            double[] res = new double[] { 1, 2, 3, 4, 5, 6, 7 };
            double[] pred = poly.Transform(res);
            RegressionPlot.Plot.AddScatter(res, pred);
            RegressionPlot.Refresh();
        }
    }
}
