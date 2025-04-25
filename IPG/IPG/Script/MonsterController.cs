using System;
using System.Collections.Generic;

namespace IPG
{
    internal class MonsterController
    {
        public int Level;
        public string Name;
        public int Hp;
        public int Atk;
        public bool IsDead = false;
        public bool IsBoss = false;

        private List<int> _saveType = new List<int>();
        private int _saveMonsterNumber;

       
        
        public void RandomMonsterType(int index, int floor)
        {
            Random rand = new Random();
            _saveType.Clear();

            int dungeonFloor = DungeonLobbyController.CurrentFloor;

            if (dungeonFloor == 3)
            {
                _saveType.Add(5);
                _saveMonsterNumber = 1;
                return;
            }

            for (int i = 0; i < index; i++)
            {
                int monsterType = rand.Next(0, GameManager.ListMonsters.Count - 1);
                _saveType.Add(monsterType);
            }

            _saveMonsterNumber = index;

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
            AddMonsterInfo(6, "파멸의 I", 50, 10, false);
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