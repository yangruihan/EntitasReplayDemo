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
        throw new System.NotImplementedException();
    }

    protected override bool Filter(GameEntity entity)
    {
        throw new System.NotImplementedException();
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        throw new System.NotImplementedException();
    }
}
