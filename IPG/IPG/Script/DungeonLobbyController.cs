using System;
namespace IPG
{
    internal class DungeonLobbyController
    {
        private int _unlockedFloor = 1;
        private readonly PlayerController _player;
        private readonly Battlecontroller _battleController;

        public DungeonLobbyController(PlayerController player, Battlecontroller battleController)
        {
            _player = player;
            _battleController = battleController;
        }
        
        static PlayerController player = new PlayerController();
        static    StoreController store = new StoreController(player);
        static    InventoryController inventory = new InventoryController(store, player);
        static    Battlecontroller battleController = new Battlecontroller();
        static   BattleManager battleManager = new BattleManager();
        static    DungeonLobbyController dungeonLobby = new DungeonLobbyController(player,battleController);
            

        VillageController village = new VillageController(store, inventory, player, battleController, battleManager,dungeonLobby);

        public void EnterDungeonLobby()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.ResetColor();
                Console.WriteLine("클리어한 층이 늘어날수록 더 높은 층이 열립니다.\n");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");

                for (int i = 1; i <= _unlockedFloor; i++)
                {
                    Console.WriteLine($"{i + 1}. {i}층 입장");
                }

                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();


                if (input == "1")
                {
                    _player.Status();
                    WaitInput();
                    continue;
                }
                else if (input == "0")
                {
                    village.Enter();
                }

                if (int.TryParse(input, out int selected) && selected >= 2 && selected <= _unlockedFloor + 1)
                {
                    int chosenFloor = selected - 1;
                    StartBattle(chosenFloor);
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    WaitInput();
                }
            }
        }

        private void StartBattle(int floor)
        {
            Console.Clear();
            Console.WriteLine($"{floor}층 전투를 시작합니다...\n");

            // 전투 시작
            _battleController.Battlestart();

            // 클리어 성공했다고 가정하고 다음 층 열기
            if (_unlockedFloor < floor + 1)
            {
                _unlockedFloor = floor + 1;
            }
                

            WaitInput();
        }

        private void WaitInput()
        {
            Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
            Console.ReadKey(true);
        }
    }
}
