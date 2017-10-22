using System.Collections.Generic;
using Entitas;

public class PositionRecordSystem : ReactiveSystem<GameEntity>
{
    private static readonly float RECORD_INTERVAL_TIME = 2f;

    private Contexts _contexts;
    private IGroup<GameEntity> _recordGroup;
    private float _timer = RECORD_INTERVAL_TIME;

    public PositionRecordSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _recordGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Recordable, GameMatcher.Position, GameMatcher.PositionRecords));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (_contexts.game.hasGameStatus && _contexts.game.gameStatus.Value != EnmGameStatus.Running && _contexts.game.hasTick)
            return;

        var recordEntities = _recordGroup.GetEntities();

        foreach (var entity in entities)
        {
            _timer += entity.deltaTime.Value;

            if (_timer >= RECORD_INTERVAL_TIME)
            {
                _timer = 0f;

                foreach (var recordEntity in recordEntities)
                {
                    var records = recordEntity.positionRecords.Value;
                    records.Add(new PositionRecordData(_contexts.game.tick.Value, recordEntity.position.Value));
                    recordEntity.ReplacePositionRecords(records);
                }
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDeltaTime;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DeltaTime);
    }
}
