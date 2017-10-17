using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private Systems systems;

    void Start()
    {
        var contexts = Contexts.sharedInstance;

        systems = createSystems(contexts);
        var logicSystems = createLogicSystems(contexts);
        systems.Add(logicSystems);
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

    Systems createSystems(Contexts contexts)
    {
        return new Feature("Game")
            .Add(new GameInitializeSystem(contexts))

            .Add(new CleanUpDestroyedEntitySystem(contexts))
            ;
    }

    Systems createLogicSystems(Contexts contexts)
    {
        return new Feature("Logic")
            .Add(new TickInitializeSystem(contexts))

            .Add(new UpdateDeltaTimeSystem(contexts))

            .Add(new ChangeTickSystem(contexts))
            .Add(new NotifyTickChangeSystem(contexts))
            ;
    }
}
