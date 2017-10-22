using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class TickComponent : IComponent
{
    public int Value;
}
