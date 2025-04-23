using System;
using System.Collections.Generic;

// 네임스페이스 -> 프로그램 클래스 안에 Monster[] monsters 필드 선언, 몬스터 클래스는 프로그램 클래스 밖에 따로 생성
namespace IPG
{
    internal class ControlMonster
    {

        public List<MonsterController> monsters = new List<MonsterController>
        {
            new MonsterController(2, "미니언", 15, 5),
            new MonsterController(3, "공허충", 10, 9),
            new MonsterController(5, "대포미니언", 25, 8)
        };
    }

    internal class MonsterController
    {
        public int Level;
        public string Name;
        public int Hp;
        public int Atk;

        public MonsterController(int level, string name, int hp, int atk)
        {
            Level = level;
            Name = name;
            Hp = hp;
            Atk = atk;
        }

        // Index 받는 메서드
        public bool IsDead = false;

        public void ShowMonsterInfo(int index)
        {
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
