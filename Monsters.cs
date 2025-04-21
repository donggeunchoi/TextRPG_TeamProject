using System;

// 네임스페이스 -> 프로그램 클래스 안에 Monster[] monsters 필드 선언, 메인 몬스터 클래스는 프로그램 클래스 밖에 따로 생성

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
