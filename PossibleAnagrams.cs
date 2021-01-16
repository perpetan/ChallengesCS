using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public static class Program
{
    private const string FileName = @"PopularWordsEnglish.TXT";

    public static void Main()
    {
        //Change above to test with other strings
        string inputString = "earth";

        StreamReader fileReader;
        try
        {
            fileReader = new StreamReader(FileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        using (fileReader)
        {
            IEnumerable<string> wordsDictionary = GetStrings(fileReader);
            IEnumerable<string> matches = FindAllMatches(wordsDictionary, inputString);
            
            Console.WriteLine(matches.Count() + " anagrams found for word " + inputString + ":");

            foreach (var match in matches)
            {
                Console.WriteLine(match);
            }
        }

        // Wait for keypress so console window doesn't close.
        Console.ReadKey();
    }

    public static IEnumerable<string> FindAllMatches(IEnumerable<string> wordsDictionary, string inputString)
    {
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        HashSet<string> matches = new HashSet<string>(StringComparer.Create(cultureInfo, true));

        foreach (string testString in wordsDictionary)
        {
            if (string.IsNullOrWhiteSpace(testString))
            {
                // Never consider an empty string or whitespace-only string to be a match, irrespective
                // of the strategy.
                continue;
            }

            if (IsAnagram(inputString, testString))
            {
                if (!matches.Contains(testString))
                {
                    matches.Add(testString);
                }
            }
        }

        return matches;
    }

    public static bool IsAnagram(string inputString, string word)
    {
        inputString = NormalizeString(inputString);
        word = NormalizeString(word);

        //First, test if the word found in the dictionary is not equal the input string
        //Then, order each character of both strings and compare this ordered strings
        //For example: 'race' will become 'acer' and 'care' will also become 'acer'.
        //So, both ordered strings are equal, which means they are anagrams
        return !inputString.Equals(word) 
                && string.Concat(inputString.OrderBy(character => character))
                        .Equals(string.Concat(word.OrderBy(character => character)));
    }

    // We use an IEnumerable rather than an array, since this doesn't require that
    // we load the entire file into memory at once.
    private static IEnumerable<string> GetStrings(TextReader textReader)
    {
        string nextLine = textReader.ReadLine();

        while (nextLine != null)
        {
            if (!string.IsNullOrWhiteSpace(nextLine))
            {
                yield return nextLine;
            }

            nextLine = textReader.ReadLine();
        }
    }

    public static string NormalizeString(string expression)
    {
        //Remove white spaces
        expression = Regex.Replace(expression, @"\s", "");
        
        //Turns all characters into lower. 
        //Then, Part and Trap will be anagrams when compared
        expression = expression.ToLower();

        return expression;
    }
}
