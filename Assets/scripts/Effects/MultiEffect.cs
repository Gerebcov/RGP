using UnityEngine;

public class MultiEffect : MonoBehaviour, IEffect
{
    [SerializeField]
    MonoBehaviour[] effects;

    public float Duration => GetTime();

    public void Activate()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            var effect = effects[i] as IEffect;
            effect.Activate();
        }
    }

    public void Deactivate()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            var effect = effects[i] as IEffect;
            effect.Deactivate();
        }
    }

    float GetTime()
    {
        float result = 0;
        for (int i = 0; i < effects.Length; i++)
        {
            var effect = effects[i] as IEffect;
            if (result < effect.Duration)
                result = effect.Duration;
        }
        return result;
    }
}
