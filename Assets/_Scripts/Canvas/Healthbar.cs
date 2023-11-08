using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSlider;
    private void Start()
    {
        healthSlider.minValue = 1;
    }

    public void SetHealth(float maxValue)
    {
        healthSlider.maxValue = maxValue;
    }

    public void DecreaseHealth(float damage)
    {
        healthSlider.value -= damage;
    }
    public void CurrentValue(float healthValue)
    {
        healthSlider.value = healthValue;
    }
}
