using System.Collections.Generic;
using Entitas;

public class ReplaySystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _recordGroup;

    public ReplaySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _recordGroup = _contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.ID, GameMatcher.Recordable, GameMatcher.Position,
            GameMatcher.InputRecords, GameMatcher.PositionRecords));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!_contexts.game.hasLogicSystem)
        {
            DebugUtil.LogErrorFormat("Replay System error: there is no logic system");
            return;
        }

        var logicSys = _contexts.game.logicSystem.Value;
        var recordEntities = _recordGroup.GetEntities();

        foreach (var entity in entities)
        {
            ReplayUtil.Replay(_contexts, logicSys, entity.replay.ToTick, recordEntities);

            entity.isDestroyed = true;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasReplay;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Replay);
    }


}
