using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        public static int Add(string numbers)
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

        private static string ExtractStringOfNumbers(string numbers, string delimeter)
        {
            string stringToProcess = numbers;

            if (numbers.StartsWith("//"))
            {
                stringToProcess = numbers.Split('\n')[1];
            }

            return stringToProcess.Replace("\n", delimeter).Replace(",", delimeter);
        }

        private static string GetDelimeterFromString(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                return numbers.Split('\n')[0].Replace("//", string.Empty);
            }

            return "|";
        }

        private static IEnumerable<int> GetIntArray(string numbers, string delimeter)
        {
            var singles = new List<int>();
            var negatives = new List<int>();
            
            foreach (var stringInt in numbers.Split(new string[] { delimeter }, StringSplitOptions.None))
            {
                var converted = Convert.ToInt32(stringInt);

                if (converted < 0)
                {
                    negatives.Add(converted);
                }
                else if (converted <= 1000)
                {
                    singles.Add(converted);
                }
            }

            if (negatives.Count > 0)
            {
                throw new Exception(string.Format("negatives not allowed ({0})", string.Join(",", negatives)));
            }

            return singles.ToArray();
        }
    }
}