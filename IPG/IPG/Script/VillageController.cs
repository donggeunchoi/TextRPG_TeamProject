using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace IPG
{
    internal class VillageController
    {
        public void Enter()
        {
            while (true) // 뒤로 가기 기능을 위해 항상 메서드를 실행시켜놓는 기능, 각 메서드마다 있음
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [ 마을 ]\n");
                Console.ResetColor();
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 모험가 조합");
                Console.WriteLine("5. 던전 입장");
                Console.WriteLine("0. 게임 종료\n");
                Console.Write("원하시는 행동을 입력해주세요.\n\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GameManager.PlayerController.Status();
                        break;

                    case "2":
                        GameManager.InventoryController.Enter();
                        break;

                    case "3":
                        GameManager.StoreController.Enter();
                        break;

                    case "4":
                        GameManager.QuestController.EnterQuestBoard();
                        break;

                    case "5":
                        GameManager.DungeonLobbyController.EnterDungeonLobby();
                        break;

                    case "0":
                        Console.WriteLine("다음에 또 만나요");
                        Environment.Exit(0);
                        break;
                    default:
                        WrongInput();
                        break;
                }
            }
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
