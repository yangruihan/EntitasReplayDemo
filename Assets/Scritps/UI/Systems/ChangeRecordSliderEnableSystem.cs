using System.Collections.Generic;
using Entitas;

public class ChangeRecordSliderEnableSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private IGroup<GameEntity> _recordSliderGroup;

    public ChangeRecordSliderEnableSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _recordSliderGroup = _contexts.game.GetGroup(GameMatcher.RecordSlider);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var sliderEntity = _recordSliderGroup.GetSingleEntity();
            switch (entity.newGameStatus.Value)
            {
                case EnmGameStatus.Pause:
                    sliderEntity.recordSlider.Value.EnableSlider();
                    break;

                case EnmGameStatus.Running:
                    sliderEntity.recordSlider.Value.DisableSlider();
                    break;
            }
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