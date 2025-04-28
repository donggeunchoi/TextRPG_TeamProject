using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace IPG
{
    internal class InventoryController
    {
        public void Enter()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 인벤토리 ]\n");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            ItemList();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            Exit();
        }

        public void ItemList()
        {
            for (int i = 0; i < GameManager.ListStoreItems.Count; i++)
            {
                ItemController item = GameManager.ListStoreItems[i];

                if (item.IsBuy)
                {
                    if (item.ItemType == "무기")
                    {
                        Console.WriteLine($"- {item.Name}    | 공격력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");
                    }
                    else if (item.ItemType == "방어구")
                    {
                        Console.WriteLine($"- {item.Name}    | 방어력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");
                    }
                    else if (item.ItemType == "포션")
                    {
                        Console.WriteLine($"- {item.Name}    | 회복량 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");
                    }   
                }
            }

        }

        public void EquipManagement()
        {
            Dictionary<int, ItemController> itemDictionaty = new Dictionary<int, ItemController>();

            Console.Clear();
            Console.WriteLine(" [ 장착 관리 ]\n");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 장착/해제 할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");


            int displayIndex = 1;

            for (int i = 0; i < GameManager.ListStoreItems.Count; i++)
            {
                ItemController item = GameManager.ListStoreItems[i];

                if (item.IsBuy && item.ItemType == "무기")
                {
                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E]" : "";
                    Console.WriteLine($"{displayIndex} {equippedMark} {item.Name}    | 공격력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");

                    displayIndex++;
                }

                if (item.IsBuy && item.ItemType == "방어구")
                {
                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E]" : "";
                    Console.WriteLine($"{displayIndex} {equippedMark} {item.Name}    | 방어력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");

                    displayIndex++;
                }

                if (item.IsBuy && item.ItemType == "포션")
                {
                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E]" : "";
                    Console.WriteLine($"{displayIndex} {equippedMark} {item.Name}    | 회복력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");

                    displayIndex++;
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("장착/헤체 하고자 하는 장비를 선택해주세요.");
            Console.Write(">> ");

            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                Enter();
            }

            else if (int.TryParse(input, out int selectedNumber) && itemDictionaty.ContainsKey(selectedNumber))
            {
                ItemController selectedItem = itemDictionaty[selectedNumber];
                selectedItem.isUse = !selectedItem.isUse;

                Console.Clear();
                EquipManagement();
            }
            else
            {
                Console.WriteLine("해당하는 숫자를 입력하세요");
                Thread.Sleep(500);

                Console.Clear();
                EquipManagement();
            }


        }
        public List<ItemController> GetPlayerItems()
        {
            return GameManager.ListStoreItems.Where(item => item.IsBuy && item.isUse).ToList();
        }

        public void Exit()
        {
            string userInput = Console.ReadLine();

            if (userInput == "0")
            {
                Console.Clear();
            }
            else if (userInput == "1")
            {
                EquipManagement();
            }
            else
            {
                Console.Write("해당하는 숫자를 입력하세요.");
                Thread.Sleep(500);
                Enter();
            }


        }
    }
}
