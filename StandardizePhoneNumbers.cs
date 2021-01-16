/*
 * Standardize phone numbers
 * Given a string with numbers, print with pattern as phone number
 * For example, splitEach = 3: XXX-XXX-XXX or XXX-XXX-XX-XX
 * Never XXX-XXX-XXX-X (single number at the end)
 */

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        const string test = "8811239984222";
        const string separator = "-";
        const int splitEach = 3; // Change to split string with separator each N number of characters

        Console.WriteLine(StandardizePhoneNumber(test, separator, splitEach));
    }

    public static string StandardizePhoneNumber(string phoneNumber, string separator, int splitEach)
    {
        phoneNumber = NormalizeString(phoneNumber);
        StringBuilder newPhoneNumber = new StringBuilder();

        // If the last number would be alone after separator, do the IF Statement above to give XX-XX pattern to the end of the string
        if ((phoneNumber.Length - (splitEach+1)) % splitEach == 0)
        {
            newPhoneNumber.Append(Regex.Replace(phoneNumber[0..^(splitEach+1)], 
                                                ".{" + splitEach + "}", //Split each $splitEach
                                                "$0" + separator)); // $0 it's the character itself. After that, it comes the separator

            newPhoneNumber.Append(Regex.Replace(phoneNumber[^(splitEach+1)..], 
                                                ".{" + (splitEach - 1) + "}(?!$)", //This (?!$)  uses a negative lookahead to ensure it does not match the characters at the end of the string
                                                "$0" + separator));
        }
        else
        {
            newPhoneNumber.Append(Regex.Replace(phoneNumber, 
                                                ".{"+ splitEach +"}(?!$)", 
                                                "$0" + separator));
        }

        return newPhoneNumber.ToString();
    }

    //This method removes spaces, and any other character that is not a number
    public static string NormalizeString(string expression)
    {
        expression = Regex.Replace(expression, @"\s", "");
        expression = string.Concat(expression.Where(char.IsDigit));

        return expression;
    }
}