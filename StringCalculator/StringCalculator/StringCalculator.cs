using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        private const char SectionSplitter = '\n';

        public static int Add(string numbers)
        {
            if (numbers == string.Empty)
            {
                return 0;
            }

            string[] delimiters = GetDelimeters(numbers);

            var stringToProcess = ExtractNumberString(numbers);

            var singles = GetIntArray(stringToProcess, delimiters);

            return singles.Sum();
        }

        private static string[] GetDelimeters(string numbers)
        {
            if (StringContainsDelimeter(numbers))
            {
                var val = numbers.Split(SectionSplitter)[0].Replace("//", string.Empty);

                var delimRegEx = new Regex(@"\[(.*)\]");

                var delims = new List<string>();

                foreach (Match match in delimRegEx.Matches(val))
                {
                    delims.Add(match.Groups[1].Value);
                }

                return delims.ToArray();
            }
            
            return new string[]{ "|" };
        }

        private static string ExtractNumberString(string numbers)
        {
            var delimitedBase = numbers.Split(SectionSplitter);

            var delimiter = "|";

            return numbers.StartsWith("//") && delimitedBase.Length == 2 ? delimitedBase[1] : numbers.Replace("\n", delimiter).Replace(",", delimiter);

            //string stringToProcess = numbers;

            //if (StringContainsDelimeter(numbers))
            //{
            //    stringToProcess = numbers.Split(SectionSplitter)[1];
            //}

            //return stringToProcess.Replace("\n", delimeter).Replace(",", delimeter);
        }

        private static IEnumerable<int> GetIntArray(string numbers, string[] delimeters)
        {
            var singles = new List<int>();
            var negatives = new List<int>();

            foreach (var stringInt in numbers.Split(delimeters, StringSplitOptions.None))
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

        private static bool StringContainsDelimeter(string numbers)
        {
            return numbers.StartsWith("//[") && numbers.Split(SectionSplitter)[0].EndsWith("]");
        }
    }
}