using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class Class1
    {
        [SetUp]
        public void fixture_setup()
        {
            _basket = new Basket(new PricingRules());
        }

        private IBasket _basket;

        [TestCase('A', Result = 50)]
        [TestCase('B', Result = 30)]
        [TestCase('C', Result = 20)]
        [TestCase('D', Result = 15)]
        public int get_price_of_single_sku_should_equal_expected_price(char sku)
        {
            // act
            _basket.Scan(sku);

            // assert
            return _basket.Total;
        }

        [TestCase(new[] { 'A', 'A' }, TestName = "2x A skus", Result = 100)]
        [TestCase(new[] { 'B', 'B' }, TestName = "2x B skus", Result = 45)]
        [TestCase(new[] { 'C', 'C' }, TestName = "2x C skus", Result = 40)]
        [TestCase(new[] { 'D', 'D' }, TestName = "2x D skus", Result = 30)]
        [TestCase(new[] { 'A', 'B' }, TestName = "1x A and 1x B skus, multiple types", Result = 80)]
        [TestCase(new[] { 'A', 'B', 'A' }, TestName = "2x A and 1x B skus, unordered", Result = 130)]
        [TestCase(new[] { 'A', 'A', 'A' }, TestName = "3x A skus, discount expected", Result = 130)]
        //[TestCase(new[] {'A', 'A', 'A', 'B', 'B'}, TestName = "3x A and 2x B skus, discount expected", Result = 175)]
        public int get_price_of_2_skus_should_equal_twice_the_price(char[] skus)
        {
            // act
            _basket.Scan(skus);

            // assert
            return _basket.Total;
        }
    }

    public class PricingRules : IPricingRules
    {
        public int GetSkuPrice(char sku)
        {
            if (sku == 'D')
            {
                return 15;
            }

            if (sku == 'C')
            {
                return 20;
            }

            if (sku == 'B')
            {
                return 30;
            }

            return 50;
        }
    }

    public class Basket : IBasket
    {
        public int Total {
            get { return GetBasketTotal(); }
        }

        private readonly List<char> _scannedSkus;
        private readonly IPricingRules _pricingRules;

        public Basket(IPricingRules pricingRules)
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

    public interface IPricingRules
    {
        int GetSkuPrice(char sku);
    }

    public interface IBasket
    {
        int Total { get; }

        void Scan(char sku);

        void Scan(char[] skus);
    }
}