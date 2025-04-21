using System;
using System.Threading;

namespace TextRPG
{
    internal class BattleSystem
    {
        class TurnSystem
        {
            //몬스터 정보를 참조 선언

            public void PlayerTurn()
            {
                Console.WriteLine("유저 공격 턴");
                Console.WriteLine($"1. [몬스터1] HP 100 "); // HP다음에 몬스터 HP 변수 입력
                Console.WriteLine($"2. [몬스터2] HP 100");
                Console.WriteLine($"3. [몬스터3] HP 100");
                Console.WriteLine($"4. [몬스터4]");
                Console.WriteLine("");
                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv.1 Chad(전사)"); // 플레이어 정보 입력
                Console.WriteLine($"HP 100/100");
                Console.WriteLine("");
                Console.WriteLine("0. 도망가기");
                Console.WriteLine("");
                Console.WriteLine("대상을 선택해주세요.");
                Console.Write(">>");

                PlayerAttack();
            }

            public void MonsterTurn()
            {
                Console.WriteLine("유저 수비 턴");
                Console.WriteLine("");
                Console.WriteLine("Battle!!");
                Console.WriteLine($"[몬스터1]의 공격!");
                Console.WriteLine($"Chad 을(를) 맞췄습니다.  [데미지 : 6]");
                // 플레이어 체력 - 몬스터 데미지

                Console.WriteLine("");
                Console.WriteLine($"[몬스터2] 의 공격!");
                Console.WriteLine($"Chad 을(를) 맞췄습니다.  [데미지 : 6]");
                // 플레이어 체력 - 몬스터 데미지

                Console.WriteLine("");
                Console.WriteLine($"[몬스터3] 의 공격!");
                Console.WriteLine($"Chad 을(를) 맞췄습니다.  [데미지 : 6]");
                // 플레이어 체력 - 몬스터 데미지

                Console.WriteLine("");
                Console.WriteLine($"[몬스터4] 의 공격!");
                Console.WriteLine($"Chad 을(를) 맞췄습니다.  [데미지 : 6]");
                // 플레이어 체력 - 몬스터 데미지

                Console.WriteLine("");
                Console.WriteLine($"Lv.1 Chad");
                Console.WriteLine($"HP 100 -> 76 (-24)"); // (총합 데미지)

                Console.WriteLine("");
                Console.WriteLine("0. 다음");
                Console.WriteLine("");
                Console.WriteLine("대상을 선택해주세요.");
                Console.Write(">>");
            }

            public void PlayerAttack()
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        // 마을로 도망가기
                        break;

                    case "1":
                        // 1번 몬스터 체력 - 플레이어 데미지
                        break;

                    case "2":
                        // 1번 몬스터 체력 - 플레이어 데미지
                        break;

                    case "3":
                        // 1번 몬스터 체력 - 플레이어 데미지
                        break;

                    case "4":
                        // 1번 몬스터 체력 - 플레이어 데미지
                        break;

                    default:
                        Console.WriteLine("해당하는 몬스터는 존재하지 않습니다.");
                        Console.WriteLine("숫자를 다시 선택해 주세요.");
                        Thread.Sleep(500);

                        Console.Clear();

                        PlayerTurn();
                        break;

                }
            }

            public void Turn()
            {
                int i = 0;

                do
                {
                    if (i % 2 == 0)
                    {
                        PlayerTurn();
                        if () // 남은 몬스터 수 <= 0
                        {
                            //승리 메시지
                        }
                        i++;
                    }

                    else
                    {
                        MonsterTurn();
                        i++;
                        if () // 플레이어 체력 <= 0
                        {
                            //패배 메시지
                        }
                    }

                } while (i <= 5); // 원래는 플레이어 체력 <= 0 || 남은 몬스터 <= 0
            }

        }

        static void Main(string[] args)
        {
            TurnSystem t = new TurnSystem();
            t.Turn();
        }
    }
}