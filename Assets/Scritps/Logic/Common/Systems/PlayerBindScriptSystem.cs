using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerBindScriptSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public PlayerBindScriptSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var obj = GameObject.Instantiate(_contexts.game.playerInitData.value.Prefab, entity.position.Value.ToVector2(), Quaternion.identity);
            entity.AddPlayerView(obj.GetComponent<PlayerViewBehaviour>());
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.player.Value;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Player);
    }
}
