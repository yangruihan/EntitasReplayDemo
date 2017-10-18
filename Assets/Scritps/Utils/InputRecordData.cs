using UnityEngine;

public class InputRecordData
{
    public int Tick;
    public KeyCode KeyCode;

    public InputRecordData(int tick, KeyCode keyCode)
    {
        Tick = tick;
        KeyCode = keyCode;
    }
}
