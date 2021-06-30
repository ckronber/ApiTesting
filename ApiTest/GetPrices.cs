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

            string cryptoInfo = getCryptoPrice(Crypto,"USD");

            Console.WriteLine(cryptoInfo);

            CryptoRead readCrypto = JsonSerializer.DeserializeFromString<CryptoRead>(cryptoInfo);

            Console.WriteLine(readCrypto.CryptoName);
            Console.WriteLine(readCrypto.CryptoPrice);

            Console.WriteLine("\n\nPress Enter to Continue");
            Console.ReadKey();
        }

        static string getCryptoPrice(IEnumerable<string> cryptoTicker, string CurrencyTicker)
        {
            string cryptoString = "";
            string webstring = "https://min-api.cryptocompare.com/data/pricemulti?fsyms=";
            int i = 0;

            foreach (string crypto in cryptoTicker)
            {
                i++;
                if (i < cryptoTicker.Count())
                    cryptoString += crypto + ",";
                else
                    cryptoString += crypto;
            }

            cryptoString += "&tsyms=" + CurrencyTicker;

            var client = new RestClient(webstring+cryptoString);

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }

    public class CryptoRead
    {
        public string CryptoName { get; set; }
        public Dictionary<string,decimal> CryptoPrice { get; set; }
    }
    
}

