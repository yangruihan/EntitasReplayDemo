using System.Collections.Generic;
using Entitas;

public class PositionRecordSystem : ReactiveSystem<GameEntity>
{
    private static readonly float RECORD_INTERVAL_TIME = 2f;

    private Contexts _contexts;
    private IGroup<GameEntity> _playerGroup;
    private float _timer = RECORD_INTERVAL_TIME;

    public PositionRecordSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _playerGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (_contexts.game.hasGameStatus && _contexts.game.gameStatus.Value != EnmGameStatus.Running && _contexts.game.hasTick)
            return;

        foreach (var entity in entities)
        {
            _timer += entity.deltaTime.Value;

            if (_timer >= RECORD_INTERVAL_TIME)
            {
                _timer = 0f;
                var records = _contexts.game.hasPositionRecords ? _contexts.game.positionRecords.Value : new List<PositionRecordData>();
                var player = _playerGroup.GetSingleEntity();

                records.Add(new PositionRecordData(_contexts.game.tick.Value, player.position.Value));
                _contexts.game.ReplacePositionRecords(records);
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
