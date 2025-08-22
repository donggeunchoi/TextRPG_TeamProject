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

            int nextFloor = DungeonLobbyController._lastClearedFloor + 1;
            if (DungeonLobbyController._unlockedFloor < nextFloor
                && nextFloor <= DungeonLobbyController._MaxFloor)
            {
                DungeonLobbyController._unlockedFloor = nextFloor;
            }

            while (exit)
            {

                    Console.Clear();
                    Console.WriteLine("\nBattle!! - Result");
                    Console.WriteLine("\nVictory");
                    int killed = BattleManager.CurrentMonsters.Count(m => m.IsDead);
                    Console.WriteLine($"\n던전에서 몬스터 {killed}마리를 잡았습니다.");
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
                            if (DungeonLobbyController._unlockedFloor == 4)
                            {
                                Ending.ShowEnding();
                            }
                            else
                            {
                                 GameManager.VillageController.Enter();    
                            }
                               
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
        public void SlowPrint(string text, int delay = 50)
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
            }

        public void ShowEnding()
        {

            Console.Clear();
            SlowPrint("…\n", 100);
            SlowPrint("거대한 어둠이 무너지고, 세상에 다시 빛이 찾아왔다.\n", 70);
            SlowPrint("모험가는 무거운 검을 내려놓고 하늘을 올려다본다.\n", 70);
            SlowPrint("\n'이제, 진짜 평화가 오는 걸까...'\n", 100);

            Thread.Sleep(1500);
            Console.Clear();

            SlowPrint("사람들은 모험가의 이름을 노래하며 거리를 가득 메웠다.\n", 70);
            SlowPrint("어린 아이들은 모험가를 영웅이라 부르고,\n", 70);
            SlowPrint("마을 어른들은 다시는 어둠이 오지 않기를 기도했다.\n", 70);

            Thread.Sleep(1500);
            Console.Clear();

            SlowPrint("하지만, 모험가는 조용히 그 자리를 떠났다.\n", 70);
            SlowPrint("그는 명예도, 보상도 바라지 않았다.\n", 70);
            SlowPrint("\n모험가에게 남은 것은...\n", 100);
            SlowPrint("오직 또 다른 여정을 향한 갈망뿐이었다.\n", 100);

            Thread.Sleep(2000);
            Console.Clear();

            SlowPrint("\n\n그리고...\n", 150);
            SlowPrint("또 다른 모험이, 그를 기다리고 있었다.\n", 100);
            SlowPrint("\n\n- The End -\n", 200);

            Console.WriteLine("\n\n[Enter 키를 누르면 크레딧이 시작됩니다]");
            Console.ReadLine();

            ShowCredits();
        }

        public void ShowCredits()
        {
            Console.Clear();
            List<string> credits = new List<string>
            {
                "===== Staff =====",
                "퀘스트 진행 및 발표자 : 임 규민",
                "메인 타이틀 및 연장자 : 최 다혜",
                "와이어 프레임 및 팀장 : 최 동근",
                "리엑션 및 포션 보상진행 : 이 광민",
                "각종 디버깅 및 막내 : 박 원희",
                "아이템 : 박 원희, 임 규민, 이 광민",
                "상점 : 박 원희",
                "인벤토리 : 박 원희",
                "전투 시스템 : 최 다혜, 이 광민, 최 동근",
                "몬스터 : 박 원희, 임 규민, 최 동근",
                "애니메이션 : 임 규민, 최 다혜",
                "",
                "===== Special Thanks =====",
                "모든 도움을 주신 튜터님들",
                "I0조의 열정과 노력",
                "",
                "끝까지 플레이해주셔서 감사합니다!",
                "THE END"
            };

            foreach (string line in credits)
            {
                Console.WriteLine(line);
                Thread.Sleep(1000); // 한 줄당 1초 딜레이
            }

            Console.WriteLine("\n\n[Enter 키를 누르면 게임이 종료됩니다]");
            Console.ReadLine();
            Environment.Exit(0);
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