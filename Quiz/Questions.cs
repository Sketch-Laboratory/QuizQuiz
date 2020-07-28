using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quiz
{
    public class Questions
    {
        public static Questions Instance { get; private set; } = new Questions();
        private Questions() { }

        private const string dir = "./Questions";
        private Random r = new Random();

        public List<Question> Dictionary { get; private set; } = new List<Question>();
        public List<SelectableQuestion> SDictionary { get; private set; } = new List<SelectableQuestion>();

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

                    var answer = columns[0].Trim();
                    var desc = columns[1].Trim();

                    var selections = answer.Split(':');
                    if (selections.Length == 1)
                    {
                        // 주관식 문제
                        Dictionary.Add(new Question(answer, desc));
                    }
                    else
                    {
                        // 답안 지정 객관식 문제
                        SDictionary.Add(new SelectableQuestion(selections.ToList(), desc));
                    }
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

        public SelectableQuestion GetRandomSelectableQuestion()
        {
            var c = Dictionary.Count + SDictionary.Count;
            if (c == 0) return null;
            var i = r.Next(c);
            if (Dictionary.Count > i)
            {
                return new SelectableQuestion(Dictionary);
            }
            else
            {
                return SDictionary[i - Dictionary.Count];
            }
        }
    }

    public class Question : Tuple<string, string>
    {
        public Question(string answer, string desc) : base(answer, desc) { }

        public string Answer { get { return base.Item1; } }
        public string Description { get { return base.Item2; } }

        public bool Check(string input)
        {
            input = input.Trim().ToLower();
            var answer = Answer.Trim().ToLower();
            return input == answer;
        }
    }

    public class SelectableQuestion
    {
        private Random r = new Random();

        public SelectableQuestion(List<Question> questions, int selectionCount = 5)
        {
            if (questions.Count == 0) return;
            var q = questions[r.Next(questions.Count)];
            this.Description = q.Description;
            this.Answer = q.Answer;
            this.Selections.Add(q.Answer);

            while (Selections.Count < selectionCount)
            {
                var answer = GetRandomAnswer(questions, this.Selections);
                if (answer == null) break;
                this.Selections.Add(answer);
            }
        }

        public SelectableQuestion(List<string> selections, string desc)
        {
            this.Description = desc;
            this.Answer = selections[0];
            this.Selections = selections;
        }

        private string GetRandomAnswer(List<Question> questions, List<string> ignores)
        {
            if (questions.Count == 0 || questions.Count <= ignores.Count) return null;
            var q = questions[r.Next(questions.Count)];
            if (ignores.Contains(q.Answer)) return GetRandomAnswer(questions, ignores);
            else return q.Answer;

        }

        public string Answer { get; private set; }
        public List<string> Selections { get; private set; } = new List<string>();

        public string Description { get; private set; }

        public bool Check(string input)
        {
            input = input.Trim().ToLower();
            var answer = Answer.Trim().ToLower();
            return input == answer;
        }
        public bool Check(int input)
        {
            return input >= 0 && Selections[input] == Answer;
        }

        private List<T> Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public List<string> ShuffledSelections
        {
            get
            {
                return Shuffle(Selections);
            }
        }
    }
}
