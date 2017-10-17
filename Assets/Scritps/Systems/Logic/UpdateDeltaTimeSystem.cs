using Entitas;
using UnityEngine;

public class UpdateDeltaTimeSystem : IInitializeSystem, IExecuteSystem
{

    private Contexts _contexts;

    public UpdateDeltaTimeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _contexts.game.ReplaceDeltaTime(0);
    }

    public void Execute()
    {
        if (_contexts.game.gameStatus.Value != EnmGameStatus.Running)
            return;

        _contexts.game.deltaTime.Value += Time.deltaTime;
        if (_contexts.game.deltaTime.Value >= 1)
        {
            var entity = _contexts.game.CreateEntity();
            entity.isChangeTickNotify = true;
            _contexts.game.ReplaceDeltaTime(0);
        }
    }
}
