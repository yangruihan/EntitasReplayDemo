using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class InputRecordsComponent : IComponent
{
    public List<InputRecordData> Value;
}
