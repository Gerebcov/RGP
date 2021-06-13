[System.Serializable]
public class UnitCommand
{
    [System.Flags]
    public enum CommandTypes
    {
        Move = 1,
        Jump = 2,
        Fly = 4,
        Fall = 8
    }
    public CommandTypes CommandType;
    public object Data;

    public UnitCommand(CommandTypes commandType, object data)
    {
        CommandType = commandType;
        Data = data;
    }
}
