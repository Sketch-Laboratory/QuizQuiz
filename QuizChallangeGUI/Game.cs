using Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace QuizChallangeGUI
{
    public class Game
    {
        private static Random r = new Random();

        public bool IncludeType { get; internal set; }
        public bool IncludeSelection { get; internal set; }

        public static bool InitializeProgram(bool generateSelection)
        {
            Console.WriteLine("퀴즈 로딩중..");
            Questions.Instance.Load();
            Questions.Instance.GenerateSelection = generateSelection;
            var qc = Questions.Instance.Dictionary.Count;
            if(qc == 0)
            {
                Console.WriteLine("불러온 퀴즈가 없습니다! Questions 폴더에 퀴즈 파일을 넣어주세요.");
                return false;
            }
            Console.WriteLine($"{qc}개의 퀴즈를 불러왔습니다.\n");
            return true;
        }

        public void StartGame(int roundCount, int lifeCount)
        {
            for(int round = 1; round <= roundCount; round++)
            {
                var quizType = false;
                if (IncludeType && IncludeSelection) quizType = (r.Next(2) == 1);
                else if (IncludeType) quizType = true;
                else if (IncludeSelection) quizType = false;
                if (quizType)
                {
                    var w = new QuestionWindow
                    {
                        Title = $"[라운드 {round}/{roundCount}]",
                        LeftedLife = lifeCount
                    };
                    w.ShowDialog();
                    lifeCount = w.LeftedLife;
                    if (lifeCount <= 0) {
                        GameOver();
                        return;
                    }
                }
                else
                {
                    var w = new SelectableQuestionWindow
                    {
                        Title = $"[라운드 {round}/{roundCount}]",
                        LeftedLife = lifeCount
                    };
                    w.ShowDialog();
                    lifeCount = w.LeftedLife;
                    if (lifeCount <= 0) {
                        GameOver();
                        return;
                    }
                }
            }
            Win();
        }

        public event Action OnGameFinished;

        private void Win()
        {
            OnGameFinished();
            MessageBox.Show("축하합니다!");
        }

        private void GameOver()
        {
            OnGameFinished();
            MessageBox.Show("게임 오버!");
        }
    }
}
