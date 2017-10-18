using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class PlayerComponent : IComponent
{
    [EntityIndex]
    public bool Value;
}
