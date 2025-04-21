using System;

// 네임스페이스 -> 프로그램 클래스 안에 Monster[] monsters 필드 선언, 몬스터 클래스는 프로그램 클래스 밖에 따로 생성

namespace MyApp
{
    internal class Program
    {
        static Monster[] monsters = new Monster[]
        {
            new Monster(2, "미니언", 15, 5),
            new Monster(3, "공허충", 10, 9),
            new Monster(5, "대포미니언", 25, 8)
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== 메인 화면 ===\n");
                Console.WriteLine("1. 모든 몬스터 정보 불러오기\n");
                Console.WriteLine("0. 종료하기\n");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.Clear();
                    foreach (var monster in monsters)
                    {
                        monster.ShowInfo();
                    }
                    Console.WriteLine("뒤로 가시려면 아무 키나 입력하세요.");
                    Console.Write(">> ");
                    Console.ReadKey(true);
                }
                else if (input == "0")
                {
                    Console.WriteLine("\n프로그램을 종료합니다.");
                    return;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.WriteLine("계속 하려면 아무 키나 누르세요.");
                    Console.Write(">> ");
                    Console.ReadKey(true);
                }
            }
        }
    }

    internal class Monster
    {
        public int Level;
        public string Name;
        public int Hp;
        public int Atk;

        public Monster(int level, string name, int hp, int atk)
        {
            Level = level;
            Name = name;
            Hp = hp;
            Atk = atk;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: Lv. {Level}, 체력: {Hp}, 공격력: {Atk}\n\n");
        }
    }
}
