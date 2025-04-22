using IPG;

internal class Program
{
    static PlayerController player = new PlayerController();
    static MonsterController[] monsters;

    static void AttackMonster(int input)
    {
        if (input < 1 || input > monsters.Length)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("잘못된 입력입니다.");
            Console.ResetColor();
            return;
        }

        MonsterController targetMonster = monsters[input - 1];

        if (targetMonster.IsDead)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("이미 처치한 몬스터입니다.");
            Console.ResetColor();
            return;
        }

        // 최소 및 최대 데미지 계산
        int minDamage = (int)Math.Ceiling(player.Atk * 0.9);
        int maxDamage = (int)Math.Ceiling(player.Atk * 1.1);

        // 데미지 랜덤 생성
        Random randomdamage = new Random();
        int damage = randomdamage.Next(minDamage, maxDamage + 1);

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