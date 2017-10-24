using Entitas;
using UnityEngine;

public class InputCollectSystem : IExecuteSystem
{
    private Contexts _contexts;
    private int _index = 0;

    public InputCollectSystem(Contexts _contexts)
    {
        this._contexts = _contexts;
    }

    public void Execute()
    {
        if (_contexts.game.gameStatus.Value != EnmGameStatus.Running)
            return;

        int id = 0;

        if (Input.anyKey)
        {
            var code = KeyCode.Keypad0;

            if (_index == 0)
            {
                _index = 1;
                if (Input.GetKey(KeyCode.A))
                {
                    code = KeyCode.A;
                    id = 0;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    code = KeyCode.D;
                    id = 0;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    code = KeyCode.S;
                    id = 0;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    code = KeyCode.W;
                    id = 0;
                }
            }
            else
            {
                _index = 0;
                if (Input.GetKey(KeyCode.J))
                {
                    code = KeyCode.J;
                    id = 1;
                }
                else if (Input.GetKey(KeyCode.K))
                {
                    code = KeyCode.K;
                    id = 1;
                }
                else if (Input.GetKey(KeyCode.I))
                {
                    code = KeyCode.I;
                    id = 1;
                }
                else if (Input.GetKey(KeyCode.L))
                {
                    code = KeyCode.L;
                    id = 1;
                }
            }

            if (code != KeyCode.Keypad0)
            {
                _contexts.game.ReplaceInput(id, _contexts.game.tick.Value, code);
            }
        }
    }
}
