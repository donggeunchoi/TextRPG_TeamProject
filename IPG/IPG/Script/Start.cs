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
            DungeonLobbyController _dungeonLobby = new DungeonLobbyController(_player,_battleController);
            

            VillageController _village = new VillageController(_store, _inventory, _player, _battleController, _battleManager,_dungeonLobby);
            

            store.SaveItem();

            player.SetVillage(village);
            player.SetInventory(inventory);
            player.StartStory();
            

            village.Enter();
        }
    }
}