using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG
{
    class PlayerController
    {
        public int Level = 1;
        public string Name;
        public string Job;
        public float baseAtk;
        public int baseDef;
        public int maxHp;
        public int currentHp;
        public int Gold = 1500;
        public int Exp = 0;

        private List<int> levelUpExp = new List<int> { 10, 35, 65, 100 };

        public PlayerController()
        {
        }

        public void StartStory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" IPG 세계로 이동중... ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n한때 평화롭던 제국에 어둠이 드리우고...");
            Console.WriteLine("당신은 이 세계를 구원할 영웅이 될 운명을 타고났습니다.");
            Console.WriteLine("\n영웅이시여... 당신의 이름은 무엇인가요?");
            Console.ResetColor();

            while (true)
            {
                Console.Write("\n내 이름 : ");
                string inputName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputName))
                {
                    Console.WriteLine("\n이름은 공백일 수 없습니다.");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n당신의 이름은 \"{inputName}\"... 맞습니까?");
                Console.ResetColor();

                Console.WriteLine("\n1. 맞아요");
                Console.WriteLine("2. 아니예요");

                Console.Write("\n>> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Name = inputName;
                    break;
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n이름을 다시 입력해주세요.\n");
                    continue;
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 다시 선택해주세요.\n");
                }

            }

            SelectJob();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("이제 험난한 여정을 뚫고 IPG를 구원해주세요...\n");
            Console.ResetColor();

            WaitInput();

            GameManager.VillageController.Enter();
        }

        public void SelectJob()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{Name}님, IPG에 오신걸 환영합니다."); //Imperium Pro Gloria
            Console.WriteLine($"당신이 어떤 사람인지 알려주십시오...\n");
            Console.ResetColor();
            WaitInput();

            Dictionary<string, int> jobScores = new Dictionary<string, int>
            {
                { "전사", 0 },
                { "창술사", 0 },
                { "도적", 0 },
                { "마검사", 0 },
                { "궁수", 0 },
                { "음유시인", 0 }
            };

            AskQuestion
            (
                "첫번 째.. 전투가 시작됐습니다. 당신의 첫 행동은 무엇인가요?\n",
                new (string answer, string[] jobs)[]
                {
                    ("적에게 돌진한다", new[] { "전사" }),
                    ("뒤로 물러나 상황을 살핀다", new[] { "창술사", "궁수" }),
                    ("은신하여 적의 뒤를 노린다", new[] { "도적" }),
                    ("정신을 집중하고 마음을 가다듬는다", new[] { "마검사" }),
                    ("노래로 전우들을 격려한다", new[] { "음유시인" })
                },
                jobScores
            );

            AskQuestion
            (

                "두번 째.. 동료가 위험에 처했습니다. 당신은 어떻게 행동할건가요?\n",
                new (string answer, string[] jobs)[]
                {
                    ("즉시 몸을 던져 구한다", new[] { "전사", "마검사" }),
                    ("차분히 상황을 분석하고 작전을 구상한다", new[] { "창술사" }),
                    ("뒤에서 적을 기습해 시선을 돌린다", new[] { "도적" }),
                    ("멀리서 엄호한다", new[] { "궁수" }),
                    ("격려의 노래로 용기를 북돋는다", new[] { "음유시인" })
                },
                jobScores
            );

            AskQuestion
            (
                "마지막으로.. 혼자 여정을 떠날 때 당신이 가장 중요하게 여기는 것은 무엇인가요?\n",
                new (string answer, string[] jobs)[]
                {
                    ("위험에 맞설 수 있는 힘", new[] { "전사" }),
                    ("치밀한 계획과 판단", new[] { "창술사" }),
                    ("빠른 판단력과 발놀림", new[] { "도적" }),
                    ("자신과의 대화, 명상", new[] { "마검사" }),
                    ("눈에 띄지 않는 은신과 관찰", new[] { "궁수" }),
                    ("풍경과 이야기, 감성", new[] { "음유시인" })
                },
                jobScores
            );

            string selectedJob = jobScores.OrderByDescending(j => j.Value).First().Key;
            Job = selectedJob;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n당신은 '{Job}'의 성향을 지닌 자군요!\n");
            Console.ResetColor();

            // 해설 출력
            switch (Job)
            {
                
                case "전사":
                    Console.WriteLine(" [ 전사 ]\n ");
                    Console.WriteLine("용감하고 불굴의 정신을 지닌 당신은, 언제나 선두에서 전투를 이끌 준비가 되어 있습니다.");
                    Console.WriteLine("육탄전과 강한 체력을 바탕으로 아군의 방패가 되어 주세요.");
                    break;

                case "창술사":
                    Console.WriteLine(" [ 창술사 ]\n ");
                    Console.WriteLine("신중하고 냉철한 판단력을 가진 당신은, 거리와 타이밍을 재는 데 능숙합니다.");
                    Console.WriteLine("긴 리치를 활용한 전략적인 전투가 어울립니다.");
                    break;

                case "도적":
                    Console.WriteLine(" [ 도적 ]\n ");
                    Console.WriteLine("민첩하고 기민한 당신은, 어둠 속에서도 자유롭게 움직이며 적의 허를 찌릅니다.");
                    Console.WriteLine("빠르고 은밀한 전투를 즐기세요.");
                    break;

                case "마검사":
                    Console.WriteLine(" [ 마검사 ]\n ");
                    Console.WriteLine("마법과 검술을 함께 다루는 당신은, 균형과 집중의 달인입니다.");
                    Console.WriteLine("혼자서도 전장을 휘어잡는 다재다능한 존재입니다.");
                    break;

                case "궁수":
                    Console.WriteLine(" [ 궁수 ]\n ");
                    Console.WriteLine("차분하고 침착한 당신은, 멀리서 전황을 관찰하며 정밀한 한 발로 전세를 뒤집습니다.");
                    Console.WriteLine("정확한 판단력과 거리감각이 강점입니다.");
                    break;

                case "음유시인":
                    Console.WriteLine(" [ 음유시인 ]\n ");
                    Console.WriteLine("당신은 이야기를 전하고, 전장의 흐름을 노래하는 감성의 전사입니다.");
                    Console.WriteLine("전투의 긴장 속에서도 웃음과 희망을 잃지 않도록 돕는 존재입니다.");
                    break;
            }

            // 직업 별 스탯
            switch (Job)
            {
                case "전사":
                    baseAtk = 12; baseDef = 7; maxHp = 120; currentHp = 120;
                    break;
                case "창술사":
                    baseAtk = 11; baseDef = 6; maxHp = 110; currentHp = 110;
                    break;
                case "도적":
                    baseAtk = 13; baseDef = 5; maxHp = 100; currentHp = 100;
                    break;
                case "마검사":
                    baseAtk = 11; baseDef = 7; maxHp = 90; currentHp = 100;
                    break;
                case "궁수":
                    baseAtk = 10; baseDef = 5; maxHp = 100; currentHp = 100;
                    break;
                case "음유시인":
                    baseAtk = 8; baseDef = 4; maxHp = 120; currentHp = 120;
                    break;
            }

            WaitInput();
        }

        void AskQuestion(string question, (string answer, string[] jobs)[] options, Dictionary<string, int> jobScores)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(question);
            Console.ResetColor();
            
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i].answer}");
            }

            int choice = 0;
            while (choice < 1 || choice > options.Length)
            {
                Console.Write("\n번호를 입력해주세요\n>> ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            foreach (string job in options[choice - 1].jobs)
            {
                jobScores[job]++;
            }

            Console.WriteLine();
        }

        private int GetBonusAtk(List<ItemController> inventory)
        {
            return inventory.Where(i => i.isUse && i.ItemType=="무기").Sum(i => i.Effect);
        }

        private int GetBonusDef(List<ItemController> inventory)
        {
            return inventory.Where(i => i.isUse && i.ItemType=="방어구").Sum(i => i.Effect);
        }

        public void Status()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [ 상태 보기 ]\n");
                Console.ResetColor();
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

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
            int bonusAtk = GetBonusAtk(GameManager.InventoryController.GetPlayerItems());
            int bonusDef = GetBonusDef(GameManager.InventoryController.GetPlayerItems());

            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ( {Job} )");
            Console.WriteLine($"공격력 : {baseAtk + bonusAtk} (+{bonusAtk})");
            Console.WriteLine($"방어력 : {baseDef + bonusDef} (+{bonusDef})");
            Console.WriteLine($"체력 : {currentHp}/{maxHp}");
            Console.WriteLine($"Gold : {Gold} G\n");
        }

        public void GainExp(int expGained)
        {
            Exp += expGained;
            LevelUp();
        }
        public void LevelUp()
        {

            while (Level - 1 < levelUpExp.Count && Exp >= levelUpExp[Level - 1])
            {
                Exp -= levelUpExp[Level - 1];
                Level++;
                Console.WriteLine($"레벨업! {Name}는 Lv{Level}로 상승했습니다!");
                baseAtk += 0.5f;
                baseDef += 1;
            }
        }

        static void WrongInput()
        {
            Console.WriteLine("\n\a잘못된 입력입니다.");
            WaitInput();
        }

        static void WaitInput() // 아직 구현 안 했습니다에 쓰려고 WrongInput이랑 분리
        {
            Console.WriteLine("\n진행하려면 아무 키나 누르세요.");
            Console.ReadKey(true);
        }
    }
}

