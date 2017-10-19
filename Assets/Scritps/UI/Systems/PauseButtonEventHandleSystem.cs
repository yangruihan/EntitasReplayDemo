using System.Collections.Generic;
using Entitas;

public class PauseButtonEventHandleSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public PauseButtonEventHandleSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var newGameStatusEntity = _contexts.game.CreateEntity();
            switch (entity.pauseBtnClick.Status)
            {
                case EnmPauseBtnStatus.Pause:
                    newGameStatusEntity.AddNewGameStatus(EnmGameStatus.Pause);
                    break;
                case EnmPauseBtnStatus.Running:
                    newGameStatusEntity.AddNewGameStatus(EnmGameStatus.Running);
                    break;
            }
            entity.isDestroyed = true;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPauseBtnClick;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PauseBtnClick);
    }
}
