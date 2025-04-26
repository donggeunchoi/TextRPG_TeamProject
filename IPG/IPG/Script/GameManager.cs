using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG
{
    internal static class GameManager
    {
        public static PlayerController PlayerController;
        public static MonsterController MonsterController;
        public static StoreController StoreController;
        public static InventoryController InventoryController;
        public static ItemController ItemController;
        public static BattleController BattleController;
        public static VillageController VillageController;
        public static DungeonLobbyController DungeonLobbyController;
        public static BattleManager BattleManager;
        public static BossController BossController;
        public static QuestController QuestController;

        public static List<ItemController> ListStoreItems = new List<ItemController>();
        public static List<int> ListPlayerOwningNumber = new List<int>();
        public static List<MonsterController> ListMonsters = new List<MonsterController>();

        public static void Init()
        {
            PlayerController = new PlayerController();
            MonsterController = new MonsterController();
            StoreController = new StoreController();
            InventoryController = new InventoryController();
            ItemController = new ItemController();
            BattleController = new BattleController();
            VillageController = new VillageController();
            DungeonLobbyController = new DungeonLobbyController();
            BattleManager = new BattleManager();
            BossController = new BossController();
            QuestController = new QuestController();
        }
    }
}
