using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Transactions;
using System.Xml.Linq;


namespace IPG
{
    internal class BattleManager
    {
        public static bool StartBattleAndCheckVictory()
        {
            PlayerAttackPhase();

            bool allDead = true;
            foreach (var monster in GameManager.ListMonsters)
            {
                if (!monster.IsDead)
                {
                    allDead = false;
                    break;
                }
            }
            return allDead;
        }

        public static void PlayerAttackPhase()
        {
            int input = -1;

            while (input != 0)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("My turn");
                Console.ResetColor();
                Console.WriteLine();

                GameManager.MonsterController.ShowMonsterInfo();

                GameManager.PlayerController.ShowPlayerInfo();

                Console.WriteLine("\n0. 도망치기");
                Console.WriteLine("\n대상을 선택해주세요.");
                Console.Write(">> ");

                bool isValid = int.TryParse(Console.ReadLine(), out input);
                if (!isValid || input < 0 || input > GameManager.ListMonsters.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    return;
                }

                if (input == 0)
                {
                    Console.WriteLine("전투에서 도망쳤습니다.");
                    GameManager.BattleController.Battlestart();
                    break;
                }

                // 모든 몬스터가 죽었는지 확인
                bool allDead = true;
                foreach (var monster in GameManager.ListMonsters)
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
                    GameManager.BattleController.Battlevictory();
                    break;
                }

                // 공격 처리
                AttackMonster(input);
                Console.WriteLine("계속하려면 아무 키나 누르세요.");
                Console.ReadKey(true);

            }
        }

        static void AttackMonster(int input)
        {
            MonsterController targetMonster = GameManager.ListMonsters[input - 1];

            if (targetMonster.IsDead)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("이미 처치한 몬스터입니다.");
                Console.ResetColor();
                return;
            }

            int minDamage = (int)Math.Ceiling(GameManager.PlayerController.baseAtk * 0.9);
            int maxDamage = (int)Math.Ceiling(GameManager.PlayerController.baseAtk * 1.1);

            Random random = new Random();
            int damage = random.Next(minDamage, maxDamage + 1);

            targetMonster.Hp -= damage;

            Console.Clear();
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
            if (targetMonster.Hp > 0)
            {
                MonsterAttackPhase();
            }

            Console.WriteLine("");

            GameManager.MonsterController.ShowMonsterInfo();

             // 모든 몬스터가 죽었는지 확인
                bool allDead = true;
                foreach (var monster in GameManager.ListMonsters)
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

                    GameManager.BattleController.Battlevictory();
                    // InitMonsters();

                    Console.WriteLine("계속하려면 아무 키나 눌러주세요");
                    Console.ReadKey(true);
                    // Environment.Exit(0);

                    return;
                }
        }

        static void MonsterAttackPhase()
        {

            int monsterIndex = 0;
            int totaldamage = 0;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("몬스터가 공격해옵니다.");
            Console.ResetColor();

            while (true)
            {
                if (monsterIndex >= GameManager.ListMonsters.Count)
                {
                    Console.WriteLine("\n0.다음");
                    //플레이어턴 으로
                    break;
                }

                var monster = GameManager.ListMonsters[monsterIndex];
                monsterIndex++;

                if (monster.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\nLv.{monster.Level} {monster.Name}은(는) 이미 쓰러졌습니다.");
                    Console.ResetColor();
                    continue;
                }

                
                Console.WriteLine($"\nLv.{monster.Level} {monster.Name}의 공격!");
                GameManager.PlayerController.Hp -= monster.Atk;
                Console.WriteLine($"{GameManager.PlayerController.Name} 을(를) 맞췄습니다. [데미지: {monster.Atk}]");
                Console.WriteLine($"{GameManager.PlayerController.Name} HP: {GameManager.PlayerController.Hp}");

                if (GameManager.PlayerController.Hp <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[플레이어가 사망했습니다...]\n");
                    Console.ResetColor();

                    Console.WriteLine("0. 메뉴로 돌아가기");
                    string defeat = Console.ReadLine();
                    if (defeat == "0")
                    {
                        GameManager.BattleController.BattleLose();
                        break; // 죽었을때 갈 화면으로
                    }
                }
            }
        }
    }
}