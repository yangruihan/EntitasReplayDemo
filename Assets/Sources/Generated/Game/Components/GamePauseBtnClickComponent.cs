//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PauseBtnClickComponent pauseBtnClick { get { return (PauseBtnClickComponent)GetComponent(GameComponentsLookup.PauseBtnClick); } }
    public bool hasPauseBtnClick { get { return HasComponent(GameComponentsLookup.PauseBtnClick); } }

    public void AddPauseBtnClick(EnmPauseBtnStatus newStatus) {
        var index = GameComponentsLookup.PauseBtnClick;
        var component = CreateComponent<PauseBtnClickComponent>(index);
        component.Status = newStatus;
        AddComponent(index, component);
    }

    public void ReplacePauseBtnClick(EnmPauseBtnStatus newStatus) {
        var index = GameComponentsLookup.PauseBtnClick;
        var component = CreateComponent<PauseBtnClickComponent>(index);
        component.Status = newStatus;
        ReplaceComponent(index, component);
    }

    public void RemovePauseBtnClick() {
        RemoveComponent(GameComponentsLookup.PauseBtnClick);
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

    static Entitas.IMatcher<GameEntity> _matcherPauseBtnClick;

    public static Entitas.IMatcher<GameEntity> PauseBtnClick {
        get {
            if (_matcherPauseBtnClick == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PauseBtnClick);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPauseBtnClick = matcher;
            }

            return _matcherPauseBtnClick;
        }
    }
}
