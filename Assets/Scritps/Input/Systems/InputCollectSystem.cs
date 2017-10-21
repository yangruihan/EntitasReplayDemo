using Entitas;
using UnityEngine;

public class InputCollectSystem : IExecuteSystem
{
    private Contexts _contexts;

    public InputCollectSystem(Contexts _contexts)
    {
        this._contexts = _contexts;
    }

    public void Execute()
    {
        if (_contexts.game.gameStatus.Value != EnmGameStatus.Running)
            return;

        if (Input.anyKey)
        {
            var code = KeyCode.Keypad0;
            if (Input.GetKey(KeyCode.A))
            {
                code = KeyCode.A;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                code = KeyCode.D;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                code = KeyCode.S;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                code = KeyCode.W;
            }

            if (code != KeyCode.Keypad0)
            {
                var entity = _contexts.game.CreateEntity();
                entity.AddInput(0, _contexts.game.tick.Value, code);
            }
        }
    }
}
