using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MortalObject
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField]
    float speed;
    [Range(0, .3f)] [SerializeField] private float m_FlySmoothing = .05f;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jampReload;

    [SerializeField]
    UnitVisual visual;
    [SerializeField]
    Rigidbody2D rigidbody;

    [SerializeField]
    Collider2D platformColliser;
    [SerializeField]
    GameTrigger checkLandTrigger;

    private Vector3 m_Velocity = Vector3.zero;
    int jampCount = 1; 
    bool canJamp = true;

    bool jamp = false;
    bool fall = false;

    private void Awake()
    {
        if(checkLandTrigger)
            checkLandTrigger.OnActive += CheckLandTrigger_OnEnter;
    }

    private void CheckLandTrigger_OnEnter()
    {
        canJamp = true;
        jampCount = 1;
        if (rigidbody.velocity.y <= 0)
            jamp = false;
    }

    public virtual void Move(float vector)
    {
        if (checkLandTrigger.Enter || (!checkLandTrigger.Enter && vector != 0))
        {
            var currentSpeed = speed * vector;
            var targetVelocity = new Vector2(currentSpeed, rigidbody.velocity.y);
            var smoothing = checkLandTrigger.Enter ? m_MovementSmoothing : m_FlySmoothing;
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref m_Velocity, smoothing);
        }
        if(vector != 0)
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

        jamp = true;
        canJamp = false;
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        StartCoroutine(ReloadJamp());
        fall = false;
    }

    public void Fall()
    {
        if (fall || !checkLandTrigger.Enter)
            return;
        fall = true;
        platformColliser.enabled = false;
        StartCoroutine(ReloadFall());
    }

    IEnumerator ReloadFall()
    {
        yield return new WaitForSeconds(0.5f);
        fall = false;
    }    

    private void Update()
    {
        if(!fall)
            platformColliser.enabled = (rigidbody.velocity.y <= 0) || !jamp;
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
        if((Type & ObjectTypes.Player) != 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
