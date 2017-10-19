using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class LastTickComponent : IComponent
{
    public int Value;
}
