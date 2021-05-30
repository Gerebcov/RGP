using UnityEngine;

public class Bulet : DamageSender
{
    [SerializeField]
    protected Rigidbody2D rigidbody2D;
    [SerializeField]
    float velocity;
    [SerializeField]
    Vector2 vector;

    [SerializeField]
    float autoDestroyTime = 1.5f;
    float time = 0;

    private void Start()
    {
        rigidbody2D.velocity = velocity * vector;
    }

    public void SetVector(Vector2 vector)
    {
        this.vector = vector;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= autoDestroyTime)
            Destroy(gameObject);
    }
}
