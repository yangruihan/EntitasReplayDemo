using System.Collections.Generic;
using Entitas;

public class ChangeGameStatusSystems : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ChangeGameStatusSystems(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            _contexts.game.ReplaceGameStatus(entity.newGameStatus.Value);
            entity.isDestroyed = true;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasNewGameStatus;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NewGameStatus);
    }
}
