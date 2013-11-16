using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class Class1
    {
        [SetUp]
        public void fixture_setup()
        {
            _checkout = new Checkout(new PricingRules());
        }

        private ICheckout _checkout;

        [TestCase('A', Result = 50)]
        [TestCase('B', Result = 30)]
        [TestCase('C', Result = 20)]
        [TestCase('D', Result = 15)]
        public int get_price_of_single_sku_should_equal_expected_price(char sku)
        {
            // act
            _checkout.Scan(sku);

            // assert
            return _checkout.Total;
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
            _checkout.Scan(skus);

            // assert
            return _checkout.Total;
        }
    }
}