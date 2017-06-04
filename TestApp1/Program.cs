using System;

namespace TestApp1
{
    class Program
    {
        static string choices = "\n 1.Console\n 2.File\n 3.Database\n";
        static string start = "\n  ##############################  \n" +
                              "\n    Small input/output program    \n" +
                              "\n  ##############################  \n\n\n";
        static InputOutput inputOutput = new InputOutput();
        static void Main(string[] args)
        {
            Console.WriteLine(start + "\n Choose your way of input:\n" + choices);
            string choice = Console.ReadLine();

            inputOutput.HandleChoiceInput(choice);

            Console.WriteLine("\n Choose your way of output:\n" + choices);
            choice = Console.ReadLine();

            inputOutput.WordCount();
            inputOutput.HandleChoiceOutput(choice);
        }
    }
}
