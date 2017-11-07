using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Asif.OASYSTEMS\AppData\Local\Temporary Projects\ConsoleApplication1\Files\wordlist.txt");

            var sortedLines = lines.OrderByDescending(word => word.Length).ToList();
            string longestWord = sortedLines.FirstOrDefault(word => hasWords(word, sortedLines));

            Console.WriteLine(longestWord);
            sortedLines.Remove(longestWord);

            string secondLongestWord = sortedLines.FirstOrDefault(word => hasWords(word, sortedLines));

            Console.WriteLine(secondLongestWord);

            Console.Read();


        }
        private static bool hasWords(string word, ICollection<string> dict)
        {
            if (String.IsNullOrEmpty(word)) return false;
            return word.Length == 1
                ? dict.Contains(word)
                : checkCombination(word).Where(pair => dict.Contains(pair.Item1))
                                     .Select(pair => dict.Contains(pair.Item2) || hasWords(pair.Item2, dict))
                                     .FirstOrDefault();
        }

        private static List<Tuple<string, string>> checkCombination(string word)
        {
            var output = new List<Tuple<string, string>>();
            for (var i = 1; i < word.Length; i++)
            {
                output.Add(Tuple.Create(word.Substring(0, i), word.Substring(i)));
            }
            return output;
        }
    }
}
