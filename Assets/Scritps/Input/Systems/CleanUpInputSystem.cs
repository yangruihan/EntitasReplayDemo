using Entitas;

public class CleanUpInputSystem : ICleanupSystem
{
    private Contexts _contexts;
    private IGroup<GameEntity> _inputGroup;

    public CleanUpInputSystem(Contexts _contexts)
    {
        this._contexts = _contexts;
        _inputGroup = _contexts.game.GetGroup(GameMatcher.Input);
    }

    public void Cleanup()
    {
        foreach (var entity in _inputGroup.GetEntities())
        {
            entity.Destroy();
        }
    }
}
