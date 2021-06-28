using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Image bar;
    [SerializeField]
    MortalObject mortalObject;
    float heetPointsOnStart;


    private void Start()
    {
        heetPointsOnStart = mortalObject.HeetsPoint;
        mortalObject.OnSetDamage += UpdateHealthBar;
        UpdateHealthBar();
    }

    private void OnDestroy()
    {
        mortalObject.OnSetDamage -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        bar.fillAmount = Mathf.InverseLerp(0, heetPointsOnStart, mortalObject.HeetsPoint);
        
    }

}
