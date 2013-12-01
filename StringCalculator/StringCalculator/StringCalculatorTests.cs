using System;
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
            int actual = StringCalculator.Add("");

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        public void add_single_number_should_return_single_number(string input, int expected)
        {
            int actual = StringCalculator.Add(input);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("1,2", 3)]
        [TestCase("2,4", 6)]
        [TestCase("57,35", 92)]
        public void add_two_numbers_should_return_sum(string input, int expected)
        {
            int actual = StringCalculator.Add(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_seven_numbers_should_return_sum()
        {
            int expected = 25;
            int actual = StringCalculator.Add("1,2,3,4,4,5,6");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_four_numbers_should_return_sum()
        {
            int expected = 14;
            int actual = StringCalculator.Add("1,2,5,6");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_two_numbers_delimeted_by_newline_should_return_sum()
        {
            int expected = 3;
            int actual = StringCalculator.Add("1\n2");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_two_numbers_delimeted_by_newline_and_comma_should_return_sum()
        {
            int expected = 4;
            int actual = StringCalculator.Add("1\n2,1");

            Assert.AreEqual(expected, actual);
        }

        [TestCase("//[;]\n1;2;1")]
        [TestCase("//[,]\n1,2,1")]
        [TestCase("//[|]\n1|2|1")]
        public void provide_delimeter_add_three_numbers_should_return_sum(string input)
        {
            int expected = 4;
            int actual = StringCalculator.Add(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_a_negative_number_should_throw_exception_and_list_in_message()
        {
            string expected = "negatives not allowed (-1)";

            var exception = Assert.Throws<Exception>(() => StringCalculator.Add("-1"));
            var actual = exception.Message;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_negative_numbers_should_throw_exception_and_list_in_message()
        {
            string expected = "negatives not allowed (-2,-6,-9)";

            var exception = Assert.Throws<Exception>(() => StringCalculator.Add("-2,-6,-9"));
            var actual = exception.Message;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void add_mixed_numbers_should_throw_exception_and_list_in_message()
        {
            string expected = "negatives not allowed (-2,-9)";

            var exception = Assert.Throws<Exception>(() => StringCalculator.Add("-2,-9"));
            var actual = exception.Message;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void include_number_greater_than_one_thousand_expect_them_to_be_ignored()
        {
            int expected = 50;
            int actual = StringCalculator.Add("50,1001");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void delimeter_of_any_length()
        {
            int expected = 6;
            int actual = StringCalculator.Add("//[***]\n1***2***3");

            Assert.AreEqual(expected, actual);
        }
    }
}