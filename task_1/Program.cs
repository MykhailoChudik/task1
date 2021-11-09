using System;
using System.Collections.Generic;
using System.Linq;

namespace task_1
{
    class WordInfo
    {
        private List<int> line = new List<int>();
        private List<int> pos = new List<int>();

        public List<int> getLine()
        {
            return this.line;
        }

        public void setLine(int n)
        {
            line.Add(n);
        }

        public List<int> getPos()
        {
            return this.pos;
        }

        public void setPos(int n)
        {
            pos.Add(n);
        }
    }

    class Statistics
    {
        private char[] separators = new[] { ' ', ',', '.', '!', '?', '\r', '\n' };
        private string str;
        private string[] words;
        private List<string> fileList = new List<string>();
        private Dictionary<string, WordInfo> myDictionary = new Dictionary<string, WordInfo>();
        
        public Statistics(string file)
        {
            try
            {
                foreach (string lines in System.IO.File.ReadLines(file))
                {
                    fileList.Add(lines);
                }

                CreateDictionary();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CreateDictionary()
        {
            foreach (string fileLine in fileList)
            {
                str +=" " + fileLine;
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
            for (int i = 0; i < fileList.Count; i++)
            {
                string[] wordLine = fileList[i].ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < wordLine.Length; j++)
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
                Console.WriteLine($"{pair.Key} {pair.Value.getLine().Count} times");
            }
        }

        public void WordInform(string str)
        {
            foreach (var wordInfo in myDictionary)
            {
                string lower1 = wordInfo.Key.ToLower();
                string lower2 = str.ToLower();
                if (lower1.Equals(lower2))
                {
                    var informLine = wordInfo.Value.getLine();
                    var informPos = wordInfo.Value.getPos();
                    for (int i = 0; i < informLine.Count; i++)
                    {
                        Console.WriteLine($"line = {informLine[i]} position = {informPos[i]}");
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Statistics text = new Statistics(Console.ReadLine());

            text.PrintDictionary();
            while (true)
            {
                Console.WriteLine("--------Word to find--------");
                text.WordInform(Console.ReadLine());
            }
        }
    }
}
