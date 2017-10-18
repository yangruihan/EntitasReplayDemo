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
        _contexts.game.ReplaceDeltaTime(Time.deltaTime);
    }
}
