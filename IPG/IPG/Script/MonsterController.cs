using System;
using System.Collections.Generic;
using System.Reflection;

namespace IPG
{
    internal class MonsterController
    {
        public int Level;
        public string Name;
        public int Hp;
        public int Atk;
        public bool IsDead = false;

        public MonsterController GetMonsterType()
        {
            Random rand = new Random();
            int monsterType = rand.Next(0, GameManager.ListMonsters.Count);

            MonsterController baseMonster = GameManager.ListMonsters[monsterType];

            return new MonsterController
            {
                Level = baseMonster.Level,
                Name = baseMonster.Name,
                Hp = baseMonster.Hp,
                Atk = baseMonster.Atk,
                IsDead = false
            };
        }



        
        public void AddMonsterInfo(int level, string name, int hp, int atk, bool isDead)
        {
            GameManager.ListMonsters.Add(new MonsterController
            {
                Level = level,
                Name = name,
                Hp = hp,
                Atk = atk,
                IsDead = isDead
            });
        }
        public void SaveMonster()
        {
            if (GameManager.ListMonsters.Count > 0) return;
            AddMonsterInfo(1, "슬라임", 5, 5, false);
            AddMonsterInfo(2, "미니언", 15, 5, false);
            AddMonsterInfo(3, "공허충", 10, 9, false);
            AddMonsterInfo(4, "임규민짱", 5, 5, false);
            AddMonsterInfo(5, "대포미니언", 25, 8, false);
        }

       
    }
}