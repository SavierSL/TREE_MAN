using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIJumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {

        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().OnJumpInput(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("JUMP LET GO");
        FindAnyObjectByType<TreeMan>().GetComponent<HeroInput>().OnJumpInput(false);
    }
}
