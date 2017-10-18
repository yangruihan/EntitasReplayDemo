using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerInitData PlayerInitData;

    private Systems systems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        contexts.game.SetPlayerInitData(PlayerInitData);

        systems = new Systems();

        var inputSystems = CreateInputSystems(contexts);
        systems.Add(inputSystems);

        var generalSystems = CreateGeneralSystems(contexts);
        systems.Add(generalSystems);

        var logicSystems = CreateLogicSystems(contexts);
        systems.Add(logicSystems);

        var replaySystems = CreateReplaySystems(contexts);
        systems.Add(replaySystems);

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

            .Add(new PushTickSystem(contexts))
            .Add(new UpdateDeltaTimeSystem(contexts))

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

    Systems CreateReplaySystems(Contexts contexts)
    {
        return new Feature("Replay")
            .Add(new InputRecordSystem(contexts))
            .Add(new ReplaySystem(contexts))
            ;
    }
}
