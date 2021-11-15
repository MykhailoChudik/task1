using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace task_1
{
    class Statistics
    {
        public bool error { get; private set;}
        private char[] separators = new[] { ' ', ',', '.', '!', '?', '\r', '\n' };
        private string str;
        private string[] words;
        private List<string> fileList = new List<string>();
        private Dictionary<string, WordInfo> myDictionary = new Dictionary<string, WordInfo>();
        private StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        public Statistics(string file)
        {
            try
            {
                foreach (var lines in System.IO.File.ReadLines(file))
                {
                    fileList.Add(lines);
                }
                CreateDictionary();
                error = false;
            }
            catch (Exception e)
            {
                error = true;
                Console.WriteLine(e.Message);
            }
        }

        private void CreateDictionary()
        {
            foreach (var fileLine in fileList)
            {
                str += " " + fileLine;
            }
            words = str.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var distinctWord = words.Distinct();
            foreach (var word in distinctWord)
            {
                myDictionary.Add(word, new WordInfo());
            }
            SetInformInDictionary();
        }

        private void SetInformInDictionary()
        {
            for (var i = 0; i < fileList.Count; i++)
            {
                var wordLine = fileList[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < wordLine.Length; j++)
                {
                    foreach (var word in myDictionary)
                    {
                        if (comparer.Equals(word.Key, wordLine[j]))
                        {
                            word.Value.setLine(i + 1);
                            word.Value.setPos(j + 1);
                        }
                    }
                }
            }
        }

        public void PrintDictionary()
        {
            var sorted = myDictionary.OrderByDescending(x => x.Value.getLine().Count);
            foreach (var pair in sorted)
            {
                Console.WriteLine($"\t{pair.Key} {pair.Value.getLine().Count} times");
            }
        }

        public void WordInform(string wordToFind)
        {
            var wordInDictionary = false;

            foreach (var wordInfo in myDictionary)
            {
                if (comparer.Equals(wordInfo.Key, wordToFind))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You word exists in:");
                    Console.ResetColor();
                    wordInDictionary = true;
                    var informLine = wordInfo.Value.getLine();
                    var informPos = wordInfo.Value.getPos();
                    for (var i = 0; i < informLine.Count; i++)
                    {
                        Console.WriteLine($"\tline = {informLine[i]} position = {informPos[i]}");
                    }
                }
            }

            if (!wordInDictionary)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You word doesn't exist in file, try to put another word");
                Console.ResetColor();
            }
        }
    }
}
