using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] InteractableTrigger exitTrigger;
    public ConditionOfPassageLevel[] ConditionsOfPassageLevel;

    private void Start()
    {
        exitTrigger.OnActive += ExitTrigger_OnActive;
    }

    private void ExitTrigger_OnActive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    bool CheckConditions()
    {
        for (int i = 0; i< ConditionsOfPassageLevel.Length; i++)
	    {
            if (ConditionsOfPassageLevel[i].isDone == false)
                return false;
        }
        return true;
    }    
}
