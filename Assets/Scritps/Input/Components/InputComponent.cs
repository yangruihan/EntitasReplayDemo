using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class InputComponent : IComponent
{
    public int ID;
    public int Tick;
    public KeyCode KeyCode;
}
