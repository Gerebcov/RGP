using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MortalObject
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField]
    float speed;
    [SerializeField]
    float flyForce;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jampReload;

    [SerializeField]
    UnitVisual visual;
    [SerializeField]
    Rigidbody2D rigidbody;

    [SerializeField]
    GameTrigger checkLandTrigger;

    private Vector3 m_Velocity = Vector3.zero;
    int jampCount = 1; 
    bool canJamp = true;

    private void Awake()
    {
        if(checkLandTrigger)
            checkLandTrigger.OnActive += CheckLandTrigger_OnEnter;
    }

    private void CheckLandTrigger_OnEnter()
    {
        canJamp = true;
        jampCount = 1;
    }

    public virtual void Move(float vector)
    {
        if (checkLandTrigger.Enter)
        {
            var currentSpeed = speed * vector;
            var targetVelocity = new Vector2(currentSpeed, rigidbody.velocity.y);
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        else
        {
            rigidbody.AddRelativeForce(Vector2.right * vector * flyForce, ForceMode2D.Force);
        }
        visual.Flip(vector < 0);
    }

    public virtual void Jump()
    {
        if (!canJamp)
            return;

        if (!checkLandTrigger.Enter && jampCount > 0)
            jampCount--;
        else if (jampCount == 0)
            return;

        canJamp = false;
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        StartCoroutine(ReloadJamp());
    }

    IEnumerator ReloadJamp()
    {
        yield return new WaitForSeconds(jampReload);
        if (!canJamp)
            canJamp = true;
    }

    protected override void Death()
    {
        base.Death();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
