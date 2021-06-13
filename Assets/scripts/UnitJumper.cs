using System.Collections;
using UnityEngine;

public class UnitJumper : UnitModule
{

    [SerializeField]
    Rigidbody2D rigidbody2D;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    int jumpCount = 1;
    [SerializeField]
    float nextJumpDelay;

    [SerializeField]
    MonoBehaviour landTrigger;

    ITrigger LandTrigger => landTrigger as ITrigger;

    [SerializeField]
    UnitModule[] blokedModules;

    int lostJump;
    float nextJumpTime = 0;

    private void Start()
    {
        lostJump = jumpCount;
        LandTrigger.OnActive += LandTrigger_OnActive;
    }

    protected virtual void LandTrigger_OnActive()
    {
        lostJump = jumpCount;
        if (nextJumpTime > Time.time)
        {
            StopAllCoroutines();
            UnblokModules();
        }
    }

    public virtual void Jump()
    {
        if ((!LandTrigger.IsActive && lostJump == 0) || nextJumpTime > Time.time)
            return;

        var targetVelocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        rigidbody2D.velocity = targetVelocity;
        nextJumpTime = Time.time + nextJumpDelay;
        StartCoroutine(BlockModules());

        if (!LandTrigger.IsActive)
            lostJump--;
    }

    IEnumerator BlockModules()
    {
        for (int i = 0; i < blokedModules.Length; i++)
        {
            blokedModules[i].Block();
        }
        yield return new WaitForSeconds(nextJumpDelay);
        UnblokModules();
    }

    void UnblokModules()
    {
        for (int i = 0; i < blokedModules.Length; i++)
        {
            blokedModules[i].Unblock();
        }
    }
}
