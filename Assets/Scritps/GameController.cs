using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Systems systems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        systems = new Systems();

        var inputSystems = CreateInputSystems(contexts);
        systems.Add(inputSystems);

        var generalSystems = CreateGeneralSystems(contexts);
        systems.Add(generalSystems);

        var logicSystems = CreateLogicSystems(contexts);
        systems.Add(logicSystems);

        var replaySystems = CreateReplaySystems(contexts);
        systems.Add(replaySystems);

        contexts.game.SetReplaySystem(logicSystems);

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

    /// <summary>
    /// General
    /// </summary>
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

            .Add(new ChangeTickSystem(contexts))
            .Add(new InputHandleSystem(contexts))
            .Add(new NotifyTickChangeSystem(contexts))
            ;
    }

    Systems CreateReplaySystems(Contexts contexts)
    {
        return new Feature("Replay")
            .Add(new InputRecordSystem(contexts))
            ;
    }
}
