using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] Collider2D PlayerCollider;
    [SerializeField] Collider2D exitCollider;
    [SerializeField] Animation idleAnimation;
    [SerializeField] Animation endAnimation;
    public ConditionOfPassageLevel[] ConditionOfPassageLevel;
    bool isReadyToPass = false;
    

    private void Start()
    {
        idleAnimation.Play();
    }

    void CheckConditions()
    {
        isReadyToPass = true;
        for (int i = 0; i< ConditionOfPassageLevel.Length; i++)
	    {
            isReadyToPass = ConditionOfPassageLevel[i].isDone || isReadyToPass;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckConditions();
        if(collision.collider == PlayerCollider || isReadyToPass)
        {
            endAnimation.Play();
            // block input
            //show level end animation
            //reload scene
        }
    }
}
