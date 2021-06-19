using UnityEngine;

public class ForceEffect : MonoBehaviour, IEffect
{
    [SerializeField]
    GameObject trigger;
    [SerializeField]
    PointEffector2D effector2D;
    [SerializeField]
    float force;
    [SerializeField]
    float timeEffect;
    [SerializeField]
    AnimationCurve forceCurve;

    float startTime;
    float endTime = 0;

    public float Duration => timeEffect;

    public void Activate()
    {
        trigger.SetActive(true);
        startTime = Time.time;
        endTime = startTime + timeEffect;
    }

    void Update()
    {
        if(effector2D.enabled)
        {
            float progress = Mathf.InverseLerp(startTime, endTime, Time.time);
            effector2D.forceMagnitude = forceCurve.Evaluate(progress) * force;
            if (progress >= 1)
                Deactivate();
        }
    }

    public void Deactivate()
    {
        trigger.SetActive(false);
    }
}