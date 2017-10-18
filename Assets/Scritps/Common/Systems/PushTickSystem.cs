using Entitas;

public class PushTickSystem : IExecuteSystem
{
    private Contexts _contexts;

    public PushTickSystem(Contexts _contexts)
    {
        this._contexts = _contexts;
    }

    public void Execute()
    {
        if (_contexts.game.gameStatus.Value != EnmGameStatus.Running)
            return;

        _contexts.game.ReplacePushTick(true);
        //_contexts.game.isPushTick = true;
        //var entity = _contexts.game.CreateEntity();
        //entity.isPushTick = true;
    }
}
