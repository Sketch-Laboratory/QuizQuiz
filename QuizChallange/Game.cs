using QuizQuiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuizChallange
{
    public class Game
    {
        private static Random r = new Random();
        public static bool InitializeProgram()
        {
            Console.WriteLine("퀴즈 로딩중..");
            Questions.Instance.Load();
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
                Console.Clear();
                Console.WriteLine($"[라운드 {round}/{roundCount}]");
                var selectable = r.Next(2) == 1;
                if (!selectable)
                {
                    var q = Questions.Instance.GetRandomQuestion();
                    Console.WriteLine($"Q. {q.Description}");
                    if (CheckAnswer(q, ref lifeCount))
                    {
                        continue;
                    }
                    else
                    {
                        GameOver();
                        break;
                    }
                }
                else
                {
                    var q = Questions.Instance.GetRandomSelectableQuestion();
                    Console.WriteLine($"Q. {q.Description}");
                    Console.WriteLine();
                    for (int i = 0; i < q.Selections.Count; i++)
                    {
                        Console.WriteLine($" {i + 1}. {q.Selections[i]}");
                    }
                    if (CheckSelection(q, ref lifeCount))
                    {
                        continue;
                    }
                    else
                    {
                        GameOver();
                        break;
                    }
                }
            }
        }

        private bool CheckSelection(SelectableQuestion q, ref int lifeCount)
        {
            Console.WriteLine();
            Console.Write("A. ");
            var input = -1;
            int.TryParse(Console.ReadLine(), out input);
            if (!q.Check(input-1))
            {
                if (--lifeCount == 0) return false;
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"오답입니다! (남은 목숨 : {lifeCount}개)");
                    return CheckSelection(q, ref lifeCount);
                }
            }
            else return true;
        }

        private bool CheckAnswer(Question q, ref int lifeCount)
        {
            Console.WriteLine();
            Console.Write("A. ");
            if (!q.Check(Console.ReadLine()))
            {
                if (--lifeCount == 0) return false;
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"오답입니다! (남은 목숨 : {lifeCount}개)");
                    return CheckAnswer(q, ref lifeCount);
                }
            }
            else return true;
        }

        private void GameOver()
        {
            Console.WriteLine();
            Console.WriteLine("게임 오버!");
        }

        #region 초기화
        public void InitializeGame()
        {
            var roundCount = SetRoundCount();
            var lifeCount = SetLifeCount();
            StartGame(roundCount, lifeCount);
        }

        private int DefaultRoundCount = 50;
        private int SetRoundCount()
        {
            Console.WriteLine("총 몇 문제에 도전하시겠습니까? (기본 50문제)");
            Console.Write("문제 수 : ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return DefaultRoundCount;
            else try
            {
                return int.Parse(input);
            }
            catch
            {
                Console.WriteLine("입력이 잘못되었습니다. 올바른 숫자를 입력해주세요.");
                return SetRoundCount();
            }
        }

        private int DefaultLifeCount = 3;
        private int SetLifeCount()
        {
            Console.WriteLine("목숨은 몇 개로 하시겠습니까? (기본 3개)");
            Console.Write("목숨 수 : ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return DefaultLifeCount;
            else try
            {
                return int.Parse(input);
            }
            catch
            {
                Console.WriteLine("입력이 잘못되었습니다. 올바른 숫자를 입력해주세요.");
                return SetLifeCount();
            }
        }
        #endregion 초기화
    }
}
