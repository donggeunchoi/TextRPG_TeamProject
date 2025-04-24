using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace IPG
{

    internal class BattleController
    {
        PlayerController player;
        List<MonsterController> monsters;
        private VillageController village;
        private DungeonLobbyController dungeonLobby;

        private int playerHpBeforeBattle;
        private int playerExpBeforeBattle;
        private int playerGoldBeforeBattle;
        private int playerLevelBeforeBattle;

        public Battlecontroller(PlayerController injectedPlayer, VillageController v, DungeonLobbyController d)
        {
            player = injectedPlayer;
            village = v;
            dungeonLobby = d;

            ControlMonster controlMonster = new ControlMonster();
            Random rand = new Random();
            monsters = new List<MonsterController>();

            for (int i = 0; i < 3; i++) // 몬스터 랜덤 생성
            {
                int index = rand.Next(controlMonster.monsters.Count);
                MonsterController copy = new MonsterController(controlMonster.monsters[index]);
                monsters.Add(copy);
            }

            BattleManager.SetMonsters(monsters); // 배틀매니저랑 연결
        }

        public void SetVillage(VillageController v)
        {
            village = v;
        }

        public void SetDungeonLobby(DungeonLobbyController d)
        {
            dungeonLobby = d;
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
                for (int i = 0; i < monsters.Count; i++)
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
            int totalExp = monsters.Where(m => m.IsDead).Sum(m => m.Level * 1);
            int totalGold = monsters.Where(m => m.IsDead).Sum(m => m.Level * 50);
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
                Console.WriteLine("\nYou Lose");
                Console.WriteLine($"\nLv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {playerHpBeforeBattle} -> {player.Hp}");
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