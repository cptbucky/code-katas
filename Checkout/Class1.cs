using NUnit.Framework;

namespace Checkout
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void get_price_of_single_A()
        {
            // arrange
            var expected = 50;
            var item = 'A';

            // act
            var actual = Scan(item);

            // assert
            Assert.AreEqual(expected, actual);
        }

        private object Scan(char item)
        {
            return 50;
        }
    }
}