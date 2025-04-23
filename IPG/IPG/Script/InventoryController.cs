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
        public List<ItemController> StoreItems = new List<ItemController>();
        private StoreController _store;
        private VillageController _village;
        private PlayerController _playerStatus;

        public InventoryController(StoreController Store, PlayerController Status)
        {
            _store = Store;
            _playerStatus = Status;
        }


        public void Enter()
        {
            Console.Clear();
            Console.WriteLine("인벤토리\r\n보유 중인 아이템을 관리할 수 있습니다.\n");
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
            for (int i = 0; i < _store.StoreItems.Count; i++)
            {
                ItemController item = _store.StoreItems[i];

                if (item.IsBuy)
                {
                    if (item.ItemType == "무기")
                    {
                        Console.WriteLine($"- {item.Name}    | 공격력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {_store.PlayerOwningNumber[i]}");
                    }
                    else if (item.ItemType == "방어구")
                    {
                        Console.WriteLine($"- {item.Name}    | 방어력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {_store.PlayerOwningNumber[i]}");
                    }
                    else if (item.ItemType == "포션")
                    {
                        Console.WriteLine($"- {item.Name}    | 회복량 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {_store.PlayerOwningNumber[i]}");
                    }
                }
            }

        }

        public void EquipManagement()
        {
            Dictionary<int, ItemController> itemDictionaty = new Dictionary<int, ItemController>();

            Console.Clear();
            Console.WriteLine("인벤토리\r\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");


            int displayIndex = 1;  // 사용자에게 보여줄 번호

            for (int i = 0; i < _store.StoreItems.Count; i++)
            {
                ItemController item = _store.StoreItems[i];

                if (item.IsBuy && item.ItemType == "무기")
                {
                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E]" : "";
                    Console.WriteLine($"{displayIndex} {equippedMark} {item.Name}    | 공격력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {_store.PlayerOwningNumber[i]}");

                    displayIndex++;
                }

                if (item.IsBuy && item.ItemType == "방어구")
                {
                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E]" : "";
                    Console.WriteLine($"{displayIndex} {equippedMark} {item.Name}    | 방어력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {_store.PlayerOwningNumber[i]}");

                    displayIndex++;
                }

                if (item.IsBuy && item.ItemType == "포션")
                {
                    itemDictionaty[displayIndex] = item;

                    string equippedMark = item.isUse ? "[E]" : "";
                    Console.WriteLine($"{displayIndex} {equippedMark} {item.Name}    | 회복력 + {item.Effect.ToString().PadLeft(2, ' ')}  | {item.Desc}  | 보유 수량: {_store.PlayerOwningNumber[i]}");

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

                if (selectedItem.isUse)
                {
                    Console.WriteLine($"\n{selectedItem.Name} 장착 완료");

                    if (selectedItem.ItemType == "무기")
                    {
                        _playerStatus.Atk += selectedItem.Effect;
                    }
                    else if (selectedItem.ItemType == "방어구")
                    {
                        _playerStatus.Def += selectedItem.Effect;
                    }
                }

                else
                {
                    Console.WriteLine($"\n{selectedItem.Name} 장착 해제");

                    if (selectedItem.ItemType == "무기")
                    {
                        _playerStatus.Atk -= selectedItem.Effect;
                    }
                    else if (selectedItem.ItemType == "방어구")
                    {
                        _playerStatus.Def -= selectedItem.Effect;
                    }
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
