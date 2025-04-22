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


            VillageController village = new VillageController(store, inventory, player);

            store.SaveItem();
            village.Enter();
        }
    }
}