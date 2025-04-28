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
            AddMonsterInfo(1, "세미콜론 빼먹기", 5, 5, false);
            AddMonsterInfo(2, "대소문자 헷갈리기", 7, 6, false);
            AddMonsterInfo(3, "괄호 짝이 안 맞음", 10, 7, false);
            AddMonsterInfo(4, "낯선 깃허브", 12, 8, false);
            AddMonsterInfo(5, "머지 컨플릭트", 15, 9, false);
        }
    }
}