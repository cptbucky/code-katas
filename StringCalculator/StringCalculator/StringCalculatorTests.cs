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

        [Test]
        public void add_seven_numbers_should_return_sum()
        {
            int expected = 25;
            int actual = Add("1,2,3,4,4,5,6");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_four_numbers_should_return_sum()
        {
            int expected = 14;
            int actual = Add("1,2,5,6");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_two_numbers_delimeted_by_newline_should_return_sum()
        {
            int expected = 3;
            int actual = Add("1\n2");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_two_numbers_delimeted_by_newline_and_comma_should_return_sum()
        {
            int expected = 4;
            int actual = Add("1\n2,1");

            Assert.AreEqual(expected, actual);
        }

        private int Add(string numbers)
        {
            if (numbers == string.Empty)
            {
                return 0;
            }

            var singles = GetIntArray(numbers);

            return singles.Sum();
        }

        private static int[] GetIntArray(string numbers)
        {
            var delimeter = '|';

            var singleDelimeted = numbers.Replace('\n', delimeter).Replace(',', delimeter);

            //var delimeter = numbers.Contains("\n") ? '\n' : ',';

            int[] singles = singleDelimeted.Split(delimeter).Select(x => Convert.ToInt32(x)).ToArray();
            return singles;
        }
    }
}