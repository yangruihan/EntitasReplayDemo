using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class ReplaySystemComponent : IComponent
{
    public ReplaySystem Value;
}
