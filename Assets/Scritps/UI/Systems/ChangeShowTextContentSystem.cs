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
        string showInfo = "";

        if (_contexts.game.hasLastTick)
        {
            var lastTick = _contexts.game.lastTick.Value;
            showInfo = string.Format("{0}/{1}", currentTick, lastTick);
        }
        else
        {
            showInfo = currentTick.ToString();
        }

        foreach (var entity in entities)
        {
            var showTextEntities = _showTextGroup.GetEntities();
            foreach (var showTextEntity in showTextEntities)
            {
                showTextEntity.showText.Value.Show(showInfo);
            }
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
