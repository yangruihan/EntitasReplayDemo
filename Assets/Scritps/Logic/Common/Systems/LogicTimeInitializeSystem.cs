using Entitas;

public class LogicTimeInitializeSystem : IInitializeSystem
{
    private Contexts _contexts;

    public LogicTimeInitializeSystem(Contexts _contexts)
    {
        this._contexts = _contexts;
    }

    public void Initialize()
    {
        _contexts.game.ReplaceLogicTime(
            0,
            _contexts.game.logicTime.DeltaTime,
            _contexts.game.logicTime.TargetFrameRate
        );
    }
}
