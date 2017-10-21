using System.Collections.Generic;
using Entitas;

public class ChangeShowTextContentSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _showTextGroup;

    public ChangeShowTextContentSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _showTextGroup = _contexts.game.GetGroup(GameMatcher.ShowText);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var currentTick = _contexts.game.tick.Value;
        var lastTick = _contexts.game.hasLastTick ? _contexts.game.lastTick.Value : 0;
        string showInfo = "";

        if (_contexts.game.gameStatus.Value == EnmGameStatus.Pause && _contexts.game.hasLastTick)
        {
            showInfo = string.Format("{0}/{1}", currentTick, lastTick);
        }
        else
        {
            showInfo = string.Format("{0}/{1}", lastTick, currentTick);
        }

        foreach (var entity in entities)
        {
            var showTextEntity = _showTextGroup.GetSingleEntity();
            showTextEntity.showText.Value.Show(showInfo);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasTick;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Tick);
    }
}
