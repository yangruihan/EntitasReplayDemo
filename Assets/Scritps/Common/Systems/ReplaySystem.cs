using System.Collections.Generic;
using Entitas;

public class ReplaySystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ReplaySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!_contexts.game.hasLogicSystem || !_contexts.game.hasRecords)
            return;

        var logicSys = _contexts.game.logicSystem.Value;
        var inputRecords = _contexts.game.records.InputRecords;

        foreach (var entity in entities)
        {
            int count = entity.replay.ToTick;
            logicSys.Initialize();

            int inputActionIndex = 0;
            for (int i = 0; i <= count; i++)
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
