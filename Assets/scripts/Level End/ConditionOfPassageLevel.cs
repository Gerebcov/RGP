using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionOfPassageLevel : MonoBehaviour
{
    [HideInInspector] public bool isDone = false;
    [HideInInspector] public EnemyToPass[] enemysToPass;
    [HideInInspector] public float startTime;
    [SerializeField] TypeOfCondition typeOfCondition;
    void Start()
    {
        switch (typeOfCondition)
        {
            case TypeOfCondition.defeatEnemy:
                enemysToPass = GetComponents<EnemyToPass>();
                break;
            case TypeOfCondition.nothing:
                isDone = true;
                break;
            case TypeOfCondition.byTime:
                startTime = Time.time;
                break;
        }
    }
    enum TypeOfCondition
    {
        defeatEnemy,
        nothing,
        byTime
    }
}
