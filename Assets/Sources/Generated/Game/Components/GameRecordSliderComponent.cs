//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RecordSliderComponent recordSlider { get { return (RecordSliderComponent)GetComponent(GameComponentsLookup.RecordSlider); } }
    public bool hasRecordSlider { get { return HasComponent(GameComponentsLookup.RecordSlider); } }

    public void AddRecordSlider(RecordSliderBehaviour newValue) {
        var index = GameComponentsLookup.RecordSlider;
        var component = CreateComponent<RecordSliderComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRecordSlider(RecordSliderBehaviour newValue) {
        var index = GameComponentsLookup.RecordSlider;
        var component = CreateComponent<RecordSliderComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRecordSlider() {
        RemoveComponent(GameComponentsLookup.RecordSlider);
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

    static Entitas.IMatcher<GameEntity> _matcherRecordSlider;

    public static Entitas.IMatcher<GameEntity> RecordSlider {
        get {
            if (_matcherRecordSlider == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RecordSlider);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRecordSlider = matcher;
            }

            return _matcherRecordSlider;
        }
    }
}