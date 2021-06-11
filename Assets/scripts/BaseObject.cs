using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    ObjectTypes type;

    public ObjectTypes Type => type;
}

[System.Flags]
public enum ObjectTypes 
{
    Word = 0x1,
    Player = 0x2,
    Enemy = 0x4,
}
