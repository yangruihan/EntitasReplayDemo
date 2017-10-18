using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class RecordsComponent : IComponent
{
    public List<InputRecordData> InputRecords;
}
