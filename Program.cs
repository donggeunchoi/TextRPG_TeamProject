using System.Numerics;
using System.Threading;

namespace Main
{
    internal class Program
    {
        static Player player = new Player();

        static Monster[] monsters = new Monster[]
       {
            new Monster(2, "미니언", 15, 5),
            new Monster(3, "공허충", 10, 9),
            new Monster(5, "대포미니언", 25, 88)
       };
        static void Main(string[] args)
        {

            int monsterIndex = 0;
            int totaldamage = 0;

            Console.WriteLine("Battle!!");

            while (true)
            {
                if (monsterIndex >= monsters.Length)
                {
                    Console.WriteLine("\n0.다음");
                    //플레이어턴 으로
                    break;
                }

                var monster = monsters[monsterIndex];
                monsterIndex++;


                Console.WriteLine($"\nLv.{monster.Level} {monster.Name}의 공격!");
                player.Hp -= monster.Atk;
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지: {monster.Atk}]");
                Console.WriteLine($"{player.Name} HP: {player.Hp}/100");

                if (player.Hp <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[플레이어가 사망했습니다...]\n");
                    Console.ResetColor();

                    Console.WriteLine("0. 메뉴로 돌아가기");
                    string defeat = Console.ReadLine();
                    if (defeat == "0")
                    {
                        break; // 죽었을때 갈 화면으로
                    }
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

        
    }
    internal class Player
    {
        public int Level = 1;
        public string Name = "Chad";
        public string Job = "전사";
        public int Atk = 10;
        public int Def = 5;
        public int Hp = 100;
        public int Gold = 1500;
    }
}