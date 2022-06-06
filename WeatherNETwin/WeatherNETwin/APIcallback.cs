using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherNETwin
{
    public static class APIcallback
    {
        private static string exchangerateAPI = "https://v6.exchangerate-api.com/v6/aaa255ecc18c08444653828f/latest/USD";
        private static string currencyfreaksAPI = "https://api.currencyfreaks.com/latest?apikey=f751f83125c44cd385171bcfae437c79";
        private static string currencyapiAPI = "https://api.currencyapi.com/v3/latest?apikey=FVjRXEjsOd5uJPwWfo6a6EXvmDJGdhu19YFynWoM";
        private static string openexchangeratesAPI = "https://openexchangerates.org/api/latest.json?app_id=ba9500c975284e3a90cfe80e3cf27a21&base=USD";

        private static string getInfoByAPI(string url)
        {
            string CorrectUrl = String.Format(url);
            WebRequest requestObj = WebRequest.Create(CorrectUrl);
            requestObj.Method = "GET";
            HttpWebResponse responseObj = null;
            responseObj = (HttpWebResponse)requestObj.GetResponse();
            string result = null;
            using (Stream stream = responseObj.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
                reader.Close();
            }
            return result;
        }
         
        public static string[] currencyapiDataForRegression()
        {
            string[] ratesPerDay = new string[6];
            for(int i = 5; i >= 0; i--)
            {
                string apiURL = "https://api.currencyapi.com/v3/historical?apikey=FVjRXEjsOd5uJPwWfo6a6EXvmDJGdhu19YFynWoM&currencies=RUB&date=" + DateTime.Now.AddDays(-(i+1)).ToString("yyyy/MM/dd").Replace('.', '-');
                string jsonFormat = getInfoByAPI(apiURL);
                JObject obj = JObject.Parse(jsonFormat);
                ratesPerDay[i] = obj["data"]["RUB"]["value"].ToString().Replace('.', ',');
            }
            return ratesPerDay;
        }

        public static string[] openexchangeratesDataForRegression()
        {
            string[] ratesPerDay = new string[6];
            for (int i = 5; i >= 0; i--)
            {
                string apiURL = "https://openexchangerates.org/api/historical/"+ DateTime.Now.AddDays(-(i + 1)).ToString("yyyy/MM/dd").Replace('.', '-') + ".json?app_id=ba9500c975284e3a90cfe80e3cf27a21";
                string jsonFormat = getInfoByAPI(apiURL);
                JObject obj = JObject.Parse(jsonFormat);
                ratesPerDay[i] = obj["rates"]["RUB"].ToString().Replace('.', ','); ;
            }
            return ratesPerDay;
        }

        public static string getExchangerate()
        {
            string jsonFormat = getInfoByAPI(exchangerateAPI);
            JObject obj = JObject.Parse(jsonFormat);
            return obj["conversion_rates"]["RUB"].ToString().Replace('.', ','); ;
        }

        public static string getOpenexchangerates()
        {
            string jsonFormat = getInfoByAPI(openexchangeratesAPI);
            JObject obj = JObject.Parse(jsonFormat);
            return obj["rates"]["RUB"].ToString().Replace('.', ','); ;
        }

        public static string getCurrencyapi()
        {
            string jsonFormat = getInfoByAPI(currencyapiAPI);
            JObject obj = JObject.Parse(jsonFormat);
            return obj["data"]["RUB"]["value"].ToString().Replace('.', ','); ;
        }

        public static string getCurrencyfreaks()
        {
            string jsonFormat = getInfoByAPI(currencyfreaksAPI);
            JObject obj = JObject.Parse(jsonFormat);
            return obj["rates"]["RUB"].ToString().Replace('.', ','); ;
        }
    }
}
