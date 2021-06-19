using UnityEngine;

public class EnableEffect : MonoBehaviour, IEffect
{
    [SerializeField]
    GameObject enableObject;
    public float Duration => 0;

    public void Activate()
    {
        enableObject.SetActive(true);
    }

    public void Deactivate()
    {
        enableObject.SetActive(false);
    }
}
