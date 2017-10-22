using System.Collections.Generic;
using Entitas;

public class InputRecordSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _recordGroup;

    public InputRecordSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _recordGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Recordable, GameMatcher.InputRecords, GameMatcher.ID));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var recordEntities = _recordGroup.GetEntities();

        foreach (var inputEntity in entities)
        {
            foreach (var recordEntity in recordEntities)
            {
                if (recordEntity.iD.Value != inputEntity.input.ID
                || recordEntity.inputRecords.CurrentTick > inputEntity.input.Tick)
                    continue;

                var records = recordEntity.inputRecords.Value;
                records.Add(new InputRecordData(inputEntity.input.Tick, inputEntity.input.KeyCode));
                recordEntity.ReplaceInputRecords(inputEntity.input.Tick, records);
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
