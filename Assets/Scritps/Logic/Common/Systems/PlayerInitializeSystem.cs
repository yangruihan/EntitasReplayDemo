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
        var player = _contexts.game.CreateEntity();
        player.AddPlayer(true);
        player.isMovable = true;
        player.AddPosition(new Position(0, 0));
        player.AddSpeed(_contexts.game.playerInitData.value.Speed);
    }
}
