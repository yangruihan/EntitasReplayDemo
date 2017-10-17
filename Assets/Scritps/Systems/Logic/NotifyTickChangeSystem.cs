
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class NotifyTickChangeSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public NotifyTickChangeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Tick);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasTick;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //Debug.Log(_contexts.game.tick.Value);
    }
}
