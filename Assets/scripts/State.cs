using System;

[Serializable]
public class State
{
    public Action Start;
    public Action Update;
    public Action End;

    public State (Action start, Action update, Action end)
    {
        Start = start;
        Update = update;
        end = End;
    }
}