using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPG
{
    public enum QuestState // 퀘스트 상태, 수락 전, 진행 중, 보상 수령 가능, 완료
    {
        NotAccepted,
        InProgress,
        RewardAvailable,
        Completed
    }

    public enum QuestType // 퀘스트 종류
    {
        KillCount,      // 몬스터 처치
        ShopPurchase,   // 상점 구매
        Equipment,      // 장비 착용
        LevelUp,        // 레벨업
        StageClear,     // 스테이지 클리어
        BossKill        // 보스 처치
    }

    public class Quest // 퀘스트 객체
    {
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public QuestType Type { get; }
        public int Target { get; }
        public int Progress { get; set; }
        public QuestState State { get; set; }
        public string[] Rewards { get; }

        public Quest(int id, string title, string desc, QuestType type, int target, string[] rewards)
        {
            Id = id;
            Title = title;
            Description = desc;
            Type = type;
            Target = target;
            Rewards = rewards;
            Progress = 0;
            State = QuestState.NotAccepted;
        }

        public bool IsHidden => State == QuestState.Completed; // 완료한 퀘스트는 숨기기
    }

    internal class QuestController // 퀘스트 관리 컨트롤러
    {
        private readonly Dictionary<int, Quest> quests = new Dictionary<int, Quest>();
        private readonly Dictionary<int, int[]> prerequire = new Dictionary<int, int[]> // 선행 퀘스트
        {
            { 3, new[] { 2 } }, // 아이템 착용퀘는 아이템 구입퀘 완료 후 공개
            { 5, new[] { 4 } }, // 2층 클리어 퀘는 레벨업 퀘 완료 후 공개
            { 6, new[] { 5 } }  // 보스 처치 퀘는 2층 클리어 퀘 완료 후 공개
        };

        public bool HasPendingRewards() // 보상 수령 대기 중인 퀘스트 확인용
        {
            return quests.Values.Any(q => q.State == QuestState.RewardAvailable);
        }

        public QuestController()
        {
            quests[1] = new Quest(1, "마을을 위협하는 몬스터 처치",
                "이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나?\n\n마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n\n모험가인 자네가 좀 처치해 주게!", 
                QuestType.KillCount, 5, new[] { "스파르타의 검 x1", "100G" });

            quests[2] = new Quest(2, "아이템을 구입해보자",
                "여기에 처음 와본 거면 한 번 상점도 이용해 보라구.\n\n자네 같은 모험가들한테 필요한 물건들을 잔뜩 구비해 놓았으니 말이지.", 
                QuestType.ShopPurchase, 1, new[] { "뿌듯함 x1" });

            quests[3] = new Quest(3, "아이템을 착용해보자", 
                "인벤토리에서 장비를 하나 착용해 보세요.", 
                QuestType.Equipment, 1, new[] { "ATK +1" });

            quests[4] = new Quest(4, "더욱 더 강해지기!", 
                "일정 경험치가 쌓이면 레벨업을 합니다.", 
                QuestType.LevelUp, 1, new[] { "HP +10" });

            quests[5] = new Quest(5, "더 높은 곳으로",
                "무사히 돌아왔구먼.\n\n스파르타 던전은 3층까지 있는데, 높은 곳으로 올라갈수록 더 강한 몬스터들이 나오지.\n\n자네라면 믿고 맡길 수 있겠어. 한 번 2층까지 몬스터 놈들을 소탕하고 오게나.", 
                QuestType.StageClear, 1, new[] { "1000G" });

            quests[6] = new Quest(6, "스파르타 던전을 정복하라", 
                "보스 몬스터를 처치하여 IPG 세계를 구해주세요.", 
                QuestType.BossKill, 1, new[] { "???" }); // 신선하게 이 퀘스트 보상으로 크레딧이 나오게 하면 어떨까?
        }

        private bool IsPrerequire(int questId) // 선행 퀘스트 완료했는지 확인
        {
            if (prerequire.TryGetValue(questId, out int[] reqs))
            {
                foreach (int r in reqs)
                {
                    if (!quests.TryGetValue(r, out Quest prereq) || prereq.State != QuestState.Completed)
                        return false;
                }
            }
            return true;
        }

        public void EnterQuestBoard() // 모험가 조합 진입
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [ 모험가 조합 ]\n");
                Console.ResetColor();
                Console.WriteLine("퀘스트를 수행하여 보상을 받을 수 있습니다.\n");
                Console.WriteLine("[ 퀘스트 목록 ]");

                var available = new List<Quest>();
                foreach (var quest in quests.Values) // 퀘스트 목록 (완료한 퀘는 안 나타남)
                {
                    if (quest.IsHidden) continue;
                    if (!IsPrerequire(quest.Id)) continue;
                    available.Add(quest);
                }

                for (int i = 0; i < available.Count; i++)
                {
                    var q = available[i];
                    Console.ResetColor();
                    Console.Write($"{i + 1}. {q.Title}");

                    if (q.State == QuestState.NotAccepted) // 신규 퀘스트는 이름 뒤에 (신규!) 가 붙음
                    {
                        Console.Write(" ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("(신규!)");
                        Console.ResetColor();
                    }
                    else if (q.State == QuestState.InProgress)
                    {
                        Console.Write(" (진행중)");
                    }
                    else if (q.State == QuestState.RewardAvailable) // 완료한 퀘스트는 이름 뒤에 (완료) 가 붙음
                    {
                        Console.Write(" ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("(완료)");
                        Console.ResetColor();
                    }

                    Console.WriteLine();
                }

                Console.Write("\n0. 뒤로가기\n>> ");

                string input = Console.ReadLine();

                if (input == "0") return;

                if (int.TryParse(input, out int sel) && sel >= 1 && sel <= available.Count)
                {
                    ShowQuestDetail(available[sel - 1]);
                }
                else
                {
                    WrongInput();
                }
            }
        }

        private void ShowQuestDetail(Quest quest) // 퀘스트 상세창 분기
        {
            switch (quest.State)
            {
                case QuestState.NotAccepted: ShowNotAccepted(quest); break;
                case QuestState.InProgress: ShowInProgress(quest); break;
                case QuestState.RewardAvailable: ShowRewardAvailable(quest); break;
                case QuestState.Completed: return; // 완료한 퀘는 목록에서 숨겨지니 호출될 일 없지만 방어적 코딩용
            }
        }

        private void ShowNotAccepted(Quest q) // 수락 전 퀘스트 상세창
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [ 모험가 조합 ]\n");
                Console.ResetColor();
                Console.WriteLine($"[ {q.Title} ]\n");
                Console.WriteLine(q.Description + "\n\n");
                Console.WriteLine("[퀘스트 목표]");
                Console.WriteLine(GetConditionText(q) + "\n");
                Console.WriteLine("[퀘스트 보상]");
                foreach (var r in q.Rewards) Console.WriteLine($"- {r}");
                Console.Write("\n1. 수락    2. 거절\n>> ");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    q.State = QuestState.InProgress;
                    Console.WriteLine("\n퀘스트 수락!");
                    WaitInput();
                    break;
                }
                else if (input == "2")
                {
                    break;
                }
                else
                {
                    WrongInput();
                }
            }
        }

        private void ShowInProgress(Quest q) // 진행 중 퀘스트 상세창
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 모험가 조합 ]\n");
            Console.ResetColor();
            Console.WriteLine($"[ {q.Title} ]\n");
            Console.WriteLine(q.Description + "\n\n");
            Console.WriteLine("[퀘스트 목표]");
            Console.WriteLine(GetConditionText(q) + "\n");
            Console.WriteLine("[퀘스트 보상]");
            foreach (var r in q.Rewards) Console.WriteLine($"- {r}");
            Console.Write("\n뒤로 가시려면 아무 키나 누르세요.\n");
            Console.ReadLine();
        }
        
        private void ShowRewardAvailable(Quest q) // 보상 수령 가능 퀘스트 상세창
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [ 모험가 조합 ]\n");
            Console.ResetColor();
            Console.WriteLine($"[ {q.Title} ]\n");
            Console.WriteLine(q.Description + "\n\n");
            Console.WriteLine("[퀘스트 진행 상황]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GetConditionText(q) + "\n");
            Console.ResetColor();
            Console.WriteLine("[퀘스트 보상]");
            foreach (var r in q.Rewards) Console.WriteLine($"- {r}");
            Console.WriteLine("\n1. 보상 수령\n>> ");

            if (Console.ReadLine() == "1")
            {
                GiveRewards(q);
                q.State = QuestState.Completed;
                WaitInput();
            }
        }
        
        private string GetConditionText(Quest q) => q.Type switch // 퀘스트 목표/진행 상황 텍스트
        {
            QuestType.KillCount => $"- 몬스터 처치 ( {q.Progress} / {q.Target} )",
            QuestType.ShopPurchase => $"- 상점에서 아이템 구매 ( {q.Progress} / {q.Target} )",
            QuestType.Equipment => $"- 인벤토리에서 아이템 착용 ( {q.Progress} / {q.Target} )",
            QuestType.LevelUp => $"- 레벨업 ( {q.Progress} / {q.Target} )",
            QuestType.StageClear => $"- 던전 2층 클리어 ( {q.Progress} / {q.Target} )",
            QuestType.BossKill => $"- 보스 몬스터 처치 ( {q.Progress} / {q.Target} )",
            _ => string.Empty // 방어적 코딩
        };

        private void GiveRewards(Quest q) // 퀘스트 별 보상
        {
            switch (q.Id)
            {
                case 1: // 몬스터 5마리 처치, 스파르타의 검 1개, 골드 +100
                    GameManager.InventoryController.AddQuestRewardSpartaSword();
                    GameManager.PlayerController.Gold += 100;
                    Console.WriteLine("\n스파르타의 검을 획득했습니다!");
                    Console.WriteLine("100G를 획득했습니다!");
                    break;

                case 2: // 상점에서 아이템 구입, 뿌듯함 +1 (사실 아무 것도 없음)
                    Console.WriteLine("\n굉장히 뿌듯해졌습니다!");
                    break;

                case 3: // 인벤토리에서 아이템 착용, 영구 공격력 +1 증가
                    GameManager.PlayerController.baseAtk += 1;
                    Console.WriteLine("\n영구적으로 공격력이 1 증가했습니다!");
                    break;

                case 4: // 레벨업, 영구 체력 +20 증가
                    GameManager.PlayerController.maxHp += 20;
                    Console.WriteLine("\n영구적으로 최대 체력이 20 증가했습니다!");
                    break;

                case 5: // 2층 클리어, 골드 +1000
                    GameManager.PlayerController.Gold += 1000;
                    Console.WriteLine("\n1000G를 획득했습니다!");
                    break;

                case 6: // 보스 몬스터 처치, 무슨 보상할지 아직 못 정함 (크레딧?)
                    Ending.StartEnding();
                    break;

                default:
                    break;
            }
        }

        public void OnMonsterKilled() // 퀘 조건 - 몬스터 처치
        {
            foreach (var q in quests.Values)
            {
                if (q.State == QuestState.InProgress && q.Type == QuestType.KillCount && q.Progress < q.Target) // 목표 횟수 초과해서 카운트하지 않도록 제한, 이하 동일
                {
                    q.Progress++;
                    if (q.Progress >= q.Target) q.State = QuestState.RewardAvailable;
                }
            }
        }

        public void OnItemPurchased() // 퀘 조건 - 아이템 구입
        {
            foreach (var q in quests.Values)
            {
                if (q.State == QuestState.InProgress && q.Type == QuestType.ShopPurchase && q.Progress < q.Target)
                {
                    q.Progress++;
                    if (q.Progress >= q.Target) q.State = QuestState.RewardAvailable;
                }
            }
        }
        public void OnEquipmentEquipped() // 퀘 조건 - 아이템 장착
        {
            foreach (var q in quests.Values)
            {
                if (q.State == QuestState.InProgress && q.Type == QuestType.Equipment && q.Progress < q.Target)
                {
                    q.Progress++;
                    if (q.Progress >= q.Target) q.State = QuestState.RewardAvailable;
                }
            }
        }

        public void OnLevelUp(int level) // 퀘 조건 - 레벨업
        {
            foreach (var q in quests.Values)
            {
                if (q.State == QuestState.InProgress && q.Type == QuestType.LevelUp && q.Progress < q.Target)
                {
                    q.Progress++;
                    if (q.Progress >= q.Target) q.State = QuestState.RewardAvailable;
                }
            }
        }

        public void OnStageCleared(int stage) // 퀘 조건 - 스테이지 클리어
        {
            foreach (var q in quests.Values)
            {
                if (q.State == QuestState.InProgress && q.Type == QuestType.StageClear && stage == 2 && q.Progress < q.Target)
                {
                    q.Progress++;
                    if (q.Progress >= q.Target) q.State = QuestState.RewardAvailable;
                }
            }
        }

        public void OnBossKilled(string bossName) // 퀘 조건 - 보스 처치
        {
            foreach (var q in quests.Values)
            {
                if (q.State == QuestState.InProgress && q.Type == QuestType.BossKill && q.Progress < q.Target)
                {
                    q.Progress++;
                    if (q.Progress >= q.Target) q.State = QuestState.RewardAvailable;
                }
            }
        }

        private void WrongInput()
        {
            Console.WriteLine("\n잘못된 입력입니다.");
            WaitInput();
        }

        private void WaitInput()
        {
            Console.WriteLine("\n계속하려면 아무 키나 누르세요.");
            Console.ReadKey(true);
        }
    }
}