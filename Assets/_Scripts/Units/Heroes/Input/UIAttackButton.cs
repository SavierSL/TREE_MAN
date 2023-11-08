using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAttackbutton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ATK DOWN");
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().AttackButton_Down();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("ATK UP");
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().AttackButton_Up();
    }
}
