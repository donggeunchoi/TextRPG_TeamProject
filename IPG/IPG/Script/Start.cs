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
            MainTitle.Title();


            // 마을 (순환구조 남음)
            // 상점 (완)
            // 아이템 (완)
            // 인벤토리 (완)
            // 배틀컨트롤러 (완)
            // 배틀 매니저 (완)
            // 던전 로비 컨트롤러 (완)
            // 몬스터 컨트롤러 (완)
            // 인벤토리 (완)
        }
    }
}