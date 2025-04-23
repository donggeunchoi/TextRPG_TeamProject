using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace IPG
{

    internal class Battlecontroller
    {

        static PlayerController player = new PlayerController();
        private List<MonsterController> monsters;
        static BattleManager battleManager = new BattleManager();

        public Battlecontroller()
        {
            ControlMonster controlMonster = new ControlMonster();
            monsters = controlMonster.monsters;

        }
        
        public void Battlestart()
        {

            bool exit = true;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].Hp > 0)
                    {
                        Console.Write($" ");
                        monsters[i].ShowMonsterInfo(i+1);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine("Lv.1  Chad (전사)");
                Console.WriteLine("HP 100/100");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                string input = Console.ReadLine();
                int choice;

                // 입력이 정수인지 확인
                if (int.TryParse(input, out choice))
                {

                    switch (choice)
                    {
                        case 1:
                            BattleManager.PlayerAttackPhase();
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    exit = true;
                }
            }
        }

        public void Battlevictory()
        {
            bool exit = true;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("Victory");
                Console.WriteLine();
                Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다.");
                Console.WriteLine();
                Console.WriteLine("Lv.1 Chad");
                Console.WriteLine("HP 100 -> 74");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.Write(">>");
                string input = Console.ReadLine();
                int choice;

                // 입력이 정수인지 확인
                if (int.TryParse(input, out choice))
                {

                    switch (choice)
                    {
                        case 0:
                            Pause();
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    exit = true;
                }
            }
        }

        public void BattleLose()
        {
            bool exit = true;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("You Lose");
                Console.WriteLine();
                Console.WriteLine("Lv.1 Chad");
                Console.WriteLine("HP 100 -> 0");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.Write(">>");

                string input = Console.ReadLine();
                int choice;

                // 입력이 정수인지 확인
                if (int.TryParse(input, out choice))
                {

                    switch (choice)
                    {
                        case 0:
                            Pause();
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    exit = true;
                }
            }
        }

        static void WrongInput()
        {
            Console.WriteLine("\n\a잘못된 입력입니다.");
            // WaitInput();
        }
        private void Pause()
        {
            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }
    }
}
