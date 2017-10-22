using System.Collections.Generic;
using Entitas;

public class ReplaySystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _recordGroup;

    public ReplaySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _contexts.game.SetReplaySystem(this);
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
            Replay(logicSys, entity.replay.ToTick, recordEntities);

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

    public void Replay(Systems logicSys, int toTick, GameEntity[] recordEntities)
    {
        logicSys.Initialize();

        int[] inputActionIndexArr = new int[recordEntities.Length];
        int startTick = 0;

        if (recordEntities.Length > 0)
        {
            var positionRecords = recordEntities[0].positionRecords.Value;
            for (int i = positionRecords.Count - 1; i >= 0 ; i--)
            {
                var pos = positionRecords[i];
                if (pos.Tick <= toTick)
                {
                    startTick = pos.Tick;

                    _contexts.game.ReplaceTick(startTick);
                    _contexts.game.ReplaceLogicTime(
                        startTick * _contexts.game.logicTime.DeltaTime,
                        _contexts.game.logicTime.DeltaTime,
                        _contexts.game.logicTime.TargetFrameRate
                        );

                    // replace record entities pos
                    foreach (var recordEntity in recordEntities)
                    {
                        recordEntity.ReplacePosition(recordEntity.positionRecords.Value[i].Position);
                    }

                    break;
                }
            }
        }

        // ignore input actions before startTick 
        if (startTick != 0)
        {
            for (int i = 0; i < recordEntities.Length; i++)
            {
                var inputRecords = recordEntities[i].inputRecords.Value;
                while (inputRecords.Count > inputActionIndexArr[i] &&
                       inputRecords[inputActionIndexArr[i]].Tick <= startTick)
                {
                    inputActionIndexArr[i]++;
                }
            }

            startTick++;
            _contexts.game.ReplacePushTick(true);
            logicSys.Execute();
            logicSys.Cleanup();
        }

        for (int i = startTick; i < toTick; i++)
        {
            for (int j = 0; j < recordEntities.Length; j++)
            {
                var inputRecords = recordEntities[j].inputRecords.Value;
                while (inputRecords.Count > inputActionIndexArr[j] &&
                       inputRecords[inputActionIndexArr[j]].Tick == _contexts.game.tick.Value)
                {
                    var inputAction = inputRecords[inputActionIndexArr[j]];
                    _contexts.game.CreateEntity().AddInput(recordEntities[j].iD.Value, inputAction.Tick, inputAction.KeyCode);
                    inputActionIndexArr[j]++;

                    logicSys.Execute();
                    logicSys.Cleanup();
                }
            }

            _contexts.game.ReplacePushTick(true);
            logicSys.Execute();
            logicSys.Cleanup();
        }
    }
}
