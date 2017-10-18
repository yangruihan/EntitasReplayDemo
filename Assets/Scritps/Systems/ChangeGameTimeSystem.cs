using System.Collections.Generic;
using Entitas;

public class ChangeGameTimeSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ChangeGameTimeSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            _contexts.game.ReplaceTime(_contexts.game.time.Value + entity.deltaTime.Value);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDeltaTime;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DeltaTime);
    }
}
