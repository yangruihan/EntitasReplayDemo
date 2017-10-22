using System.Collections.Generic;
using Entitas;

[Game]
public class InputRecordsComponent : IComponent
{
    public int CurrentTick;
    public List<InputRecordData> Value;
}
