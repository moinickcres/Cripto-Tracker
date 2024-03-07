using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using CriptoTracker.Entities;

namespace CriptoTracker.Models
{
    public class Tracker
    {
        public static async Task<decimal> GetPriceChange(string crypto)
        {
            string cryptocurrency = crypto;
            string currency = "usd";

            // Obtiene el precio actual de la criptomoneda
            decimal currentPrice = await GetCurrentPrice(cryptocurrency, currency);

            // Obtiene el precio de la criptomoneda hace 24 horas
            decimal historicalPrice = await GetHistoricalPrice(cryptocurrency, currency, DateTime.UtcNow.AddDays(-1));

            // Calcula el cambio de precio en las últimas 24 horas
            decimal priceChange = currentPrice - historicalPrice;

            // Imprime los resultados
            Console.WriteLine($"Precio Actual de {cryptocurrency.ToUpper()}: {currentPrice} {currency.ToUpper()}");
            Console.WriteLine($"Precio hace 24 horas: {historicalPrice} {currency.ToUpper()}");
            Console.WriteLine($"Cambio de Precio en las últimas 24 horas: {priceChange} {currency.ToUpper()}");

            return priceChange;
        }

        private static async Task<decimal> GetCurrentPrice(string cryptocurrency, string currency)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://api.coingecko.com/api/v3/simple/price?ids={cryptocurrency}&vs_currencies={currency}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsAsync<dynamic>();
                return data[cryptocurrency][currency];
            }
        }

        private static async Task<decimal> GetHistoricalPrice(string cryptocurrency, string currency, DateTime date)
        {
            using (var client = new HttpClient())
            {
                string dateString = date.ToString("dd-MM-yyyy");
                string url = $"https://api.coingecko.com/api/v3/coins/{cryptocurrency}/history?date={dateString}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsAsync<dynamic>();
                return data["market_data"]["current_price"][currency];
            }
        }
    }
}
