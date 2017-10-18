using System.Collections.Generic;
using Entitas;

public class MovementSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public MovementSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var newPos = entity.position.Value.ToVector2() + entity.move.Direction.ToVector2() * entity.speed.Value * _contexts.game.deltaTime.Value;
            entity.ReplacePosition(new Position(newPos));
            entity.RemoveMove();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMove && entity.isMovable && entity.hasSpeed && entity.hasPosition;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Move, GameMatcher.Movable, GameMatcher.Position, GameMatcher.Speed));
    }
}
