using System;
using System.Collections.Generic;
// 네임스페이스 -> 프로그램 클래스 안에 Monster[] monsters 필드 선언, 몬스터 클래스는 프로그램 클래스 밖에 따로 생성
namespace IPG
{
    internal class MonsterController
    {
        public int Level;
        public string Name;
        public int Hp;
        public int Atk;
        public bool IsDead = false;

        private int[] _saveType = new int[5];
        private int _saveMonsterNumber;

        public void RandomMonsterType(int index)
        {
            Random rand = new Random();
            for (int i = 0; i < index; i++)
            {
                int monsterType = rand.Next(0, GameManager.ListMonsters.Count);
                _saveType[i] = monsterType;
            }
            int monsterNumber = rand.Next(1, 4);
            _saveMonsterNumber = monsterNumber;

            // 타입    숫자
            // 0 1 2    3
        }  // 2 4 3
        

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
        
        
        // Index 받는 메서드
        public void ShowMonsterInfo()
        {
            
            for (int i = 0; i < _saveMonsterNumber; i++)
            {
                // 타입    숫자
                // 0 1 2    3
                // 2 4 3

                if (GameManager.ListMonsters[_saveType[i]].Hp <= 0)
                {
                    GameManager.ListMonsters[_saveType[i]].Hp = 0;
                    GameManager.ListMonsters[_saveType[i]].IsDead = true;
                }


                if (GameManager.ListMonsters[_saveType[i]].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i+1}. Lv {GameManager.ListMonsters[_saveType[i]].Level} [{GameManager.ListMonsters[_saveType[i]].Name}]  HP: Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i+1}. Lv {GameManager.ListMonsters[_saveType[i]].Level} [{GameManager.ListMonsters[_saveType[i]].Name}]  HP: {GameManager.ListMonsters[_saveType[i]].Hp}");
                }

            }
        }
    }
}