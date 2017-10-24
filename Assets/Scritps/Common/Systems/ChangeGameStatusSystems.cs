using System.Collections.Generic;
using Entitas;

public class ChangeGameStatusSystems : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _recordGroup;

    public ChangeGameStatusSystems(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _recordGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Recordable, GameMatcher.Position, GameMatcher.InputRecords, GameMatcher.PositionRecords));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            switch (entity.newGameStatus.Value)
            {
                case EnmGameStatus.Running:
                    if (_contexts.game.hasLogicSystem && _contexts.game.hasLastTick)
                    {
                        var logicSys = _contexts.game.logicSystem.Value;
                        var recordEntities = _recordGroup.GetEntities();

                        ReplayUtil.Replay(_contexts, logicSys, _contexts.game.lastTick.Value, recordEntities);
                    }

                    break;

                case EnmGameStatus.Pause:
                    _contexts.game.ReplaceLastTick(_contexts.game.tick.Value);
                    break;
            }

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
