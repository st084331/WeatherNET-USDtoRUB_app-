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

namespace WeatherNETwin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string todayCurrencyapi = APIcallback.getCurrencyapi();
            string todayOpenexchangerates = APIcallback.getOpenexchangerates();;
            Exchangerate.Content = "Exchangerate: " + APIcallback.getExchangerate();
            Currencyfreaks.Content = "Currencyfreaks: " + APIcallback.getCurrencyfreaks();
            Currencyapi.Content = "Currencyapi: " + todayCurrencyapi;
            Openexchangerates.Content = "Openexchangerates: " + todayOpenexchangerates;
            Load_Plot(todayCurrencyapi, todayOpenexchangerates);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Exchangerate.Content = "Exchangerate: " + APIcallback.getExchangerate();
            Currencyfreaks.Content = "Currencyfreaks: " + APIcallback.getCurrencyfreaks();
            Currencyapi.Content = "Currencyapi: " + APIcallback.getCurrencyapi();
            Openexchangerates.Content = "Openexchangerates: " + APIcallback.getOpenexchangerates();
        }

        private void Load_Plot(string todayCurrencyapi, string todayOpenexchangerates)
        {
            string[] regres1 = APIcallback.currencyapiDataForRegression();
            string[] regres2 = APIcallback.openexchangeratesDataForRegression();
            double[] y = new double[14];
            double[] x = new double[14];
            y[0] = Convert.ToDouble(todayCurrencyapi);
            y[7] = Convert.ToDouble(todayOpenexchangerates);
            x[0] = 1;
            x[7] = 1;
            for (int i = 0; i < 6; i++)
            {
                x[i + 1] = i + 2;
                x[i + 8] = i + 2;
                y[i + 1] = Convert.ToDouble(regres1[i]);
                y[i + 8] = Convert.ToDouble(regres2[i]);
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
