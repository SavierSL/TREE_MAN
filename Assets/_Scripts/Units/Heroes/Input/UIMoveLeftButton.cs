using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIMoveLeftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().LeftButton_Down();
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().isMoving = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("UP NAAA");
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().LeftButton_Up();
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().isMoving = false;
    }
}
