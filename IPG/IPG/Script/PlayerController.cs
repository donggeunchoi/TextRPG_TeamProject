using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG
{
    internal class PlayerController
    {
        public int Level = 1;
        public string Name = "Chad";
        public string Job = "전사";
        public int Atk = 10;
        public int Def = 5;
        public int Hp = 100;
        public int Gold = 3500;

        public void Status()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("상태 보기");
                Console.ResetColor();
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                int bonusAtk = Atk - 10;
                int bonusDef = Def - 5;


                ShowPlayerInfo();

                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                string input = Console.ReadLine();

                if (input == "0")
                    return;
                else
                {
                    WrongInput();
                }
            }

        }
            public void ShowPlayerInfo()
        {
            int bonusAtk = Atk - 10;
            int bonusDef = Def - 5;

            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ( {Job} )");
            Console.WriteLine($"공격력 : {Atk} (+{bonusAtk})");
            Console.WriteLine($"방어력 : {Def} (+{bonusDef})");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G\n");
        }

        static void WrongInput()
        {
            Console.WriteLine("\n\a잘못된 입력입니다.");
            WaitInput();
        }

        static void WaitInput() // 아직 구현 안 했습니다에 쓰려고 WrongInput이랑 분리
        {
            Console.WriteLine("\n이전 화면으로 돌아가려면 아무 키나 누르세요.");
            Console.ReadKey(true);
        }
    }
}

