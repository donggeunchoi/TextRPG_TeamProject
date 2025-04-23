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
            foreach (ItemController item in _store.StoreItems)
            {
                if (item.isSold)
                {
                    Console.WriteLine($"- {item.Name}    | {(item.isWeapons ? "공격력" + item.Effect : "방어력" + item.Effect)}  | {item.Desc}");
                }
            }
        }

        public void EquipManagement()
        {
            int i = 0;

            Dictionary<int, ItemController> itemDictionaty = new Dictionary<int, ItemController>();

            Console.Clear();
            Console.WriteLine(" [ 장착 관리 ]\n");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 장착/해제 할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");

            foreach (ItemController item in _store.StoreItems)
            {
                if (item.isSold)
                {
                    i++;
                    item.number = i;
                    itemDictionaty[i] = item;

                    string equippedMark = item.isUse ? "[E]" : "";

                    Console.WriteLine($"{item.number} {equippedMark} {item.Name}    | {(item.isWeapons ? "공격력" + item.Effect : "방어력" + item.Effect)}  | {item.Desc}");
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

                    if (selectedItem.isWeapons)
                    {
                        _playerStatus.baseAtk += selectedItem.Effect;
                    }
                    else
                    {
                        _playerStatus.baseDef += selectedItem.Effect;
                    }
                }

                else
                {
                    Console.WriteLine($"\n{selectedItem.Name} 장착 해제");

                    if (selectedItem.isWeapons)
                    {
                        _playerStatus.baseAtk -= selectedItem.Effect;
                    }
                    else
                    {
                        _playerStatus.baseDef -= selectedItem.Effect;
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
        public List<ItemController> GetPlayerItems()
        {
            return _store.StoreItems.Where(item => item.isSold && item.isUse).ToList();
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
