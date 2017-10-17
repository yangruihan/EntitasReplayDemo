using System.Collections.Generic;
using Entitas;

public class ChangeTickSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ChangeTickSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            _contexts.game.ReplaceTick(_contexts.game.tick.Value + 1);
            entity.isDestroyed = true;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isChangeTickNotify;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChangeTickNotify);
    }
}
