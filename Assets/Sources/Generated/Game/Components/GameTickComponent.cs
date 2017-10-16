//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity tickEntity { get { return GetGroup(GameMatcher.Tick).GetSingleEntity(); } }
    public TickComponent tick { get { return tickEntity.tick; } }
    public bool hasTick { get { return tickEntity != null; } }

    public GameEntity SetTick(int newValue) {
        if (hasTick) {
            throw new Entitas.EntitasException("Could not set Tick!\n" + this + " already has an entity with TickComponent!",
                "You should check if the context already has a tickEntity before setting it or use context.ReplaceTick().");
        }
        var entity = CreateEntity();
        entity.AddTick(newValue);
        return entity;
    }

    public void ReplaceTick(int newValue) {
        var entity = tickEntity;
        if (entity == null) {
            entity = SetTick(newValue);
        } else {
            entity.ReplaceTick(newValue);
        }
    }

    public void RemoveTick() {
        tickEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TickComponent tick { get { return (TickComponent)GetComponent(GameComponentsLookup.Tick); } }
    public bool hasTick { get { return HasComponent(GameComponentsLookup.Tick); } }

    public void AddTick(int newValue) {
        var index = GameComponentsLookup.Tick;
        var component = CreateComponent<TickComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTick(int newValue) {
        var index = GameComponentsLookup.Tick;
        var component = CreateComponent<TickComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTick() {
        RemoveComponent(GameComponentsLookup.Tick);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTick;

    public static Entitas.IMatcher<GameEntity> Tick {
        get {
            if (_matcherTick == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Tick);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTick = matcher;
            }

            return _matcherTick;
        }
    }
}
