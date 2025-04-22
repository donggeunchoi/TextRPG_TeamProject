using IPG;
using System.Numerics;

namespace IPG
{
    internal class Start
    {
        static void Main(string[] args)
        {
            PlayerController _player = new PlayerController();
            StoreController _store = new StoreController(_player);
            InventoryController _inventory = new InventoryController(_store, _player);
            Battlecontroller _battleController = new Battlecontroller();
            BattleManager _battleManager = new BattleManager();

            VillageController village = new VillageController(_store, _inventory, _player, _battleController, _battleManager);

            _store.SaveItem();
            village.Enter();
        }
    }
}