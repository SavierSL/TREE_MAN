using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CombatAtk : MonoBehaviour
{
  // Start is called before the first frame update
  private Animator animator;
  private bool isAttacking;
  [SerializeField] Transform combatCollider;
  [SerializeField] float combatRadius;
  [SerializeField] LayerMask enemiesLayer;


  void Start()
  {
    animator = GetComponentInChildren<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    // if (Input.GetKeyDown(KeyCode.Mouse1))
    // {

    //   isAttacking = true;
    //   Collider2D[] enemies = Physics2D.OverlapCircleAll(combatCollider.transform.position, combatRadius, enemiesLayer);
    //   foreach (Collider2D enemy in enemies)
    //   {
    //     FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
    //     Debug.Log("Can attack enemey");
    //     enemy.GetComponent<Enemies>().TakeDamage(10);
    //     Camera.main.transform.DOComplete();
    //     Camera.main.transform.DOShakePosition(.1f, .2f, 7, 45, false, true);
    //   }
    //   animator.SetBool("isAtking", isAttacking);
    //   Debug.Log("ATTACKED");
    //   StartCoroutine(FinishedAttacking());
    // }
  }
  IEnumerator FinishedAttacking()
  {
    yield return new WaitForSeconds(.1f);
    // FindAnyObjectByType<GhostTrail>().ShowGhostAtk();
    if (isAttacking)
    {

      isAttacking = false;
      animator.SetBool("isAtking", isAttacking);

    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(combatCollider.position, combatRadius);
  }
}
