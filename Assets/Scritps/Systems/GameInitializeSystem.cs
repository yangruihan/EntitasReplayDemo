using Entitas;

public class GameInitializeSystem : IInitializeSystem
{
    private Contexts _contexts;

    public GameInitializeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _contexts.game.SetTime(0);
        _contexts.game.SetGameStatus(EnmGameStatus.Running);
    }
}
