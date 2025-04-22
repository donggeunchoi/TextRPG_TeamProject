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
        public int Gold = 1500;        
        
        public void ShowPlayerInfo()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[내 정보]");
            Console.ResetColor();
            Console.WriteLine($"Lv.{Level} {Name} ({Job})");
            Console.WriteLine($"체력: {Hp}/100");
            Console.WriteLine($"공격력: {Atk}");
            Console.WriteLine($"방어력: {Def}");
            Console.WriteLine($"골드: {Gold}");
        }
    }
}

