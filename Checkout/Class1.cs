using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void get_price_of_single_A_should_equal_50()
        {
            // arrange
            var expected = 50;
            var item = 'A';

            // act
            var actual = Scan(item);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void get_price_of_single_B_should_equal_30()
        {
            // arrange
            var expected = 30;
            var item = 'B';

            // act
            var actual = Scan(item);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void get_price_of_single_C_should_equal_20()
        {
            // arrange
            var expected = 20;
            var item = 'C';

            // act
            var actual = Scan(item);

            // assert
            Assert.AreEqual(expected, actual);
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