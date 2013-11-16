using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class Class1
    {
        private ISkuFactory _skuFactory;

        [SetUp]
        public void fixture_setup()
        {
            _skuFactory = new SkuFactory();
        }

        [TestCase('A', Result = 50)]
        [TestCase('B', Result = 30)]
        [TestCase('C', Result = 20)]
        [TestCase('D', Result = 15)]
        public int get_price_of_single_sku_should_equal_expected_price(char sku)
        {
            // act
            _skuFactory.Scan(sku);

            // assert
            return _skuFactory.GetBasketTotal();
        }

        [TestCase(new char[] { 'A', 'A' }, TestName = "2x A skus", Result = 100)]
        [TestCase(new char[] { 'B', 'B' }, TestName = "2x B skus", Result = 60)]
        [TestCase(new char[] { 'C', 'C' }, TestName = "2x C skus", Result = 40)]
        [TestCase(new char[] { 'D', 'D' }, TestName = "2x D skus", Result = 30)]
        public int get_price_of_2_skus_should_equal_twice_the_price(char[] skus)
        {
            // act
            _skuFactory.Scan(skus);

            // assert
            return _skuFactory.GetBasketTotal();
        }
    }

    public class SkuFactory : ISkuFactory
    {
        private readonly List<char> _scannedSkus;

        public SkuFactory()
        {
            _scannedSkus = new List<char>();
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
            return _scannedSkus.Sum(x => GetSkuPrice(x));
        }

        private static int GetSkuPrice(char sku)
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

    public interface ISkuFactory
    {
        void Scan(char sku);

        int GetBasketTotal();

        void Scan(char[] skus);
    }
}