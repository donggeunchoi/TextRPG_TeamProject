using System;
using System.Reflection.Metadata.Ecma335;

namespace IPG
{
    internal class Ending
    {
        public static void SlowPrint(string text, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        public static void ShowEnding()
        {

            Console.Clear();
            SlowPrint("…\n", 100);
            SlowPrint("거대한 어둠이 무너지고, 세상에 다시 빛이 찾아왔다.\n", 70);
            SlowPrint($"{GameManager.PlayerController.Name}은(는) 무거운 검을 내려놓고 하늘을 올려다본다.\n", 70);
            SlowPrint("\n'이제, 진짜 평화가 오는 걸까...'\n", 100);

            Thread.Sleep(1500);
            Console.Clear();

            SlowPrint("[빛을 찾은 마을].\n", 70);
            SlowPrint($"사람들은 {GameManager.PlayerController.Name}의 이름을 노래하며 거리를 가득 메웠다.\n", 70);
            SlowPrint($"어린 아이들은 {GameManager.PlayerController.Name}을(를) 영웅이라 부르고,\n", 70);
            SlowPrint("마을 어른들은 다시는 어둠이 오지 않기를 기도했다.\n", 70);

            Thread.Sleep(1500);
            Console.Clear();

            SlowPrint($"하지만, {GameManager.PlayerController.Name}은(는) 조용히 그 자리를 떠났다.\n", 70);
            SlowPrint("그는 명예도, 보상도 바라지 않았다.\n", 70);
            SlowPrint($"\n{GameManager.PlayerController.Name}에게 남은 것은...\n", 100);
            SlowPrint("오직 또 다른 여정을 향한 갈망뿐이었다.\n", 100);

            Thread.Sleep(2000);
            Console.Clear();

            SlowPrint("\n\n그리고...\n", 150);
            SlowPrint("또 다른 모험이, 그를 기다리고 있었다.\n", 100);

            Console.ReadLine();

            Console.Clear();
            Console.WriteLine(" [ 에필로그 ] ");

            Console.ReadLine();
            GameManager.VillageController.Enter();
        }

        public static void LastQuest()
        {
            TypeEffect($"\n고맙네! {GameManager.PlayerController.Name}, 자네 덕분에 드디어 IPG 세상은 평화를 되찾았소.");
            Thread.Sleep(1000);
            TypeEffect("\n지금까지 수고 많았네.");
            Thread.Sleep(1000);
            TypeEffect("\n보상은 이것이오.");
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.ReadKey(true);
            ShowCredits();
        }

        public static void ShowCredits()
        {
            Console.Clear();
            List<string> credits = new List<string>
            {
                "===== Staff =====",
                "퀘스트 진행 및 발표자 : 임 규민",
                "메인 타이틀 및 연장자 : 최 다혜",
                "와이어 프레임 및 팀장 : 최 동근",
                "포션 보상 및 리엑션  : 이 광민",
                "각종 디버깅 및 막내  : 박 원희",
                "아이템 : 박 원희, 임 규민, 이 광민",
                "상점 : 박 원희",
                "인벤토리 : 박 원희",
                "전투 시스템 : 최 다혜, 이 광민, 최 동근",
                "몬스터 : 박 원희, 임 규민, 최 동근",
                "애니메이션 : 임 규민, 최 다혜",
                "",
                "===== Special Thanks =====",
                "모든 도움을 주신 튜터님들",
                "I0조의 열정과 노력",
                "",
                "끝까지 플레이해주셔서 감사합니다!",
                "THE END"
            };

            foreach (string line in credits)
            {
                Console.WriteLine(line);
                Thread.Sleep(1000); // 한 줄당 1초 딜레이
            }

            Console.WriteLine("\n\n타이틀로 돌아가기");
            Console.ReadLine();
            MainTitle.Title();
        }

        static void TypeEffect(string text, int delay = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}