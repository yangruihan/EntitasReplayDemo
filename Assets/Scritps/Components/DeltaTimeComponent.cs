using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class DeltaTimeComponent : IComponent
{
    public float Value;
}
