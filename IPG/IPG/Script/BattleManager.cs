using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace IPG
{
    public class BattleResult
    {
        public int ExpGained { get; set; }
        public int GoldGained { get; set; }
        public List<string> ItemsGained { get; set; }

        public BattleResult(int exp, int gold, List<string> items)
        {
            ExpGained = exp;
            GoldGained = gold;
            ItemsGained = items;
        }
    }
    internal class Battlecontroller
    {
        static PlayerController player = new PlayerController();
        static StoreController store = new StoreController(player);
        static InventoryController inventory = new InventoryController(store, player);
        static Battlecontroller battleController = new Battlecontroller();
        static BattleManager battleManager = new BattleManager();
        static DungeonLobbyController dungeonLobby = new DungeonLobbyController(player, battleController);
        static VillageController village;

        private int playerHpBeforeBattle;
        private int playerExpBeforeBattle;
        private int playerGoldBeforeBattle;
        private int playerLevelBeforeBattle;

        private MonsterController[] monsters;


        public Battlecontroller()
        {
            ControlMonster controlMonster = new ControlMonster();
            monsters = controlMonster.monsters;

            BattleManager.Initialize(player, monsters, this);

            village = new VillageController(store, inventory, player, this, battleManager, dungeonLobby);

        }

        public void Battlestart()
        {
            playerHpBeforeBattle = player.Hp;
            playerExpBeforeBattle = player.Exp;
            playerGoldBeforeBattle = player.Gold;
            playerLevelBeforeBattle = player.Level;

            bool exit = true;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                for (int i = 0; i < monsters.Length; i++)
                {
                    if (monsters[i].Hp > 0)
                    {
                        Console.Write($" ");
                        monsters[i].ShowMonsterInfo(i + 1);
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

            int totalExp = 0;
            int totalGold = 0;

            foreach (var monster in monsters)
            {
                if (monster.IsDead)
                {
                    totalExp += monster.Level * 1;
                    totalGold += monster.Level * 50;
                }
            }

            player.GainExp(totalExp);
            player.Gold += totalGold;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("\nBattle!! - Result");
                Console.WriteLine("\nVictory");
                Console.WriteLine($"\n던전에서 몬스터 {monsters.Count(m => m.IsDead)}마리를 잡았습니다.");
                Console.WriteLine("\n[캐릭터 정보]");
                Console.Write($"Lv.{playerLevelBeforeBattle} {player.Name}");
                Console.WriteLine($"-> Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {playerHpBeforeBattle} -> {player.Hp}");
                Console.Write($"Exp {playerExpBeforeBattle} -> {player.Exp} ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"(+{totalExp})");
                Console.ResetColor();
                Console.WriteLine("\n[획득 아이템]");
                Console.WriteLine($"+{totalGold}");
                Console.WriteLine("\n0. 다음");
                Console.Write("\n>>");
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
                Console.WriteLine($"HP {playerHpBeforeBattle} -> {player.Hp}");
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