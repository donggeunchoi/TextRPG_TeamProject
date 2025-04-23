using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace IPG
{
    internal class VillageController
    {
        private StoreController _store;
        private PlayerController _playerStatus;
        private InventoryController _inventory;
        private Battlecontroller _battleController;
        private BattleManager _battleManager;
        private DungeonLobbyController _dungeonLobby;

        public VillageController(StoreController Store, InventoryController inventory, PlayerController Status, Battlecontroller battleController, BattleManager battleManager, DungeonLobbyController dungeonLobby)
        {
            _store = Store;
            _playerStatus = Status;
            _inventory = inventory;
            _battleController = battleController;
            _battleManager = battleManager;
            _dungeonLobby = dungeonLobby;

        }

        public void Enter()
        {
            while (true) // 뒤로 가기 기능을 위해 항상 메서드를 실행시켜놓는 기능, 각 메서드마다 있음
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("0. 게임 종료\n");
                Console.Write("원하시는 행동을 입력해주세요.\n\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _playerStatus.Status();
                        break;

                    case "2":
                        _inventory.Enter();
                        break;

                    case "3":
                        _store.Enter();
                        break;

                    case "4":
                        _dungeonLobby.EnterDungeonLobby();
                        break;

                    case "0":
                        Console.WriteLine("다음에 또 만나요");
                        Environment.Exit(0);
                        break;
                    default:
                        WrongInput();
                        break;
                }
            }
        }

        static void WrongInput()
        {
            Console.WriteLine("\n\a잘못된 입력입니다.");
            WaitInput();
        }

        static void WaitInput() // 아직 구현 안 했습니다에 쓰려고 WrongInput이랑 분리
        {
            Console.WriteLine("\n이전 화면으로 돌아가려면 아무 키나 누르세요.");
            Console.ReadKey(true);
        }
    }

}
