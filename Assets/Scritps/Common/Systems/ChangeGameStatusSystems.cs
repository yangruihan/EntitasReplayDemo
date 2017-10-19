using System.Collections.Generic;
using Entitas;

public class ChangeGameStatusSystems : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ChangeGameStatusSystems(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            switch (entity.newGameStatus.Value)
            {
                case EnmGameStatus.Running:

                    if (_contexts.game.hasLogicSystem && _contexts.game.hasRecords)
                    {
                        var logicSys = _contexts.game.logicSystem.Value;
                        var inputRecords = _contexts.game.records.InputRecords;
                        int count = _contexts.game.lastTick.Value;
                        logicSys.Initialize();

                        int inputActionIndex = 0;
                        for (int i = 0; i < count; i++)
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
