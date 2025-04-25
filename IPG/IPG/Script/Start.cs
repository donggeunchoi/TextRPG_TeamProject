using IPG;
using System.Numerics;

namespace IPG
{
    internal class Start
    {
        static void Main(string[] args)
        {
            // 기본 객체 생성
            PlayerController _player = new PlayerController();
            StoreController _store = new StoreController(_player);
            InventoryController _inventory = new InventoryController(_store, _player);
            MonsterController _monsters = new MonsterController();

            // null 주입
            BattleController _battleController = new BattleController(_player, null, null);  // 일단 null
            VillageController _village = new VillageController(_store, _inventory, _player, _battleController, null, null); // 일단 null
            DungeonLobbyController _dungeonLobby = new DungeonLobbyController(_player, _battleController, _village);

            // 순환 연결
            _battleController.SetVillage(_village);
            _battleController.SetDungeonLobby(_dungeonLobby);
            _village.SetDungeonLobby(_dungeonLobby);

            BattleManager _battleManager = new BattleManager(_player, _battleController, null);

            _monsters.SaveMonster();
            _store.SaveItem();
            _player.SetVillage(_village);
            _player.SetInventory(_inventory);
            _player.StartStory();
            _village.Enter();
        
        }
    }
}