using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSlider;
    void Start()
    {
        healthSlider.minValue = 1;
        gameObject.SetActive(false);
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
    public void SetPos(Transform newPos)
    {
        transform.position = newPos.position;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
