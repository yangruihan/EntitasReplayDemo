using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class IDComponent : IComponent
{
    [EntityIndex]
    public int Value;
}
