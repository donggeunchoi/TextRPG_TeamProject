using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace IPG
{

    internal class Battlecontroller
    {
        static PlayerController player = new PlayerController();
        static StoreController store = new StoreController(player);
        static InventoryController inventory = new InventoryController(store, player);
        static Battlecontroller battleController = new Battlecontroller();
        static BattleManager battleManager = new BattleManager();
        static DungeonLobbyController dungeonLobby = new DungeonLobbyController(player, battleController);
        static VillageController village;
        

        private MonsterController[] _monsters;


        public Battlecontroller()
        {
            ControlMonster controlMonster = new ControlMonster();
            _monsters = controlMonster.monsters;
            
            village = new VillageController(store, inventory, player, this, battleManager,dungeonLobby);

        }
          
        public void Battlestart()
        {


            bool exit = true;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                for (int i = 0; i < _monsters.Length; i++)
                {
                    if (_monsters[i].Hp > 0)
                    {
                        Console.Write($" ");
                        _monsters[i].ShowMonsterInfo(i + 1);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} {player.Job}");
                Console.WriteLine($"HP {player.Hp}/100");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine("0. 나가기");
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
                        case 0:
                            dungeonLobby.EnterDungeonLobby();
                            break;

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
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP 100 -> {player.Hp}");
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
                            village.Enter();
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
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP 100 -> {player.Hp}");
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
                            village.Enter();
                            exit = false;
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
