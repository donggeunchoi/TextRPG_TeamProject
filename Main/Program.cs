using System.Numerics;

namespace Main
{
    internal class Program
    {
        static Player player = new Player();

        static void Main(string[] args)
        {
            while (true) // 뒤로 가기 기능을 위해 항상 메서드를 실행시켜놓는 기능, 각 메서드마다 있음
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.WriteLine("0. 게임 종료\n");
                Console.Write("원하시는 행동을 입력해주세요.\n\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Status();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("여기에다가 전투 시작 메서드 연결하면 됩니다.");
                        WrongInput();
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

        static void Status()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("상태 보기");
                Console.ResetColor();
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                int bonusAtk = 0;
                int bonusDef = 0;

                //아래는 장비 아이템으로 추가된 스탯을 보여주는 함수입니다. 현재 아이템, 인벤토리 시스템 아직 구현 안 됐기 때문에 임시 주석처리 했습니다.
                //foreach (var item in player.Equipped)
                //{
                //    if (item != null)
                //    {
                //        bonusAtk += item.Atk;
                //        bonusDef += item.Def;
                //    }
                //}

                Console.WriteLine($"Lv. {player.Level:D2}");
                Console.WriteLine($"{player.Name} ( {player.Job} )");
                Console.WriteLine($"공격력 : {player.Atk} (+{bonusAtk})");
                Console.WriteLine($"방어력 : {player.Def} (+{bonusDef})");
                Console.WriteLine($"체력 : {player.Hp}");
                Console.WriteLine($"Gold : {player.Gold} G\n");
                Console.Write("0. 나가기\n\n");
                Console.Write("원하시는 행동을 입력해주세요.\n\n>> ");

                string input = Console.ReadLine();

                if (input == "0")
                    return;
                else
                {
                    WrongInput();
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

    internal class Player
    {
        public int Level = 1
        ;
        public string Name = "Chad";
        public string Job = "전사";
        public int Atk = 10;
        public int Def = 5;
        public int Hp = 100;
        public int Gold = 1500;
    }
}
