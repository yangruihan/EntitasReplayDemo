using System.Collections.Generic;
using Entitas;

public class ChangePlayerPositionSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ChangePlayerPositionSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.playerView.Value.Move(entity.position.Value.ToVector2());
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition && entity.hasPlayer && entity.hasPlayerView;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Player, GameMatcher.PlayerView));
    }
}
