using Entitas;

public class ReplayUtil
{
    public static void Replay(Contexts contexts, Systems logicSys, int toTick, GameEntity[] recordEntities)
    {
        logicSys.Initialize();

        int[] inputActionIndexArr = new int[recordEntities.Length];
        int startTick = 0;

        if (recordEntities.Length > 0)
        {
            var positionRecords = recordEntities[0].positionRecords.Value;
            for (int i = positionRecords.Count - 1; i >= 0; i--)
            {
                var pos = positionRecords[i];
                if (pos.Tick <= toTick)
                {
                    startTick = pos.Tick;

                    // replace record entities pos
                    foreach (var recordEntity in recordEntities)
                    {
                        recordEntity.ReplacePosition(recordEntity.positionRecords.Value[i].Position);
                    }

                    break;
                }
            }
        }

        if (startTick == toTick)
        {
            contexts.game.ReplaceTick(startTick);
            contexts.game.ReplaceLogicTime(
                startTick * contexts.game.logicTime.DeltaTime,
                contexts.game.logicTime.DeltaTime,
                contexts.game.logicTime.TargetFrameRate
                );
            return;
        }

        // ignore input actions before startTick 
        if (startTick != 0)
        {
            for (int i = 0; i < recordEntities.Length; i++)
            {
                var inputRecords = recordEntities[i].inputRecords.Value;
                while (inputRecords.Count > inputActionIndexArr[i] &&
                       inputRecords[inputActionIndexArr[i]].Tick <= startTick)
                {
                    inputActionIndexArr[i]++;
                }
            }

            startTick++;

            contexts.game.ReplaceTick(startTick);
            contexts.game.ReplaceLogicTime(
                startTick * contexts.game.logicTime.DeltaTime,
                contexts.game.logicTime.DeltaTime,
                contexts.game.logicTime.TargetFrameRate
                );
        }

        for (int i = startTick; i < toTick; i++)
        {
            for (int j = 0; j < recordEntities.Length; j++)
            {
                var inputRecords = recordEntities[j].inputRecords.Value;
                while (inputRecords.Count > inputActionIndexArr[j] &&
                       inputRecords[inputActionIndexArr[j]].Tick == contexts.game.tick.Value)
                {
                    var inputAction = inputRecords[inputActionIndexArr[j]];
                    contexts.game.ReplaceInput(recordEntities[j].iD.Value, inputAction.Tick, inputAction.KeyCode);
                    inputActionIndexArr[j]++;

                    logicSys.Execute();
                    logicSys.Cleanup();
                }
            }

            contexts.game.ReplacePushTick(true);
            logicSys.Execute();
            logicSys.Cleanup();

        }
    }
}
