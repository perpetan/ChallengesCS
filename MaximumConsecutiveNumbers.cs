/*
Given an array, find the maximum number of consecutive numbers in this array.
For example, having the next array: [1,1,0,1,1,1], the maximum number of consecutive 1s is 3. 
 */

using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int[] input = { 1, 1, 0, 0, 0, 0, 0, 1, 1, 1 };
        int numberToBeCounted = 1;

        var result = ConsecutiveNumbers(input);
        Console.WriteLine(result.Item1 + " consecutive " + result.Item2 + "s found");

        Console.WriteLine(ConsecutiveNumbers(input, numberToBeCounted) + " consecutive " + numberToBeCounted + "s found");
    }

    // This method compares all array positions and give the maximum number of any consecutive number.
    // Note that it will return the consecutive value AND the number which is consecutive
    public static (int, int) ConsecutiveNumbers(int[] numbers)
    {
        int count = 1;
        int consecutive = 1;

        int consecutiveNumber = numbers.First();

        for (int i=1; i<numbers.Length; i++)
        {
            _ = numbers[i] == numbers[i - 1] ? count++ : count = 1;

            if (count >= consecutive)
            {
                consecutive = count;
                consecutiveNumber = numbers[i];
            }
        }

        return (consecutive, consecutiveNumber);
    }

    // This method give the maximum number of consecutive number entered in code.
    // It only returns how many times the given number is consecutive.
    // It does not return the consecutive number itself, because it is already declared in numberToBeCounted variable in Main()
    public static int ConsecutiveNumbers(int[] numbers, int numberToBeCounted)
    {
        int count = 1;
        int consecutive = 1;

        foreach (int number in numbers)
        {
            _ = number == numberToBeCounted ? count++ : count = 1;

            if (count >= consecutive)
                consecutive = count;
        }

        return consecutive;
    }
}