//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity playerInitDataEntity { get { return GetGroup(GameMatcher.PlayerInitData).GetSingleEntity(); } }
    public PlayerInitDataComponent playerInitData { get { return playerInitDataEntity.playerInitData; } }
    public bool hasPlayerInitData { get { return playerInitDataEntity != null; } }

    public GameEntity SetPlayerInitData(PlayerInitData newValue) {
        if (hasPlayerInitData) {
            throw new Entitas.EntitasException("Could not set PlayerInitData!\n" + this + " already has an entity with PlayerInitDataComponent!",
                "You should check if the context already has a playerInitDataEntity before setting it or use context.ReplacePlayerInitData().");
        }
        var entity = CreateEntity();
        entity.AddPlayerInitData(newValue);
        return entity;
    }

    public void ReplacePlayerInitData(PlayerInitData newValue) {
        var entity = playerInitDataEntity;
        if (entity == null) {
            entity = SetPlayerInitData(newValue);
        } else {
            entity.ReplacePlayerInitData(newValue);
        }
    }

    public void RemovePlayerInitData() {
        playerInitDataEntity.Destroy();
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

    public PlayerInitDataComponent playerInitData { get { return (PlayerInitDataComponent)GetComponent(GameComponentsLookup.PlayerInitData); } }
    public bool hasPlayerInitData { get { return HasComponent(GameComponentsLookup.PlayerInitData); } }

    public void AddPlayerInitData(PlayerInitData newValue) {
        var index = GameComponentsLookup.PlayerInitData;
        var component = CreateComponent<PlayerInitDataComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlayerInitData(PlayerInitData newValue) {
        var index = GameComponentsLookup.PlayerInitData;
        var component = CreateComponent<PlayerInitDataComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerInitData() {
        RemoveComponent(GameComponentsLookup.PlayerInitData);
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

    static Entitas.IMatcher<GameEntity> _matcherPlayerInitData;

    public static Entitas.IMatcher<GameEntity> PlayerInitData {
        get {
            if (_matcherPlayerInitData == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerInitData);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerInitData = matcher;
            }

            return _matcherPlayerInitData;
        }
    }
}
