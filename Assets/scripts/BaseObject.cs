using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    ObjectTypes type;

    public ObjectTypes Type => type;
}

[System.FlagsAttribute]
public enum ObjectTypes : byte
{
    Word = 1,
    Player = 2,
    Enemy = 4
}
