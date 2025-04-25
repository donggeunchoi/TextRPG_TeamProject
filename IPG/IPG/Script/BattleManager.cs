using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions;
using System.Xml.Linq;

namespace IPG
{
    internal class BattleManager
    {
        public static MonsterController Monsters = new MonsterController();
        public static List <MonsterController> CurrentMonsters = new List <MonsterController> ();

        public static void DungeonMonster()
        {
            CurrentMonsters.Clear();

            Random rand = new Random ();
            int NumberOfMonster = rand.Next (1, 4);

            for (int i = 0; i < NumberOfMonster; i++)
            {
                CurrentMonsters.Add(Monsters.GetMonsterType());
            }

        }

        public static void ShowDungeonMonster()
        {
            int i = 1;

            foreach (MonsterController monster in CurrentMonsters)
            {
                if (monster.Hp <= 0)
                {
                    monster.Hp = 0;
                    monster.IsDead = true;
                }


                if (monster.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i}. Lv {monster.Level} [{monster.Name}]  HP: Dead");
                    Console.ResetColor();
                    i++;
                }
                else
                {
                    Console.WriteLine($"{i}. Lv {monster.Level} [{monster.Name}]  HP: {monster.Hp}");
                    i++;
                }
            }
        }

        public static void PlayerAttackPhase()
        {
            // 몬스터가 죽었는지 확인
            bool allDead = true;
            foreach (var monster in CurrentMonsters)
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

            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("My turn");
            Console.ResetColor();
            Console.WriteLine();

            ShowDungeonMonster();
            GameManager.PlayerController.ShowPlayerInfo();

            Console.WriteLine("\n0. 도망치기");
            Console.WriteLine("\n대상을 선택해주세요.");
            Console.Write(">> ");

            string result = Console.ReadLine();
            bool isValid = int.TryParse(result, out int input);

            
            if (isValid == false || input < 0 || input > GameManager.ListMonsters.Count)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
            }

            else if (input == 0)
            {
                Console.WriteLine("전투에서 도망쳤습니다.");
                GameManager.BattleController.Battlestart();

            }

            else
            {
                AttackMonster(input);
            }

        }

        static void AttackMonster(int input)
        {
            MonsterController targetMonster = CurrentMonsters[input - 1];

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

        }

        public static void MonsterAttackPhase()
        {
            var monster = GameManager.ListMonsters;
            int monsterIndex = 0;
            //int totaldamage = 0;

            int currentHp = GameManager.PlayerController.currentHp;
            int maxHp = GameManager.PlayerController.maxHp;

            string playerName = GameManager.PlayerController.Name;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("몬스터가 공격해옵니다.");
            Console.ResetColor();

            while (true)
            {
                if (monsterIndex >= GameManager.ListMonsters.Count)
                {
                    Console.WriteLine("\n0.다음");
                    PlayerAttackPhase();//플레이어턴 으로
                    break;
                }
                
                monsterIndex++;
            }

            foreach(var dungeonMonster in CurrentMonsters)
            {
                if (dungeonMonster.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\nLv.{dungeonMonster.Level} {dungeonMonster.Name}은(는) 이미 쓰러졌습니다.");
                    Console.ResetColor();

                }

                Console.WriteLine($"\nLv.{dungeonMonster.Level} {dungeonMonster.Name}의 공격!");
                currentHp -= dungeonMonster.Atk;
                Console.WriteLine($"{playerName} 을(를) 맞췄습니다. [데미지: {dungeonMonster.Atk}]");
                Console.WriteLine($"{playerName} HP: {currentHp}/{maxHp}");
            }

            if (currentHp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[플레이어가 사망했습니다...]\n");
                Console.ResetColor();

                Console.WriteLine("0. 메뉴로 돌아가기");
                string defeat = Console.ReadLine();
                if (defeat == "0")
                {
                    GameManager.BattleController.BattleLose();

                }
            }

        }
    }
}