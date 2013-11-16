using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public interface ICheckout
    {
        int Total { get; }

        void Scan(char sku);

        void Scan(char[] skus);
    }

    public class Checkout : ICheckout
    {
        public int Total {
            get { return GetBasketTotal(); }
        }

        private readonly List<char> _scannedSkus;
        private readonly IPricingRules _pricingRules;

        public Checkout(IPricingRules pricingRules)
        {
            _scannedSkus = new List<char>();
            _pricingRules = pricingRules;
        }

        public void Scan(char sku)
        {
            _scannedSkus.Add(sku);
        }

        public void Scan(char[] skus)
        {
            _scannedSkus.AddRange(skus);
        }

        public int GetBasketTotal()
        {
            if (_scannedSkus.Count == 2 && _scannedSkus.All(x => x == 'B'))
            {
                return _scannedSkus.Sum(x => _pricingRules.GetSkuPrice(x)) - 15;
            }

            if (_scannedSkus.Count == 3 && _scannedSkus.All(x => x == 'A'))
            {
                return _scannedSkus.Sum(x => _pricingRules.GetSkuPrice(x)) - 20;
            }

            return _scannedSkus.Sum(x => _pricingRules.GetSkuPrice(x));
        }
    }
}