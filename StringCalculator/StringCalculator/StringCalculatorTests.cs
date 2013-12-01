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

        [TestCase("//;\n1;2;1")]
        [TestCase("//,\n1,2,1")]
        [TestCase("//|\n1|2|1")]
        public void provide_delimeter_add_three_numbers_should_return_sum(string input)
        {
            int expected = 4;
            int actual = Add(input);

            Assert.AreEqual(expected, actual);
        }

        private static int Add(string numbers)
        {
            if (numbers == string.Empty)
            {
                return 0;
            }

            var delimeter = GetDelimeterFromString(numbers);

            var stringToProcess = ExtractStringOfNumbers(numbers, delimeter);

            var singles = GetIntArray(stringToProcess, delimeter);

            return singles.Sum();
        }

        private static string ExtractStringOfNumbers(string numbers, char delimeter)
        {
            string stringToProcess = numbers;

            if (numbers.StartsWith("//"))
            {
                stringToProcess = numbers.Split('\n')[1];
            }

            return stringToProcess.Replace('\n', delimeter).Replace(',', delimeter);
        }

        private static char GetDelimeterFromString(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                return Convert.ToChar(numbers.Split('\n')[0].Replace("//", string.Empty));
            }

            return '|';
        }

        private static int[] GetIntArray(string numbers, char delimeter)
        {
            int[] singles = numbers.Split(delimeter).Select(x => Convert.ToInt32(x)).ToArray();

            return singles;
        }
    }
}