using System;
using 연습장.Script;

namespace IPG
{
    internal class DungeonLobbyController
    {
        static int _unlockedFloor = 1;
            
        public void EnterDungeonLobby()
        {

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.ResetColor();
                Console.WriteLine("클리어한 층이 늘어날수록 더 높은 층이 열립니다.\n");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");

                for (int i = 1; i <= _unlockedFloor; i++)
                {
                    Console.WriteLine($"{i + 1}. {i}층 입장");
                }

                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                Random rand = new Random();
                int index = rand.Next(1, 4);
                GameManager.MonsterController.RandomMonsterType(index);

                string input = Console.ReadLine();


                if (input == "1")
                {
                    GameManager.PlayerController.Status();
                    WaitInput();
                    continue;
                }
                else if (input == "0")
                {
                    GameManager.VillageController.Enter();
                }

                if (int.TryParse(input, out int selected) && selected >= 2 && selected <= _unlockedFloor + 1)
                {
                    int chosenFloor = selected - 1;
                    StartBattle(chosenFloor);
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    WaitInput();
                }
            }
        }

        private void StartBattle(int chosenFloor)
        {
            Console.Clear();

            Console.WriteLine("뚜벅뚜벅 문 앞으로 다가갑니다.");
            Console.WriteLine();
            
            int[,] gameMap = new int[8, 8]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 1, 1, 1, 1, 0, 0},
                { 0, 0, 1, 2, 2, 1, 0, 0},
                { 0, 0, 1, 2, 2, 1, 0, 0},
                { 0, 0, 1, 1, 1, 1, 0, 0},
                { 0, 0, 1, 1, 1, 1, 0, 0},
                { 0, 0, 1, 1, 1, 1, 0, 0}
            };

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    switch (gameMap[y, x])
                    {
                        case 0: Console.Write("□ "); break; // 빈 공간
                        case 1: Console.Write("■ "); break; // 벽
                        case 2: Console.Write("Ω "); break;
                        
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine($"{chosenFloor}층 전투를 시작합니다. 행운을 빕니다.\n");

            // 클리어 성공했다고 가정하고 다음 층 열기
            if (  _unlockedFloor < chosenFloor + 1)
            {
                _unlockedFloor = chosenFloor + 1;
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
            Console.ReadKey(true);

            // EnterDungeonLobby();
            GameManager.BattleController.Battlestart();
            bool isVictory = BattleManager.StartBattleAndCheckVictory();

            if (isVictory && _unlockedFloor < chosenFloor + 1)
            {
                _unlockedFloor = chosenFloor + 1;
                Console.WriteLine($"{chosenFloor}층을 클리어했습니다.");
                Console.WriteLine("다음 층이 열렸습니다.");
                
            }
            // 배틀컨트롤러로 넘어가면 초기화되어 1로 다시 시작 되는 문제.

                WaitInput();
        }

        private void WaitInput()
        {
            Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
            Console.ReadKey(true);
        }
    }
}
