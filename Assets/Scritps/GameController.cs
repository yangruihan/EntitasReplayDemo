using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LogicInitData LogicInitData;
    public PlayerInitData PlayerInitData;

    private Systems systems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        contexts.game.SetPlayerInitData(PlayerInitData);
        contexts.game.SetLogicTime(0, 1.0f / LogicInitData.TargetFrameRate, LogicInitData.TargetFrameRate);

        systems = new Systems();

        var inputSystems = CreateInputSystems(contexts);
        systems.Add(inputSystems);

        var generalSystems = CreateGeneralSystems(contexts);
        systems.Add(generalSystems);

        var logicSystems = CreateLogicSystems(contexts);
        systems.Add(logicSystems);

        var afterLogicSystems = CreateAfterLogicSystems(contexts);
        systems.Add(afterLogicSystems);

        var replaySystems = CreateReplaySystems(contexts);
        systems.Add(replaySystems);

        var uiSystems = CreateUISystems(contexts);
        systems.Add(uiSystems);

        contexts.game.SetLogicSystem(logicSystems);

        systems.Initialize();
    }

    void Update()
    {
        systems.Execute();
        systems.Cleanup();
    }

    void OnDestroy()
    {
        systems.TearDown();
    }

    Systems CreateGeneralSystems(Contexts contexts)
    {
        return new Feature("Game")
            .Add(new GameInitializeSystem(contexts))

            .Add(new UpdateDeltaTimeSystem(contexts))
            .Add(new PushTickSystem(contexts))

            .Add(new ChangeGameTimeSystem(contexts))

            .Add(new CleanUpDestroyedEntitySystem(contexts))
            ;
    }

    Systems CreateInputSystems(Contexts contexts)
    {
        return new Feature("Input")
            .Add(new InputCollectSystem(contexts))

            .Add(new CleanUpInputSystem(contexts))
            ;
    }

    Systems CreateLogicSystems(Contexts contexts)
    {
        return new Feature("Logic")
            .Add(new LogicTimeInitializeSystem(contexts))
            .Add(new TickInitializeSystem(contexts))
            .Add(new PlayerInitializeSystem(contexts))

            .Add(new PlayerBindScriptSystem(contexts))
            .Add(new ChangeTickSystem(contexts))
            .Add(new InputHandleSystem(contexts))
            .Add(new NotifyTickChangeSystem(contexts))
            .Add(new MovementSystem(contexts))
            .Add(new ChangePlayerPositionSystem(contexts))
            ;
    }

    Systems CreateAfterLogicSystems(Contexts contexts)
    {
        return new Feature("After Logic")
            .Add(new ChangeGameStatusSystems(contexts))
            ;
    }

    Systems CreateReplaySystems(Contexts contexts)
    {
        return new Feature("Replay")
            .Add(new InputRecordSystem(contexts))
            .Add(new PositionRecordSystem(contexts))
            .Add(new ReplaySystem(contexts))
            ;
    }

    Systems CreateUISystems(Contexts contexts)
    {
        return new Feature("UI")
            .Add(new ChangeShowTextContentSystem(contexts))
            .Add(new PauseButtonEventHandleSystem(contexts))
            .Add(new ChangeRecordSliderEnableSystem(contexts))
            .Add(new ChangeRecordSliderValueSystem(contexts))
            ;
    }
}
