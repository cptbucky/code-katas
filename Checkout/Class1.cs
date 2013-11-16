using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class Class1
    {
        [TestCase('A', 50)]
        [TestCase('B', 30)]
        [TestCase('C', 20)]
        public void get_price_of_single_sku_should_equal_expected_price(char sku, int expectedPrice)
        {
            // act
            var actualPrice = Scan(sku);

            // assert
            Assert.AreEqual(expectedPrice, actualPrice);
        }

        private object Scan(char item)
        {
            if (item == 'C')
            {
                return 20;
            }

            if (item == 'B')
            {
                return 30;
            }

            return 50;
        }
    }
}