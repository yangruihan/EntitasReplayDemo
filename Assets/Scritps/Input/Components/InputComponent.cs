using Entitas;
using UnityEngine;

[Game]
public class InputComponent : IComponent
{
    public int ID;
    public int Tick;
    public KeyCode KeyCode;
}
