using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class GameStatusComponent : IComponent
{
    public EnmGameStatus Value;
}
