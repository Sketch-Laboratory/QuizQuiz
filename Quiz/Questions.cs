using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QuizQuiz
{
    public class Questions
    {
        public static Questions Instance { get; private set; } = new Questions();
        private Questions() { }

        private const string dir = "./Questions";
        private Random r = new Random();

        public List<Question> Dictionary { get; private set; } = new List<Question>();

        public void Load()
        {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                if (!File.Exists(file)) continue;
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    // 주석 라인 처리
                    if (line.StartsWith("#")) continue;

                    var columns = line.Split(';');
                    if (columns.Length < 2) continue;
                    Dictionary.Add(new Question(columns[0].Trim(), columns[1].Trim()));
                }
            }
        }

        public void PrintAll()
        {
            foreach (var item in Dictionary)
            {
                Console.WriteLine($"{item.Item1} -> {item.Item2}");
            }
        }

        public Question GetRandomQuestion()
        {
            if (Dictionary.Count == 0) return null;
            return Dictionary[r.Next(Dictionary.Count)];
        }
    }

    public class Question : Tuple<string, string> {
        public Question(string answer, string desc) : base(answer, desc)
        {
        }

        public string Answer { get { return base.Item1; } }
        public string Description { get { return base.Item2; } }

        public bool Check(string input)
        {
            input = input.Trim().ToLower();
            var answer = Answer.Trim().ToLower();
            return input == answer;
        }
    }
}
