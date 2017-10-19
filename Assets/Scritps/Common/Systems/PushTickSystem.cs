using Entitas;
using UnityEngine;

public class PushTickSystem : IExecuteSystem
{
    private Contexts _contexts;

    private float _lastTime;
    private float _currentTime;
    private float _lag;

    public PushTickSystem(Contexts _contexts)
    {
        this._contexts = _contexts;
    }

    public void Execute()
    {
        if (_contexts.game.gameStatus.Value != EnmGameStatus.Running)
            return;

        _currentTime = Time.time;
        _lag += _currentTime - _lastTime;
        _lastTime = _currentTime;

        while (_lag > _contexts.game.logicTime.DeltaTime)
        {
            _contexts.game.ReplacePushTick(true);
            _lag -= _contexts.game.logicTime.DeltaTime;
        }

    }
}
