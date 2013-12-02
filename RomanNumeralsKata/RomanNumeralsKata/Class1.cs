using System;
using NUnit.Framework;

namespace RomanNumeralsKata
{
    [TestFixture]
    public class Class1
    {
        // test values 1 through 9
        // test values incrementing by 10, 10 through 90
        // test values incrementing by 100, 100 through 900
        // test values incrementing by 1000, 1000 through 3000

        [TestCase(1, Result = "I")]
        [TestCase(2, Result = "II")]
        [TestCase(3, Result = "III")]
        [TestCase(4, Result = "IV")]
        public string roman_numeral_for_single_digit(int arabic)
        {
            return GetRomanNumeral(arabic);
        }

        enum RomanNumerals
        {
            I = 1,
            IV = 4
        }

        private string GetRomanNumeral(int arabic)
        {
            string romanNumeral = string.Empty;

            var values = Enum.GetValues(typeof(RomanNumerals));

            for (int i = values.Length - 1; i >= 0; i--)
            {
                var value = (int)values.GetValue(i);

                while (arabic >= value)
                {
                    romanNumeral += Enum.GetName(typeof(RomanNumerals), value);
                    arabic -= value;
                }
            }

            return romanNumeral;
        }
    }
}