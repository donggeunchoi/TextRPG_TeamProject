// 보스 정보
//2층 깨지면 열려야하고.
//필드 선언
//선언.
//배틀매니저 넘어오게 만들고.
using System;

namespace IPG
{
    internal class BossController : MonsterController
    {
        // ✅ 보스 정보 필드 선언
        public int Level { get; private set; }
        public string Name { get; private set; }
        public int Hp { get; private set; }
        public int Atk { get; private set; }
        public bool IsDead { get; set; } = false;

        // ✅ 보스 몬스터 선언 (생성자)
        public BossController()
        {
            Level = 6;
            Name = "최후의 I";
            Hp = 100;
            Atk = 20;
            IsDead = false;
        }

        // ✅ 보스 정보 출력 (BattleManager 등에서 호출용)
        public void DisplayBossInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[보스 몬스터 등장!]");
            Console.WriteLine($"Lv.{Level} {Name} | HP {Hp} | Atk {Atk}");
            Console.ResetColor();
        }

        // ✅ BattleManager 에 넘겨줄 수 있게 보스 객체 리턴 메서드
        public static BossController GetBoss()
        {
            return new BossController();
        }
    }
}
