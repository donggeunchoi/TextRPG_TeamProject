using System;
using System.Collections.Generic;

// 네임스페이스 -> 프로그램 클래스 안에 Monster[] monsters 필드 선언, 몬스터 클래스는 프로그램 클래스 밖에 따로 생성
namespace IPG
{
    internal class ControlMonster
    {

        public List<MonsterController> monsters = new List<MonsterController> // 몬스터 도감
        {
            new MonsterController(1, "슬라임", 5, 5),
            new MonsterController(2, "미니언", 15, 5),
            new MonsterController(3, "공허충", 10, 9),
            new MonsterController(4, "임규민", 5, 5),
            new MonsterController(5, "대포미니언", 25, 8)
        };
    }

    internal class MonsterController
    {
        public int Level;
        public string Name;
        public int Hp;
        public int Atk;
        public bool IsDead = false;

        public MonsterController(int level, string name, int hp, int atk) // 몬스터 생성틀
        {
            Level = level;
            Name = name;
            Hp = hp;
            Atk = atk;
        }

        public MonsterController(MonsterController original) // 몬스터 복사틀 (실제로 우리가 보게 되는 몬스터)
        {
            Level = original.Level;
            Name = original.Name;
            Hp = original.Hp;
            Atk = original.Atk;
            IsDead = false;
        }

        // Index 받는 메서드
        public void ShowMonsterInfo(int index)
        {
            if (Hp <= 0)
            {
                Hp = 0;
                IsDead = true;
            }

            if (IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{index}. Lv {Level} [{Name}]  HP: Dead");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"{index}. Lv {Level} [{Name}]  HP: {Hp}");
            }
        }
    }
}
