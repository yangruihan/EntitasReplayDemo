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
        if (_contexts.game.gameStatus.Value != EnmGameStatus.Running)
            return;

        var recordEntities = _recordGroup.GetEntities();
        var inputComp = _contexts.game.input;

        foreach (var recordEntity in recordEntities)
        {
            if (recordEntity.iD.Value != inputComp.ID
                || recordEntity.inputRecords.CurrentTick > inputComp.Tick)
                continue;

            var records = recordEntity.inputRecords.Value;
            records.Add(new InputRecordData(inputComp.Tick, inputComp.KeyCode));
            recordEntity.ReplaceInputRecords(inputComp.Tick, records);
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
