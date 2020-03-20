using System;
using JQuiz;

namespace CliFrontend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            var wordProvider = new SQLiteWordProvider("data.db");
            var wordList = wordProvider.GetWordsWithKanji(args);

            while (true)
            {
                var question = wordList.Random();

                Console.WriteLine(question.Kanji);
                Console.Write(">|");

                var guess = Console.ReadLine();

                if (question.Readings.Contains(guess))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong!");
                }
                Console.WriteLine(question);
                Console.WriteLine();
                Console.ResetColor();
            }
        }
    }
}
