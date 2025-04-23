using IPG;
using System.Numerics;

namespace IPG
{
    internal class Start
    {
        static void Main(string[] args)
        {
            //PlayerController _player = new PlayerController();
            //StoreController _store = new StoreController(_player);
            //InventoryController _inventory = new InventoryController(_store, _player);
            //Battlecontroller _battleController = new Battlecontroller(_player, _inventory);
            //BattleManager _battleManager = new BattleManager(_player);

            PlayerController _player = new PlayerController();
            StoreController _store = new StoreController(_player);
            InventoryController _inventory = new InventoryController(_store, _player);
            BattleController _battleController = new BattleController(_player);
            BattleManager _battleManager = new BattleManager();
            DungeonLobbyController _dungeonLobby = new DungeonLobbyController(_player,_battleController);
            

            VillageController _village = new VillageController(_store, _inventory, _player, _battleController, _battleManager,_dungeonLobby);
            

            _store.SaveItem();

            _player.SetVillage(_village);
            _player.SetInventory(_inventory);
            _player.StartStory();
            

            _village.Enter();
        }
    }
}