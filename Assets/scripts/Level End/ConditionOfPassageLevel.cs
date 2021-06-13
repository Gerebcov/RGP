using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionOfPassageLevel : MonoBehaviour
{
    [HideInInspector] public bool isDone = false;
    [SerializeField] TypeOfCondition typeOfCondition;
    EnemyToPass[] enemysToPass;



    private void Update()
    {
        
    }

    void Start()
    {
        /*switch (typeOfCondition)
        {
            case defeatEnemy:
                EnemyToPass[] enemysToPass = GetComponents<EnemyToPass>();
                break;
            case 1:
                isDone = true;
                break;
            case 2:
                float startTime = Time.time;
                break;

        }*/

        
    }

    enum TypeOfCondition
    {
        defeatEnemy,
        nothing,
        byTime
    }
}
