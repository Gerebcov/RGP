using UnityEngine;

public class Bulet : DamageSender
{
    [SerializeField]
    Rigidbody2D rigidbody2D;
    [SerializeField]
    Vector2 velocity;
    [SerializeField]
    Vector2 vector;

    private void Start()
    {
        rigidbody2D.velocity = velocity * vector;
    }

    public void SetVector(Vector2 vector)
    {
        this.vector = vector;
    }

    public override void Contact(IDamageHandler handler)
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }
}
