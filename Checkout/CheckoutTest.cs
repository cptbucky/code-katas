using System.Collections.Generic;
using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class CheckoutTest
    {
        private ICheckout _checkout;
        private PricingEngine _pricing;

        [TestFixtureSetUp]
        public void init()
        {
            _pricing = new PricingEngine(new Dictionary<char, ISkuPricingRule>
                {
                    {'A', new SkuPricingRule(50) { DiscountRule = new DiscountRule(3, 20)}},
                    {'B', new SkuPricingRule(30) { DiscountRule = new DiscountRule(2, 15)}},
                    {'C', new SkuPricingRule(20)},
                    {'D', new SkuPricingRule(15)},
                });
        }

        [SetUp]
        public void fixture_setup()
        { 
            _checkout = new Checkout(_pricing);
        }

        [TestCase(new[] { 'A' }, TestName = "1x A skus", Result = 50)]
        [TestCase(new[] { 'B' }, TestName = "1x B skus", Result = 30)]
        [TestCase(new[] { 'C' }, TestName = "1x C skus", Result = 20)]
        [TestCase(new[] { 'D' }, TestName = "1x D skus", Result = 15)]
        [TestCase(new[] { 'A', 'A' }, TestName = "2x A skus", Result = 100)]
        [TestCase(new[] { 'B', 'B' }, TestName = "2x B skus", Result = 45)]
        [TestCase(new[] { 'C', 'C' }, TestName = "2x C skus", Result = 40)]
        [TestCase(new[] { 'D', 'D' }, TestName = "2x D skus", Result = 30)]
        [TestCase(new[] { 'A', 'B' }, TestName = "1x A and 1x B skus, multiple types", Result = 80)]
        [TestCase(new[] { 'A', 'B', 'A' }, TestName = "2x A and 1x B skus, unordered", Result = 130)]
        [TestCase(new[] { 'A', 'A', 'A' }, TestName = "3x A skus, discount expected", Result = 130)]
        [TestCase(new[] { 'A', 'A', 'A', 'A', 'A', 'A' }, TestName = "6x A skus, discount expected", Result = 260)]
        [TestCase(new[] { 'A', 'A', 'A', 'B', 'B' }, TestName = "3x A and 2x B skus, discount expected", Result = 175)]
        [TestCase(new[] { 'A', 'A', 'A', 'B', 'B', 'A', 'A', 'A' }, TestName = "6x A and 2x B skus, discount expected", Result = 305)]
        public int get_price_of_multiple_skus(char[] skus)
        {
            // act
            foreach (var sku in skus)
            {
                _checkout.Scan(sku);
            }

            // assert
            return _checkout.Total;
        }

        [Test]
        public void incremental_total_check_for_multiple_skus()
        {
            _checkout.Scan('A');

            Assert.AreEqual(_checkout.Total, 50);

            _checkout.Scan('B');

            Assert.AreEqual(_checkout.Total, 80);

            _checkout.Scan('C');

            Assert.AreEqual(_checkout.Total, 100);

            _checkout.Scan('D');

            Assert.AreEqual(_checkout.Total, 115);

            _checkout.Scan('B');

            Assert.AreEqual(_checkout.Total, 130);

            _checkout.Scan('A');

            Assert.AreEqual(_checkout.Total, 180);

            _checkout.Scan('A');

            Assert.AreEqual(_checkout.Total, 210);
        }
    }
}