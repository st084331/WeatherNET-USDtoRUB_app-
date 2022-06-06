using NUnit.Framework;
using WeatherNETwin;

namespace WeatherNET_Tests
{
    public class APIcallbackTest
    {

        [Test]
        public void getCurrencyfreaksTest()
        {
            Assert.AreNotEqual(null, APIcallback.getCurrencyfreaks());
        }

        [Test]
        public void getExchangerateTest()
        {
            Assert.AreNotEqual(null, APIcallback.getExchangerate());
        }

        [Test]
        public void getOpenexchangeratesTest()
        {
            Assert.AreNotEqual(null, APIcallback.getOpenexchangerates());
        }

        [Test]
        public void getCurrencyapiTest()
        {
            Assert.AreNotEqual(null, APIcallback.getCurrencyapi());
        }

        [Test]
        public void currencyapiDataForRegressionTest()
        {
            Assert.AreNotEqual(null, APIcallback.currencyapiDataForRegression());
        }

        [Test]
        public void openexchangeratesDataForRegressionTest()
        {
            Assert.AreNotEqual(null, APIcallback.openexchangeratesDataForRegression());
        }
    }
}