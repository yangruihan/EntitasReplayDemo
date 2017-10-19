using System.Collections.Generic;
using Entitas;

public class ChangeRecordSliderValueSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _recordSliderGroup;

    public ChangeRecordSliderValueSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _recordSliderGroup = _contexts.game.GetGroup(GameMatcher.RecordSlider);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var sliderEntity = _recordSliderGroup.GetSingleEntity();
            sliderEntity.recordSlider.Value.SetSlider(entity.lastTick.Value);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasLastTick;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.LastTick);
    }
}
