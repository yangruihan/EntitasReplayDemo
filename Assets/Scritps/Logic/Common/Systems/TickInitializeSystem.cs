using Entitas;

public class TickInitializeSystem : IInitializeSystem
{
    private Contexts _contexts;

    public TickInitializeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _contexts.game.ReplaceTick(0);
    }
}
