using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
  [SerializeField] Transform ghostParent;
  public Color trailColor;
  private SpriteRenderer sr;
  public Color fadeColor;
  public float fadeTime;
  public float ghostInterval;
  public float fadeTimeAtk;
  public float ghostIntervalAtk;
  private Animation animator;
  // Start is called before the first frame update
  void Start()
  {
    animator = FindObjectOfType<Animation>();
    sr = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame

  // public void ShowGhost()
  // {

  //   Sequence sequence = DOTween.Sequence();
  //   for (int i = 0; i < ghostParent.childCount; i++)
  //   {
  //     Transform currentGhost = ghostParent.GetChild(i);
  //     sequence.AppendCallback(() => currentGhost.position = movement.transform.position);
  //     sequence.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = animator.sr.sprite);
  //     sequence.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
  //     sequence.AppendCallback(() => FadeSprite(currentGhost));
  //     sequence.AppendInterval(ghostInterval);
  //   }
  // }
  // public void FadeSprite(Transform current)
  // {
  //   current.GetComponent<SpriteRenderer>().material.DOKill();
  //   current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
  // }
  public void ShowGhost()
  {
    Sequence sequence = DOTween.Sequence();

    for (int i = 0; i < ghostParent.childCount; i++)
    {
      Transform currentGhost = ghostParent.GetChild(i);
      sequence.AppendCallback(() => currentGhost.position = FindAnyObjectByType<TreeMan>().transform.position);
      sequence.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = animator.sr.sprite);
      sequence.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
      sequence.AppendCallback(() => FadeSprite(currentGhost, fadeTime));
      sequence.AppendInterval(ghostInterval);
    }
  }
  public void ShowGhostAtk()
  {
    Sequence sequence = DOTween.Sequence();

    for (int i = 0; i < ghostParent.childCount; i++)
    {

      Transform currentGhost = ghostParent.GetChild(i);
      sequence.AppendCallback(() => currentGhost.position = FindAnyObjectByType<TreeMan>().transform.position);
      sequence.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = animator.sr.sprite);
      sequence.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
      sequence.AppendCallback(() => FadeSprite(currentGhost, fadeTimeAtk));
      sequence.AppendInterval(ghostIntervalAtk);
    }
  }

  public void FadeSprite(Transform current, float time)
  {
    current.GetComponent<SpriteRenderer>().material.DOKill();
    current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, time);
  }
}
