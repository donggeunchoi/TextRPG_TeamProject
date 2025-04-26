using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace IPG
{
    internal class StoreController
    {
        public void Enter()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 상점 ]\n");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 구매할 수 있습니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.PlayerController.Gold} G\n");

            ShowItems();
            ShowItemsWithNumber();
            BuyItem();
        }

        private void AddItem(string itemName, string itemType, int itemEffect, string itemDesc, int itemPrice, int remaining, bool itemIsSold, bool isBuy, double dropRate)
        {
            GameManager.ListStoreItems.Add(new ItemController
            {
                ItemType = itemType,
                Name = itemName,
                Effect = itemEffect,
                Desc = itemDesc,
                Price = itemPrice,
                Remaining = remaining,
                IsSold = itemIsSold,
                IsBuy = isBuy,
                DropRate = dropRate
            }
            );
        }

        public void SaveItem()
        {
            if (GameManager.ListStoreItems.Count > 0) return;

            AddItem("수련자 갑옷    ", "방어구", 5, " 수련에 도움을 주는 갑옷입니다.                   ", 1000, 1, false, false, 0.1);
            AddItem("무쇠갑옷       ", "방어구", 9, " 무쇠로 만들어져 튼튼한 갑옷입니다.               ", 2000, 1, false, false, 0.1);
            AddItem("스파르타의 갑옷", "방어구", 15, " 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 1, false, false, 0.1);
            AddItem("낡은 검        ", "무기", 2, " 쉽게 볼 수 있는 낡은 검 입니다.                  ", 600, 1, false, false, 0.1);
            AddItem("청동 도끼      ", "무기", 5, " 어디선가 사용됐던 거 같은 도끼입니다.            ", 1500, 1, false, false, 0.1);
            AddItem("스파르타의 창  ", "무기", 7, " 스파르타의 전사들이 사용했다는 전설의 창입니다.  ", 3000, 1, false, false, 0.1);
            AddItem("중급 회복 포션 ", "포션", 30, " 연금술사가 나름의 심혈을 기울여 만든 포션입니다. ", 800, 3, false, false, 0.1);


            foreach (ItemController item in GameManager.ListStoreItems)
            {
                if (item.IsSold && item.Remaining == 0)
                {
                    GameManager.ListPlayerOwningNumber.Add(1);
                }
                else
                {
                    GameManager.ListPlayerOwningNumber.Add(0);
                }
            }
        }

        public void BuyItem()
        {
            bool stopBuyItems = false;

            while (stopBuyItems == false && _saveUserInput == "1")
            {
                string choiceBuyItem = Console.ReadLine();

                switch (choiceBuyItem)
                {
                    case "0":
                        stopBuyItems = true;
                        break;

                    case "1":
                        CompareWithMoney(int.Parse(choiceBuyItem));
                        ShowItemsWithNumber();
                        break;

                    case "2":
                        CompareWithMoney(int.Parse(choiceBuyItem));
                        ShowItemsWithNumber();
                        break;

                    case "3":
                        CompareWithMoney(int.Parse(choiceBuyItem));
                        ShowItemsWithNumber();
                        break;

                    case "4":
                        CompareWithMoney(int.Parse(choiceBuyItem));
                        ShowItemsWithNumber();
                        break;

                    case "5":
                        CompareWithMoney(int.Parse(choiceBuyItem));
                        ShowItemsWithNumber();
                        break;

                    case "6":
                        CompareWithMoney(int.Parse(choiceBuyItem));
                        ShowItemsWithNumber();
                        break;

                    case "7":
                        CompareWithMoney(int.Parse(choiceBuyItem));
                        ShowItemsWithNumber();
                        break;

                    default:
                        ShowItemsWithNumber();
                        break;

                }

                choiceBuyItem = null;

            }
        }

        public void ShowItems()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [ 상점 ]\n");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 구매할 수 있습니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{GameManager.PlayerController.Gold} G\n");

                Console.WriteLine("[아이템 목록]");

                foreach (ItemController item in GameManager.ListStoreItems)
                {
                    if (item.Remaining == 0)
                    {
                        item.IsSold = true;
                    }
                    else
                    {
                        item.IsSold = false;
                    }
                }

                foreach (ItemController item in GameManager.ListStoreItems)
                {
                    if (item.ItemType == "무기")
                    {
                        Console.Write($"- {item.Name}    | 공격력 +{item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}   | ");

                        if (item.IsSold)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[ 품절 ]");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.Price.ToString().PadLeft(4, ' ')} G  | 남은 수량 : {item.Remaining}");
                        }
                        Console.WriteLine();
                    }

                    if (item.ItemType == "방어구")
                    {
                        Console.Write($"- {item.Name}    | 방어력 +{item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}   | ");

                        if (item.IsSold)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[ 품절 ]");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.Price.ToString().PadLeft(4, ' ')} G  | 남은 수량 : {item.Remaining}");
                        }
                        Console.WriteLine();
                    }

                    if (item.ItemType == "포션")
                    {
                        Console.Write($"- {item.Name}    | 회복량 +{item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}   | ");

                        if (item.IsSold)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[ 품절 ]");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.Price.ToString().PadLeft(4, ' ')} G  | 남은 수량 : {item.Remaining}");
                        }
                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.Write("0. 나가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>>");

                string buyItem = Console.ReadLine();
                _saveUserInput = buyItem;

                if (_saveUserInput == "0" || _saveUserInput == "1")
                    break;

                Console.Write("해당하는 숫자를 입력하세요.");
                Thread.Sleep(500);
                Console.Clear();
            }
        }

        public void CompareWithMoney(int itemNumber)
        {
            if (GameManager.PlayerController.Gold >= GameManager.ListStoreItems[itemNumber - 1].Price && GameManager.ListStoreItems[itemNumber - 1].IsSold == false)
            {
                GameManager.PlayerController.Gold -= GameManager.ListStoreItems[itemNumber - 1].Price;
                GameManager.ListStoreItems[itemNumber - 1].Remaining--;

                GameManager.ListStoreItems[itemNumber - 1].IsBuy = true;
                GameManager.ListPlayerOwningNumber[itemNumber - 1]++;
                Console.WriteLine("\n아이템을 구매했습니다!");
                Thread.Sleep(1000);
                GameManager.QuestController.OnItemPurchased();
            }

            else if (GameManager.ListStoreItems[itemNumber - 1].IsSold == true)
            {
                Console.WriteLine("\n이미 구매한 아이템입니다.");
                Thread.Sleep(1000);
                ShowItemsWithNumber();
            }

            else
            {
                Console.WriteLine("\n돈이 부족합니다.");
                Thread.Sleep(1000);
                ShowItemsWithNumber();
            }
        }

        public void ShowItemsWithNumber()
        {
            
            if (_saveUserInput == "1")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [ 상점 ]\n");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 구매할 수 있습니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{GameManager.PlayerController.Gold} G\n");

                Console.WriteLine("[아이템 목록]");

                foreach (ItemController item in GameManager.ListStoreItems)
                {
                    if (item.Remaining == 0)
                    {
                        item.IsSold = true;
                    }
                }

                int i = 1;
                foreach (ItemController item in GameManager.ListStoreItems)
                {

                    if (item.ItemType == "무기")
                    {
                        Console.Write($"{i} {item.Name}    | 회복량 +{item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}   | ");

                        if (item.IsSold)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[ 품절 ]");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.Price.ToString().PadLeft(4, ' ')} G  | 남은 수량 : {item.Remaining}");
                        }
                        Console.WriteLine();
                        i++;
                    }

                    if (item.ItemType == "방어구")
                    {
                        Console.Write($"{i} {item.Name}    | 회복량 +{item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}   | ");

                        if (item.IsSold)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[ 품절 ]");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.Price.ToString().PadLeft(4, ' ')} G  | 남은 수량 : {item.Remaining}");
                        }
                        Console.WriteLine();
                        i++;
                    }

                    if (item.ItemType == "포션")
                    {
                        Console.Write($"{i} {item.Name}    | 회복량 +{item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}   | ");

                        if (item.IsSold)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[ 품절 ]");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{item.Price.ToString().PadLeft(4, ' ')} G  | 남은 수량 : {item.Remaining}");
                        }
                        Console.WriteLine();
                        i++;
                    }
                }

                Console.WriteLine();
                Console.Write("0. 나가기\r\n\r\n구매하고자 하는 물품(번호)을 입력해주세요.\r\n\r\n>>");
            }
        }

        public void Exit()
        {
            string userInput = Console.ReadLine();
            if (userInput == "0")
            {
                Console.Clear();
            }
        }

        string _saveUserInput;
    }
}

