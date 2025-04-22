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

        public StoreController(PlayerController status)
        {
            _playerStatus = status;
        }

        public void Enter()
        {
            Console.Clear();
            Console.WriteLine("상점\r\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{_playerStatus.Gold} G\n");

            ShowItems();
            ShowItemsWithNumber();
            BuyItem();
        }

        private void AddItem(string itemName, bool itemKind, int itemEffect, string itemDesc, int itemPrice, bool itemIsSold)
        {
            StoreItems.Add(new ItemController
            {
                isWeapons = itemKind,
                Name = itemName,
                Effect = itemEffect,
                Desc = itemDesc,
                Price = itemPrice,
                isSold = itemIsSold
            });
        }

        public void SaveItem()
        {
            if (StoreItems.Count > 0) return;

            AddItem("수련자 갑옷    ", false, 5, " 수련에 도움을 주는 갑옷입니다.                   ", 1000, false);
            AddItem("무쇠갑옷       ", false, 9, " 무쇠로 만들어져 튼튼한 갑옷입니다.               ", 2000, true);
            AddItem("스파르타의 갑옷", false, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false);
            AddItem("낡은 검        ", true, 2, " 쉽게 볼 수 있는 낡은 검 입니다.                  ", 600, false);
            AddItem("청동 도끼      ", true, 5, " 어디선가 사용됐던거 같은 도끼입니다.             ", 1500, false);
            AddItem("스파르타의 창  ", true, 7, " 스파르타의 전사들이 사용했다는 전설의 창입니다.  ", 3000, true);
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

                Console.WriteLine($"- {item.Name}    | {(item.isWeapons ? "공격력" + item.Effect : "방어력" + item.Effect)}  | {item.Desc}       |  {(item.isSold ? "구매완료" : item.Price + " G")}");
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
            if (_playerStatus.Gold > StoreItems[itemNumber - 1].Price)
            {
                StoreItems[itemNumber - 1].isSold = true;
                _playerStatus.Gold -= StoreItems[itemNumber - 1].Price;

            }

            else if (StoreItems[itemNumber - 1].isSold == true)
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
                int i = 1;
                foreach (ItemController item in StoreItems)
                {

                    Console.WriteLine($"- {i} {item.Name}    | {(item.isWeapons ? "공격력" + item.Effect : "방어력" + item.Effect)}  | {item.Desc}       |  {(item.isSold ? "구매완료" : item.Price + " G")}");
                    i++;
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

