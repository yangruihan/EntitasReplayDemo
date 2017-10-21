using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class PositionRecordsComponent : IComponent
{
    public List<PositionRecordData> Value;
}
