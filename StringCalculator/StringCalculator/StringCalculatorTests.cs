using System;
using System.Linq;
using NUnit.Framework;

namespace StringCalculator
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        public void add_empty_string_should_return_zero()
        {
            int expected = 0;
            int actual = Add("");

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        public void add_single_number_should_return_single_number(string input, int expected)
        {
            int actual = Add(input);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1,2", 3)]
        [TestCase("2,4", 6)]
        [TestCase("57,35", 92)]
        public void add_two_numbers_should_return_sum(string input, int expected)
        {
            int actual = Add(input);

            Assert.AreEqual(expected, actual);
        }

        private int Add(string numbers)
        {
            if (numbers == string.Empty)
            {
                return 0;
            }

            int[] singles = numbers.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            int sum = 0;

            for (int i = 0; i < singles.Length; i++)
            {
                sum += singles[i];
            }

            return sum;
        }
    }
}