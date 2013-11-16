using System.Collections.Generic;

namespace Checkout
{
    public interface ICheckout
    {
        int Total { get; }

        void Scan(char sku);
    }

    public class Checkout : ICheckout
    {
        public int Total {
            get { return _pricingEngine.GetTotalPriceOfSkus(_scannedSkus.ToArray()); }
        }

        private readonly List<char> _scannedSkus;
        private readonly IPricingEngine _pricingEngine;

        public Checkout(IPricingEngine pricingEngine)
        {
            _scannedSkus = new List<char>();
            _pricingEngine = pricingEngine;
        }

        public void Scan(char sku)
        {
            _scannedSkus.Add(sku);
        }
    }
}