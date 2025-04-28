using IPG;
using System.Numerics;

namespace IPG
{
    internal class Start
    {
        static void Main(string[] args)
        {
            // 기본 객체 생성
            GameManager.Init();
            GameManager.MonsterController.SaveMonster();
            GameManager.StoreController.SaveItem();
            // #if DEBUG // 디버그 모드일 때만 아래 플레이어 필드대로 설정하고 마을 진입
            //            GameManager.PlayerController.Name = "디버그맨";
            //            GameManager.PlayerController.Job = "전사";
            //            GameManager.PlayerController.baseAtk = 12;
            //            GameManager.PlayerController.baseDef = 7;
            //            GameManager.PlayerController.maxHp = 120;
            //            GameManager.PlayerController.currentHp = 120;
            //            GameManager.VillageController.Enter();
            // #else // 릴리즈 버전에선 정상적으로 메인 타이틀 실행
            //            MainTitle.Title();
            // #endif
            MainTitle.Title();
        }
    }
}