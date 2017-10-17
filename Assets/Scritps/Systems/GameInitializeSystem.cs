using System.Collections.Generic;
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
        _contexts.game.ReplaceGameStatus(EnmGameStatus.Running);
    }
}
