using System.Collections.Generic;
using Entitas;

public class ChangeGameStatusSystems : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _playerGroup;

    public ChangeGameStatusSystems(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _playerGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            switch (entity.newGameStatus.Value)
            {
                case EnmGameStatus.Running:

                    if (_contexts.game.hasLogicSystem && _contexts.game.hasInputRecords && _contexts.game.hasPositionRecords)
                    {
                        var logicSys = _contexts.game.logicSystem.Value;
                        var inputRecords = _contexts.game.inputRecords.Value;
                        var positionRecords = _contexts.game.positionRecords.Value;
                        var player = _playerGroup.GetSingleEntity();

                        var replaySys = _contexts.game.replaySystem.Value;

                        replaySys.Replay(logicSys, _contexts.game.lastTick.Value, player, inputRecords, positionRecords);
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
