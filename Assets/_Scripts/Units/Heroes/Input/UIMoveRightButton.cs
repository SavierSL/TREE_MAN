using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMoveRightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {

        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().RightButton_Down();
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().isMoving = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {

        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().RightButton_Up();
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().isMoving = false;
    }

}


