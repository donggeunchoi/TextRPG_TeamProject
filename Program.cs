using Microsoft.VisualBasic;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Battle(Player player)
        {
            int monsterIndex = 0;
            int totaldamage = 0;


            while (true)
            {
                if (monsterIndex >= monsters.Length)
                {
                    //플레이어턴
                    continue;
                }

                var monster = monsters[monsterIndex];
                monsterIndex++;

                if (monster.IsDead)
                {
                    Console.WriteLine($"{monster.Name}은(는) 죽어서 공격할 수 없습니다.");
                    continue;
                }


                Console.WriteLine("Battle!!");


                Console.WriteLine($"\n[{monster.Name}의 차례]");
                Console.WriteLine(">> 다음을 누르세요...");
                Console.ReadLine();

              

                Console.WriteLine($"{monster.Name}의 공격! [데미지: {monster.Atk}]");
                


                Console.WriteLine("\n영웅이 아파합니다.\n");

                Console.WriteLine("\n[내 정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Class})");
                Console.WriteLine($"HP {player.Hp}/100 (-{monster.Atk})");

                if (player.Hp <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[플레이어가 사망했습니다...]");
                    Console.ResetColor();

                    Console.WriteLine("0. 메뉴로 돌아가기");
                    string defeat = Console.ReadLine();
                    if (defeat == "0")
                    {

                    }
                    else
                    {

                    }
                    return;
                }

                Console.WriteLine("\n0.다음");
            }
        }
    }
}
