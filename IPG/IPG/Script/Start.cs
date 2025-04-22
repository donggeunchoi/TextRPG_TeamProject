using IPG;
using System.Numerics;

namespace IPG
{
    internal class Start
    {
        static void Main(string[] args)
        {
            PlayerController player = new PlayerController();
            StoreController store = new StoreController(player);
            InventoryController inventory = new InventoryController(store, player);
            Battlecontroller battleController = new Battlecontroller();
            BattleManager battleManager = new BattleManager();

            VillageController village = new VillageController(store, inventory, player, battleController, battleManager);

            store.SaveItem();
            village.Enter();
        }
    }
}