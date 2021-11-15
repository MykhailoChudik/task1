using System;
using System.Collections.Generic;
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
            words = RemoveDuplicates(words);

            foreach (var word in words)
            {
                myDictionary.Add(word, new WordInfo());
            }
            SetInformInDictionary();
        }

        private void SetInformInDictionary()
        {
            for (var i = 0; i < fileList.Count; i++)
            {
                var wordLine = fileList[i].ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < wordLine.Length; j++)
                {
                    foreach (var word in myDictionary)
                    {
                        if (word.Key.Equals(wordLine[j]))
                        {
                            word.Value.setLine(i + 1);
                            word.Value.setPos(j + 1);
                        }
                    }
                }
            }
        }

        private static string[] RemoveDuplicates(string[] s)
        {
            var set = new HashSet<string>(s);
            var result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

        public void PrintDictionary()
        {
            var sorted = myDictionary.OrderByDescending(x => x.Value.getLine().Count);
            foreach (var pair in sorted)
            {
                Console.WriteLine($"\t{pair.Key} {pair.Value.getLine().Count} times");
            }
        }

        public void WordInform(string str)
        {
            var wordInDictionary = false;

            foreach (var wordInfo in myDictionary)
            {
                var lower1 = wordInfo.Key.ToLower();
                var lower2 = str.ToLower();
                if (lower1.Equals(lower2))
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
