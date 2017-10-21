using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class InputHandleSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public InputHandleSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var moveEntity = _contexts.game.GetEntitiesWithID(entity.input.ID).First();

            if (moveEntity == null)
                continue;

            switch (entity.input.KeyCode)
            {
                case KeyCode.A:
                    moveEntity.ReplaceMove(new Direction(-1, 0));
                    break;
                case KeyCode.D:
                    moveEntity.ReplaceMove(new Direction(1, 0));
                    break;
                case KeyCode.W:
                    moveEntity.ReplaceMove(new Direction(0, 1));
                    break;
                case KeyCode.S:
                    moveEntity.ReplaceMove(new Direction(0, -1));
                    break;
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasInput;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }
}
