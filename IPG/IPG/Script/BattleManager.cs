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
            if (CurrentMonsters.All(monster => monster.IsDead))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("모든 몬스터를 처치했습니다! 전투 종료!");
                Console.ResetColor();
                GameManager.BattleController.Battlevictory();
                return; 
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ My turn ]\n");
            Console.ResetColor();
            Console.WriteLine();

            ShowDungeonMonster();

            Console.WriteLine("\n\n");
            GameManager.PlayerController.ShowPlayerInfo();

            Console.WriteLine("\n0. 도망치기");
            Console.WriteLine("\n대상을 선택해주세요.");
            Console.Write(">> ");

            string result = Console.ReadLine();
            bool isValid = int.TryParse(result, out int input);

            
            if (isValid == false || input < 0 || input > CurrentMonsters.Count)
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
            Console.WriteLine($"[{GameManager.PlayerController.Name}의 공격!]\n");
            Console.ResetColor();
            Console.WriteLine($"{targetMonster.Name}에게 {damage}의 데미지를 입혔습니다!\n");

            if (targetMonster.Hp <= 0)
            {
                targetMonster.Hp = 0;
                targetMonster.IsDead = true;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{targetMonster.Name}을(를) 처치했습니다!\n");
                Console.ResetColor();
            }

            
            bool anyAlive = CurrentMonsters.Any(monster => !monster.IsDead);
            if (anyAlive)
            {
                ShowDungeonMonster();
                Console.WriteLine("\n\n아무 키나 눌러 진행하세요.\n>>");
                Console.ReadKey();

                MonsterAttackPhase();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("모든 몬스터를 처치했습니다! 전투 종료!");
                Console.ResetColor();
                GameManager.BattleController.Battlevictory();
            }
        }

        public static void MonsterAttackPhase()
        {
            int currentHp = GameManager.PlayerController.currentHp;
            int maxHp = GameManager.PlayerController.maxHp;

            int monsterIndex = 0;
            string playerName = GameManager.PlayerController.Name;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 몬스터가 공격해옵니다. ]\n");
            Console.ResetColor();

            foreach (var dungeonMonster in CurrentMonsters)
            {
                if (dungeonMonster.IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\nLv.{dungeonMonster.Level} {dungeonMonster.Name}은(는) 이미 쓰러졌습니다.");
                    Console.ResetColor();
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n - Lv.{dungeonMonster.Level} {dungeonMonster.Name}의 공격!");
                Console.ResetColor();
                currentHp -= dungeonMonster.Atk;
                Console.WriteLine($" {playerName} 을(를) 맞췄습니다. [데미지: {dungeonMonster.Atk}]\n\n");
                
            }

            GameManager.PlayerController.currentHp = currentHp;

            if (currentHp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n플레이어가 사망했습니다...\n");
                Console.ResetColor();

                Console.WriteLine("0. 메뉴로 돌아가기");
                string defeat = Console.ReadLine();
                if (defeat == "0")
                {
                    GameManager.BattleController.BattleLose();
                }
            }
            else
            {
                Console.WriteLine($"{playerName} HP: {currentHp}/{maxHp}\n");
                Console.WriteLine("\n아무 키나 눌러 공격하세요.");
                Console.ReadKey();

                PlayerAttackPhase(); 
            }
        }
    
    }
}