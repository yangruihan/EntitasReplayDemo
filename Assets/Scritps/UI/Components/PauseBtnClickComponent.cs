using Entitas;

[Game]
public class PauseBtnClickComponent : IComponent
{
    public EnmPauseBtnStatus Status;
}

public enum EnmPauseBtnStatus
{
    Pause,
    Running,
}