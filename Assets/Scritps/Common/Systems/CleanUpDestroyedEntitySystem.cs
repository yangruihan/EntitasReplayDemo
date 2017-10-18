using System.Collections.Generic;
using Entitas;

public class CleanUpDestroyedEntitySystem : ICleanupSystem
{
    private Contexts _contexts;
    private IGroup<GameEntity> _destroyedGroup;

    public CleanUpDestroyedEntitySystem(Contexts _contexts)
    {
        this._contexts = _contexts;
        _destroyedGroup = _contexts.game.GetGroup(GameMatcher.Destroyed);
    }

    public void Cleanup()
    {
        foreach (var entity in _destroyedGroup.GetEntities())
        {
            entity.Destroy();
        }
    }
}
