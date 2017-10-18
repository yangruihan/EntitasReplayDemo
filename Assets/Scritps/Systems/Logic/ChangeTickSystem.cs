using Entitas;

public class ChangeTickSystem : IExecuteSystem
{
    private Contexts _contexts;

    public ChangeTickSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        _contexts.game.ReplaceTick(_contexts.game.tick.Value + 1);
    }
}