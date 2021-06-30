using RestSharp;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Linq;
using System;


namespace ApiTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Crypto Tickers seperated by Commas: ");
            string CryptoVals = Console.ReadLine();

            string[] Crypto = CryptoVals.Split(',');
            string Fiat = "USD";

            decimal? cryptoInfo = getSingleCryptoPrice(Crypto[0], Fiat);

            Console.WriteLine(Crypto[0] + ": $" + cryptoInfo +" " + Fiat);

            Console.WriteLine("\n\nPress Enter to Continue");
            Console.ReadKey();
        }


        static decimal? getSingleCryptoPrice(string cryptoTicker, string CurrencyTicker)
        {
            string webstring = "https://min-api.cryptocompare.com/data/price?fsym=";

            webstring += cryptoTicker + "&tsyms=" + CurrencyTicker;

            var client = new RestClient(webstring);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            CryptoRead readCrypto = JsonSerializer.DeserializeFromString<CryptoRead>(response.Content);

            if(CurrencyTicker == "JPY")
                return readCrypto.JPY;
            if(CurrencyTicker == "USD")
                return readCrypto.USD;
            if (CurrencyTicker == "EUR")
                return readCrypto.EUR;
            if (CurrencyTicker == "CNY")
                return readCrypto.CNY;
            if (CurrencyTicker == "KRW")
                return readCrypto.KRW;
            if (CurrencyTicker == "INR")
                return readCrypto.INR;
            if (CurrencyTicker == "CAD")
                return readCrypto.CAD;
            if (CurrencyTicker == "HKD")
                return readCrypto.HKD;
            if (CurrencyTicker == "AUD")
                return readCrypto.AUD;
            else
                return null;
        }   
    }

    public class CryptoRead
    {
        public decimal USD { get; set; }
        public decimal JPY { get; set; }
        public decimal EUR { get; set; }
        public decimal CNY { get; set; }
        public decimal KRW { get; set; }
        public decimal INR { get; set; }
        public decimal CAD { get; set; }
        public decimal HKD { get; set; }
        public decimal AUD { get; set; }
    }

}

