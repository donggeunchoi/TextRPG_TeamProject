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

        private int playerHpBeforeBattle;
        private int playerExpBeforeBattle;
        private int playerGoldBeforeBattle;
        private int playerLevelBeforeBattle;

    
        public void Battlestart()
        {
            playerHpBeforeBattle = GameManager.PlayerController.currentHp;
            playerExpBeforeBattle = GameManager.PlayerController.Exp;
            playerGoldBeforeBattle = GameManager.PlayerController.Gold;
            playerLevelBeforeBattle = GameManager.PlayerController.Level;

            bool exit = true;
            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                BattleManager.ShowDungeonMonster();

                Console.WriteLine();
                Console.WriteLine("\n[내 정보]");
                Console.WriteLine($"Lv.{GameManager.PlayerController.Level} <{GameManager.PlayerController.Name}> {GameManager.PlayerController.Job}");
                Console.WriteLine($"HP {GameManager.PlayerController.currentHp}/{GameManager.PlayerController.maxHp}");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                string input = Console.ReadLine();
                int choice;


                if (int.TryParse(input, out choice))
                {

                    switch (choice)
                    {
                        case 0:
                            GameManager.DungeonLobbyController.EnterDungeonLobby();
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
            int totalExp = BattleManager.CurrentMonsters.Where(m => m.IsDead).Sum(m => m.Level * 1);
            int totalGold = BattleManager.CurrentMonsters.Where(m => m.IsDead).Sum(m => m.Level * 50);
            GameManager.PlayerController.GainExp(totalExp);
            GameManager.PlayerController.Gold += totalGold;
            if (DungeonLobbyController._lastClearedFloor == 2)
                GameManager.QuestController.OnStageCleared(2);
            Random random = new Random();
            List<ItemController> droppedItems = new List<ItemController>();

            foreach (var monster in BattleManager.CurrentMonsters.Where(m => m.IsDead))
            {
                var droppableItems = GameManager.ListStoreItems
                    .Where(item => item.DropRate > 0)
                    .ToList();

                foreach (var item in droppableItems)
                {
                    if (random.NextDouble() < item.DropRate)
                    {
                        int index = GameManager.ListStoreItems.FindIndex(x => x.Name == item.Name);
                        if (index != -1)
                        {
                            GameManager.ListPlayerOwningNumber[index]++;
                        }
                        else
                        {
                            GameManager.ListStoreItems.Add(item);
                            GameManager.ListPlayerOwningNumber.Add(1);
                        }

                        item.IsBuy = true;
                        droppedItems.Add(item);
                    }
                }
            }

            if ( DungeonLobbyController._unlockedFloor < DungeonLobbyController._MaxFloor)
            {
                DungeonLobbyController._unlockedFloor++;

            }

            while (exit)
                {

                    Console.Clear();
                    Console.WriteLine("\nBattle!! - Result");
                    Console.WriteLine("\nVictory");
                    Console.WriteLine($"\n던전에서 몬스터 {GameManager.ListMonsters.Count(m => m.IsDead)}마리를 잡았습니다.");
                    Console.WriteLine("\n[캐릭터 정보]");
                    Console.Write($"Lv.{playerLevelBeforeBattle} {GameManager.PlayerController.Name}");
                    Console.WriteLine($"-> Lv.{GameManager.PlayerController.Level} {GameManager.PlayerController.Name}");
                    Console.WriteLine($"HP {playerHpBeforeBattle} -> {GameManager.PlayerController.currentHp}");
                    Console.Write($"Exp {playerExpBeforeBattle} -> {GameManager.PlayerController.Exp} ");
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
                                GameManager.VillageController.Enter();
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
                Console.WriteLine($"\nLv.{GameManager.PlayerController.Level} {GameManager.PlayerController.Name}");
                Console.WriteLine($"HP {playerHpBeforeBattle} -> {GameManager.PlayerController.currentHp}");
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
                            GameManager.VillageController.Enter();
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
    }
}