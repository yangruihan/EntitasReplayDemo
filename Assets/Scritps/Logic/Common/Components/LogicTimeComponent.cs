using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class LogicTimeComponent : IComponent
{
    public float Time;
    public float DeltaTime;
    public int TargetFrameRate;
}
