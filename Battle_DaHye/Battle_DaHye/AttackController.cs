using System.Reflection.Emit;
using System.Xml.Linq;
using IPG;

internal class Program
{
    static PlayerController player = new PlayerController();
    static MonsterController[] monsters;

    // 공격 턴 UI
    static void ShowBattleAttack()
    {
        int input = -1;

        while (input != 0)
        {
            // 모든 몬스터가 죽었는지 확인
            bool allDead = true;
            foreach (var monster in monsters)
            {
                if (!monster.IsDead)
                {
                    allDead = false;
                    break;
                }
            }

            if (allDead)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("모든 몬스터를 처치했습니다! 전투 종료!");
                Console.ResetColor();
                break;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("My turn");
            Console.ResetColor();
            Console.WriteLine();

            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i].ShowMonsterInfo(i + 1);
            }

            player.ShowPlayerInfo();

            Console.WriteLine("\n0. 도망치기");
            Console.WriteLine("\n대상을 선택해주세요.");
            Console.Write(">> ");

            bool isValid = int.TryParse(Console.ReadLine(), out input);
            if (!isValid || input < 0 || input > monsters.Length)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
                return;
            }

            if (input == 0)
            {
                Console.WriteLine("전투에서 도망쳤습니다.");
                break;
            }

            // 공격 처리
            AttackMonster(input);
            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey(true);
        }
    }

    static void AttackMonster(int input)
    {
        MonsterController targetMonster = monsters[input - 1];

        if (targetMonster.IsDead)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("이미 처치한 몬스터입니다.");
            Console.ResetColor();
            return;
        }

        int minDamage = (int)Math.Ceiling(player.Atk * 0.9);
        int maxDamage = (int)Math.Ceiling(player.Atk * 1.1);

        Random random = new Random();
        int damage = random.Next(minDamage, maxDamage + 1);

        targetMonster.Hp -= damage;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{targetMonster.Name}에게 {damage}의 데미지를 입혔습니다!");
        Console.ResetColor();

        if (targetMonster.Hp <= 0)
        {
            targetMonster.Hp = 0;
            targetMonster.IsDead = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{targetMonster.Name}을(를) 처치했습니다!");
            Console.ResetColor();
        }
    }
}