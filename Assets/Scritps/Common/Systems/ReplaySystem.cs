using System.Collections.Generic;
using Entitas;

public class ReplaySystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _playerGroup;

    public ReplaySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _playerGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!_contexts.game.hasLogicSystem || !_contexts.game.hasInputRecords || !_contexts.game.hasPositionRecords)
        {
            DebugUtil.LogErrorFormat("Replay System error: record datas error");
            return;
        }

        var logicSys = _contexts.game.logicSystem.Value;
        var inputRecords = _contexts.game.inputRecords.Value;
        var positionRecords = _contexts.game.positionRecords.Value;

        var player = _playerGroup.GetSingleEntity();

        foreach (var entity in entities)
        {
            Replay(logicSys, entity.replay.ToTick, player, inputRecords, positionRecords);

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

    public void Replay(Systems logicSys, int toTick, GameEntity player, List<InputRecordData> inputRecords, List<PositionRecordData> positionRecords)
    {
        logicSys.Initialize();

        int inputActionIndex = 0;
        int startTick = 0;

        for (int i = positionRecords.Count - 1; i >= 0; i--)
        {
            var positionRecord = positionRecords[i];
            if (positionRecord.Tick <= toTick)
            {
                startTick = positionRecord.Tick;
                player.ReplacePosition(positionRecord.Position);

                _contexts.game.ReplaceTick(startTick);
                _contexts.game.ReplaceLogicTime(
                    startTick * _contexts.game.logicTime.DeltaTime,
                    _contexts.game.logicTime.DeltaTime,
                    _contexts.game.logicTime.TargetFrameRate);

                while (inputRecords.Count > inputActionIndex && inputRecords[inputActionIndex].Tick < _contexts.game.tick.Value)
                {
                    inputActionIndex++;
                }

                break;
            }
        }

        for (int i = startTick + 1; i <= toTick; i++)
        {
            while (inputRecords.Count > inputActionIndex && inputRecords[inputActionIndex].Tick == _contexts.game.tick.Value)
            {
                var inputAction = inputRecords[inputActionIndex];
                _contexts.game.CreateEntity().AddInput(inputAction.Tick, inputAction.KeyCode);
                logicSys.Execute();

                inputActionIndex++;
            }

            _contexts.game.ReplacePushTick(true);

            logicSys.Execute();
            logicSys.Cleanup();
        }
    }
}
