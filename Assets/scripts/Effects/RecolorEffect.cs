using System.Collections;
using UnityEngine;

public class RecolorEffect : MonoBehaviour, IEffect
{
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Gradient gradient;
    [SerializeField]
    float time;


    public float Duration => time;

    public void Activate()
    {
        StopAllCoroutines();
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        float startTime = Time.time;
        float EndTime = startTime + time;
        float progress = 0;
        while(progress < 1)
        {
            progress = Mathf.Clamp01(Mathf.InverseLerp(startTime, EndTime, Time.time));
            var color = gradient.Evaluate(progress);
            sprite.color = color;
            yield return null;
        }
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        var color = gradient.Evaluate(1);
        sprite.color = color;
    }
}