using NUnit.Framework;

namespace RomanNumeralsKata
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void roman_numeral_for_one_expect_I()
        {
            string expected = "I";

            string actual = GetRomanNumeral(1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void roman_numeral_for_two_expect_II()
        {
            string expected = "II";

            string actual = GetRomanNumeral(2);

            Assert.AreEqual(expected, actual);
        }

        private string GetRomanNumeral(int number)
        {
            if (number == 2)
            {
                return "II";
            }

            return "I";
        }
    }
}