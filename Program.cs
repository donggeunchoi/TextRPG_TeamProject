using System.Numerics;
using System.Threading;

namespace IPG
{
    internal class Program
    {
        static PlayerController player = new PlayerController();
        static MonsterController[] monsters;


        static void Main(string[] args)
        {

            int monsterIndex = 0;


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

                if (monster.Hp <= 0)
                {
                    monster.Hp = 0;
                 
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\nLv.{monster.Level} {monster.Name}은(는) 이미 쓰러졌습니다.");
                    Console.ResetColor();
                    continue;
                }


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
}
