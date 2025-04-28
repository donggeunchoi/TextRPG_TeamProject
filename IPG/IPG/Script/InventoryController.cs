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
        Dictionary<int, ItemController> itemDictionaty = new Dictionary<int, ItemController>();

        public void Enter()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 인벤토리 ]\n");
            Console.ResetColor();

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");

            bool hasItems = false; // 보유 아이템이 있는지 체크
            for (int i = 0; i < GameManager.ListStoreItems.Count; i++)
            {
                if (GameManager.ListStoreItems[i].IsBuy && GameManager.ListPlayerOwningNumber[i] > 0)
                {
                    hasItems = true;
                    break;
                }
            }

            if (hasItems)
            {
                Console.WriteLine();
                ItemList();
            }
            else
            {
                Console.WriteLine("보유하고 있는 아이템이 없습니다.");
            }
            
            Console.WriteLine();
            Console.WriteLine("1. 아이템 장착/사용");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            Exit(hasItems);
        }

        public void Exit(bool hasItems)
        {
            string userInput = Console.ReadLine();

            if (userInput == "1" && hasItems)
                EquipManagement();

            else if (userInput == "1" && !hasItems)
            {
                Console.Write("보유하고 있는 아이템이 없습니다.");
                Thread.Sleep(500);
                Enter();
            }

            else if (userInput == "0")
                Console.Clear();

            else
            {
                Console.Write("해당하는 숫자를 입력하세요.");
                Thread.Sleep(500);
                Enter();
            }
        }

        public void ItemList()
        {
            for (int i = 0; i < GameManager.ListStoreItems.Count; i++)
            {
                ItemController item = GameManager.ListStoreItems[i];

                if (item.IsBuy && item.ItemType == "무기")
                {
                    if (item.isUse) Console.ForegroundColor = ConsoleColor.Green;
                    string equippedMark = item.isUse ? "[E] " : "";
                    Console.WriteLine($"- {equippedMark}{item.Name}    | 공격력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");
                    Console.ResetColor();
                }

                if (item.IsBuy && item.ItemType == "방어구")
                {
                    if (item.isUse) Console.ForegroundColor = ConsoleColor.Green;
                    string equippedMark = item.isUse ? "[E] " : "";
                    Console.WriteLine($"- {equippedMark}{item.Name}    | 방어력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");
                    Console.ResetColor();
                }

                if (item.IsBuy && item.ItemType == "포션")
                {
                    string equippedMark = item.isUse ? "[E] " : "";
                    Console.WriteLine($"- {equippedMark}{item.Name}    | 회복력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");
                }
            }
        }

        public void EquipManagement()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 인벤토리 ]\n");
            Console.ResetColor();
            Console.WriteLine("아이템을 장착 및 해제할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");


            int displayIndex = 1;

            for (int i = 0; i < GameManager.ListStoreItems.Count; i++)
            {
                ItemController item = GameManager.ListStoreItems[i];

                if (item.IsBuy && item.ItemType == "무기")
                {
                    if (item.isUse) Console.ForegroundColor = ConsoleColor.Green;

                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E] " : "";
                    Console.WriteLine($"{displayIndex}. {equippedMark}{item.Name}    | 공격력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");

                    Console.ResetColor();

                    displayIndex++;
                    GameManager.QuestController.OnEquipmentEquipped();
                }

                if (item.IsBuy && item.ItemType == "방어구")
                {
                    if (item.isUse) Console.ForegroundColor = ConsoleColor.Green;

                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E] " : "";
                    Console.WriteLine($"{displayIndex}. {equippedMark}{item.Name}    | 방어력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");

                    Console.ResetColor();

                    displayIndex++;
                    GameManager.QuestController.OnEquipmentEquipped();
                }

                if (item.IsBuy && item.ItemType == "포션")
                {
                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E] " : "";
                    Console.WriteLine($"{displayIndex}. {equippedMark}{item.Name}    | 회복력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {GameManager.ListPlayerOwningNumber[i]}");

                    displayIndex++;
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("아이템을 선택해주세요.");
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
                int itemIndex = GameManager.ListStoreItems.FindIndex(i => i == selectedItem);

                if (selectedItem.ItemType == "포션")
                {
                    GameManager.ListPlayerOwningNumber[itemIndex]--;

                    if (GameManager.ListPlayerOwningNumber[itemIndex] <= 0)
                    {
                        GameManager.ListPlayerOwningNumber[itemIndex] = 0;
                        selectedItem.IsBuy = false;
                    }

                    int beforeHp = GameManager.PlayerController.currentHp;

                    GameManager.PlayerController.currentHp += 30;
                    if (GameManager.PlayerController.currentHp > GameManager.PlayerController.maxHp)
                    {
                        GameManager.PlayerController.currentHp = GameManager.PlayerController.maxHp;
                    }
                    

                    Console.WriteLine($"\n{selectedItem.Name}을(를) 사용했습니다!");
                    Console.Write($"HP {beforeHp} → {GameManager.PlayerController.currentHp}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"(+30)");
                    Console.ResetColor();

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey();
                }
                else
                {
                    selectedItem.isUse = !selectedItem.isUse;
                }

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

        public void AddQuestRewardSpartaSword() // 퀘스트 보상용 아이템, 시연 때 쓰려고 만들었습니다.
        {
            const string itemName = "스파르타의 검";

            int idx = GameManager.ListStoreItems.FindIndex(i => i.Name == itemName);
            if (idx >= 0)
            {
                GameManager.ListPlayerOwningNumber[idx]++;
            }
            else
            {
                var sword = new ItemController
                {
                    Name = itemName,
                    ItemType = "무기",
                    Effect = 10,
                    Desc = "스파르타의 전사들이 사용했다는 전설의 검입니다. 아주 희귀합니다.",
                    Price = 3500,
                    Remaining = 0,
                    IsSold = true,
                    IsBuy = true,
                    isUse = false,
                    DropRate = 0.0
                };
                GameManager.ListStoreItems.Add(sword);
                GameManager.ListPlayerOwningNumber.Add(1);
            }
        }
    }
}
