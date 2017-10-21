using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class InputHandleSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private GameEntity _player;

    public InputHandleSystem(Contexts _contexts) : base(_contexts.game)
    {
        this._contexts = _contexts;
        _player = _contexts.game.GetEntitiesWithPlayer(true).FirstOrDefault();
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (_player == null)
        {
            _player = _contexts.game.GetEntitiesWithPlayer(true).FirstOrDefault();
        }

        if (_player == null)
        {
            return;
        }

        foreach (var entity in entities)
        {
            switch (entity.input.KeyCode)
            {
                case KeyCode.A:
                    _player.ReplaceMove(new Direction(-1, 0));
                    break;
                case KeyCode.D:
                    _player.ReplaceMove(new Direction(1, 0));
                    break;
                case KeyCode.W:
                    _player.ReplaceMove(new Direction(0, 1));
                    break;
                case KeyCode.S:
                    _player.ReplaceMove(new Direction(0, -1));
                    break;
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasInput;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }
}
