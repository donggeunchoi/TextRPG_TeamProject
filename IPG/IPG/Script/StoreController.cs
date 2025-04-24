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
        PlayerController _playerStatus;
        public List<ItemController> StoreItems = new List<ItemController>();
        public List <int> PlayerOwningNumber = new List<int>();

        public StoreController(PlayerController status)
        {
            _playerStatus = status;
        }

        public void Enter()
        {
            Console.Clear();
            Console.WriteLine(" [ 상점 ]\n");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 구매할 수 있습니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{_playerStatus.Gold} G\n");

            ShowItems();
            ShowItemsWithNumber();
            BuyItem();
        }

        private void AddItem(string itemName, string itemType, int itemEffect, string itemDesc, int itemPrice, int remaining, bool itemIsSold, bool isBuy)
        {
            StoreItems.Add(new ItemController
            {
                ItemType = itemType,
                Name = itemName,
                Effect = itemEffect,
                Desc = itemDesc,
                Price = itemPrice,
                Remaining = remaining,
                IsSold = itemIsSold,
                IsBuy = isBuy
            });
        }

        public void SaveItem()
        {
            if (StoreItems.Count > 0) return;

            AddItem("수련자 갑옷    ", "방어구", 5, " 수련에 도움을 주는 갑옷입니다.                   ", 1000, 1, false, false);
            AddItem("무쇠갑옷       ", "방어구", 9, " 무쇠로 만들어져 튼튼한 갑옷입니다.               ", 2000, 0, true, true);
            AddItem("스파르타의 갑옷", "방어구", 15, " 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, 1, false, false);
            AddItem("낡은 검        ", "무기", 2, " 쉽게 볼 수 있는 낡은 검 입니다.                  ", 600, 1, false, false);
            AddItem("청동 도끼      ", "무기", 5, " 어디선가 사용됐던거 같은 도끼입니다.             ", 1500, 1, false, false);
            AddItem("스파르타의 창  ", "무기", 7, " 스파르타의 전사들이 사용했다는 전설의 창입니다.  ", 3000, 0, true, true);
            AddItem("중급 회복 포션 ", "포션", 30, " 연금술사가 나름의 심혈을 기울여 만든 포션입니다. ", 800, 3, false, false);


            foreach (ItemController item in StoreItems)
            {
                if (item.IsSold && item.Remaining == 0)
                {
                    PlayerOwningNumber.Add(1);
                }
                else
                {
                    PlayerOwningNumber.Add(0);
                }
            }
        }

        public void BuyItem()
        {
            bool stopBuyItems = false;


            while (stopBuyItems == false && _saveUserInput == "1")
            {
                string choiceBuyItem = Console.ReadLine();
                Console.Clear();

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
            Console.WriteLine("[아이템 목록]");

            foreach (ItemController item in StoreItems)
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

            foreach (ItemController item in StoreItems)
            {
                if(item.ItemType == "무기")
                {
                    Console.WriteLine($"- {item.Name}    | 공격력 +{item.Effect.ToString().PadLeft(2, ' ')} " +
                                      $"| {item.Desc}   |  {(item.IsSold ? "  품절" : item.Price.ToString().PadLeft(4, ' ') + " G  | 남은 수량 : " + item.Remaining)}");
                }

                if (item.ItemType == "방어구")
                {
                    Console.WriteLine($"- {item.Name}    | 방어력 +{item.Effect.ToString().PadLeft(2, ' ')} " +
                                      $"| {item.Desc}   |  {(item.IsSold ? "  품절" : item.Price.ToString().PadLeft(4, ' ') + " G  | 남은 수량 : " + item.Remaining)}");
                }

                if (item.ItemType == "포션")
                {
                    Console.WriteLine($"- {item.Name}    | 회복량 +{item.Effect.ToString().PadLeft(2, ' ')} " +
                                      $"| {item.Desc}   |  {(item.IsSold ? "  품절" : item.Price.ToString().PadLeft(4, ' ') + " G  | 남은 수량 : " + item.Remaining)}");
                }

            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.Write("0. 나가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>>");

            string buyItem = Console.ReadLine();
            _saveUserInput = buyItem;

            if (_saveUserInput != "0" && _saveUserInput != "1")
            {
                Console.Write("해당하는 숫자를 입력하세요.");
                Thread.Sleep(500);

                Enter();
                ShowItems();

            }
        }

        public void CompareWithMoney(int itemNumber)
        {
            if (_playerStatus.Gold >= StoreItems[itemNumber - 1].Price && StoreItems[itemNumber - 1].IsSold == false)
            {
                _playerStatus.Gold -= StoreItems[itemNumber - 1].Price;
                StoreItems[itemNumber - 1].Remaining--;

                StoreItems[itemNumber - 1].IsBuy = true;
                PlayerOwningNumber[itemNumber - 1]++;
            }

            else if (StoreItems[itemNumber - 1].IsSold == true)
            {
                ShowItemsWithNumber();
                Console.WriteLine("이미 구매하셨습니다.");
                Thread.Sleep(1000);
            }

            else
            {
                ShowItemsWithNumber();
                Console.WriteLine("돈이 부족합니다.");
                Thread.Sleep(1000);
            }
        }

        public void ShowItemsWithNumber()
        {
            Console.Clear();

            if (_saveUserInput == "1")
            {
                Console.Clear();
                Console.WriteLine("상점\r\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{_playerStatus.Gold} G\n");

                Console.WriteLine("[아이템 목록]");

                foreach (ItemController item in StoreItems)
                {
                    if (item.Remaining == 0)
                    {
                        item.IsSold = true;
                    }
                }

                int i = 1;
                foreach (ItemController item in StoreItems)
                {

                    if (item.ItemType == "무기")
                    {
                        Console.WriteLine($"{i} {item.Name}    | 공격력 +{item.Effect.ToString().PadLeft(2, ' ')} " +
                                          $"| {item.Desc}   |  {(item.IsSold ? "  품절" : item.Price.ToString().PadLeft(4, ' ') + " G  | 남은 수량 : " + item.Remaining)}");
                        i++;
                    }

                    if (item.ItemType == "방어구")
                    {
                        Console.WriteLine($"{i} {item.Name}    | 방어력 +{item.Effect.ToString().PadLeft(2, ' ')} " +
                                          $"| {item.Desc}   |  {(item.IsSold ? "  품절" : item.Price.ToString().PadLeft(4, ' ') + " G  | 남은 수량 : " + item.Remaining)}");
                        i++;
                    }

                    if (item.ItemType == "포션")
                    {
                        Console.WriteLine($"{i} {item.Name}    | 회복량 +{item.Effect.ToString().PadLeft(2, ' ')} " +
                                          $"| {item.Desc}   |  {(item.IsSold ? "  품절" : item.Price.ToString().PadLeft(4, ' ') + " G  | 남은 수량 : " + item.Remaining)}");
                        i++;
                    }
                }

                Console.WriteLine();
                Console.Write("0. 나가기\r\n\r\n구매하고자 하는 물품(번호)을 입력해주세요.\r\n>>");
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

