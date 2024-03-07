namespace CriptoTracker.Entities
{
    public class CryptoInfo
    {
        public string SymbolId { get; set; }
        public double LastPrice { get; set; }
        public double PeriodHigh { get; set; }
        public double PeriodLow { get; set; }
        public double PriceChange24h { get; set; }
        public double PriceChangePercentage24h { get; set; }
    }
}
