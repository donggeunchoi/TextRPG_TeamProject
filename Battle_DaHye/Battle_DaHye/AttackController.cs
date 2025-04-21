using IPG;

internal class Program
{
    static PlayerController player = new PlayerController();

    static MonsterController[] monsters = new MonsterController[]
    {
        new MonsterController(2, "미니언", 15, 5),
        new MonsterController(3, "공허충", 10, 9),
        new MonsterController(5, "대포미니언", 25, 8)
    };

    static void AttackMonster(int input)
    {
        if (input < 1 || input > monsters.Length)
        {
            Console.WriteLine("잘못된 입력입니다.");
            return;
        }

        MonsterController targetMonster = monsters[input - 1];

        if (targetMonster.IsDead)
        {
            Console.WriteLine("이미 죽은 몬스터입니다.");
            return;
        }

        // 최소 및 최대 데미지 계산
        int minDamage = (int)Math.Ceiling(player.Atk * 0.9);
        int maxDamage = (int)Math.Ceiling(player.Atk * 1.1);

        // 데미지 랜덤 생성
        Random randomdamage = new Random();
        int damage = randomdamage.Next(minDamage, maxDamage + 1);

        targetMonster.Hp -= damage;
        Console.WriteLine($"{targetMonster.Name}에게 {damage}의 데미지를 입혔습니다!");

        if (targetMonster.Hp <= 0)
        {
            targetMonster.Hp = 0;
            targetMonster.IsDead = true;
            Console.WriteLine($"{targetMonster.Name}을(를) 처치했습니다!");
        }
    }
}