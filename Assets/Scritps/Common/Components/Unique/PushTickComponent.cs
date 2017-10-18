using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class PushTickComponent : IComponent
{
    public bool Value;
}
