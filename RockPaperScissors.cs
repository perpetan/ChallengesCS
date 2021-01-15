using System;

class RockPaperScissors
{
    private static int userScore = 0;
    private static int computerScore = 0;

    enum Choice
    {
        Rock,
        Paper,
        Scissors
    }
        
    static void Main(string[] args)
    {
        while (true)
        {
            Array choices = Enum.GetValues(typeof(Choice));
            Random random = new Random();

            Console.Write("Choose and type Rock, Paper or Scissors: ");

            // The above will read user input and test it against enum Choice. If the enumeration for user input is found, it will execute this if statement
            if (Enum.TryParse<Choice>(Console.ReadLine(), ignoreCase: true, out var userChoice))
            {
                Choice computerChoice = (Choice)choices.GetValue(random.Next(choices.Length));

                Console.WriteLine("User: " + userChoice.ToString());
                Console.WriteLine("Computer: " + computerChoice.ToString());

                if (userChoice == computerChoice)
                    Console.WriteLine("Tie");

                else if (userChoice == Choice.Rock)
                    PrintWinner(computerChoice == Choice.Scissors);

                else if (userChoice == Choice.Scissors)
                    PrintWinner(computerChoice == Choice.Paper);

                else if (userChoice == Choice.Paper)
                    PrintWinner(computerChoice == Choice.Rock);

                Console.WriteLine();
                Console.WriteLine("Score: Computer = " + computerScore.ToString() + " User: " + userScore.ToString());
            }
            else
                break;
        }
    }

    public static void PrintWinner(bool isUserWinner)
    {
        Console.WriteLine(isUserWinner ? "!!! User won !!!" : "!!! Computer won !!!");
        _ = isUserWinner ? userScore++ : computerScore++;
    }
}