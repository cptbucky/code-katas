namespace Checkout
{
    using System.Collections.Generic;

    public interface ICheckout
    {
        int Total { get; }

        void Scan(char sku);
    }

    public class Checkout : ICheckout
    {
        public int Total
        {
            get
            {
                return this._pricingEngine.GetTotalPriceOfSkus(this._scannedSkus.ToArray());
            }
        }

        private readonly List<char> _scannedSkus;

        private readonly IPricingEngine _pricingEngine;

        public Checkout(IPricingEngine pricingEngine)
        {
            this._scannedSkus = new List<char>();
            this._pricingEngine = pricingEngine;
        }

        public void Scan(char sku)
        {
            this._scannedSkus.Add(sku);
        }
    }
}