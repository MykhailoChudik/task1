using System;

namespace task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Put file path like:");
            Console.ResetColor();
            Console.WriteLine("\t" + @"C:\Users\mykhailo.chudik\Desktop\text.txt");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("or drop file into the console window and press enter");
            Console.ResetColor();
            var text = new Statistics(Console.ReadLine());

            if (!text.error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Words statistic in file:");
                Console.ResetColor();
                text.PrintDictionary();

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("--------Put word to find--------");
                    Console.ResetColor();
                    text.WordInform(Console.ReadLine());
                }
            }
        }
    }
}
