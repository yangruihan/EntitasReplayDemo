using System.Collections.Generic;
using Entitas;

public class InputRecordSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public InputRecordSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var records = _contexts.game.hasRecords ? _contexts.game.records.InputRecords : new List<GameEntity>();

        foreach (var entity in entities)
        {
            records.Add(entity);
        }
        _contexts.game.ReplaceRecords(records);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isRecord && entity.hasInput;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Record);
    }
}
