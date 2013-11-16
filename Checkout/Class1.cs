using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class Class1
    {
        [TestCase('A', 50, Result = 50)]
        [TestCase('B', 30, Result = 30)]
        [TestCase('C', 20, Result = 20)]
        [TestCase('D', 15, Result = 15)]
        public int get_price_of_single_sku_should_equal_expected_price(char sku, int expectedPrice)
        {
            // act
            var actualPrice = Scan(sku);

            // assert
            return actualPrice;
        }

        private int Scan(char item)
        {
            if (item == 'D')
            {
                return 15;
            }

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