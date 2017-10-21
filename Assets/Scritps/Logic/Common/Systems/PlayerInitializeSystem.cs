using System.Collections.Generic;
using System.Linq;
using Entitas;

public class PlayerInitializeSystem : IInitializeSystem
{
    private Contexts _contexts;

    public PlayerInitializeSystem(Contexts _contexts)
    {
        this._contexts = _contexts;
    }

    public void Initialize()
    {
        var entities = new List<GameEntity>(_contexts.game.GetEntitiesWithPlayer(true));

        List<GameEntity> players;
        if (entities.Count == 0)
        {
            players = new List<GameEntity>();

            var player = _contexts.game.CreateEntity();
            player.AddPlayer(true);
            player.AddID(0);
            player.isMovable = true;
            player.AddInputRecords(new List<InputRecordData>());
            player.AddPositionRecords(new List<PositionRecordData>());
            player.isRecordable = true;
            players.Add(player);

            var player1 = _contexts.game.CreateEntity();
            player1.AddPlayer(true);
            player1.AddID(1);
            player1.isMovable = true;
            player1.AddInputRecords(new List<InputRecordData>());
            player1.AddPositionRecords(new List<PositionRecordData>());
            player1.isRecordable = true;
            players.Add(player1);
        }
        else
        {
            players = entities;
        }

        foreach (var player in players)
        {
            player.ReplacePosition(new Position(0, 0));
            player.ReplaceSpeed(_contexts.game.playerInitData.value.Speed);
        }
    }
}
