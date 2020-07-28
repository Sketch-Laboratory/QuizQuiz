using Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuizChallange
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Game.InitializeProgram()) { Quit(); return; }

            InitializeGame(args);

        }

        private static void InitializeGame(string[] args)
        {
            var game = new Game();
            if (args.Length < 2) game.InitializeGame();
            else game.StartGame(int.Parse(args[0]), int.Parse(args[1]));

            Console.Clear();
            Console.Write("한번 더 플레이 하시겠습니까? (Y/N) : ");
            if (Console.ReadLine().Trim().ToUpper().StartsWith("Y")) InitializeGame(args);
            else Quit();
        }

        private static void Quit()
        {
            Console.WriteLine();
            Console.WriteLine("아무 키나 누르면 종료합니다.");
            Console.ReadKey();
        }
    }
}
