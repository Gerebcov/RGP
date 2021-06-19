using UnityEngine;

public class Arrow: Bullet
{
    private void Update()
    {
        Quaternion quaternion = Quaternion.LookRotation(rigidbody2D.velocity, Vector2.right);
        rigidbody2D.SetRotation(quaternion);
    }
}
