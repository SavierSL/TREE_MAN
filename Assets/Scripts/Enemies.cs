using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] Enemy enemy;
  private float currentHealth;
  void Start()
  {
    currentHealth = enemy.enemyHP;
  }

  // Update is called once per frame
  void Update()
  {
    if (currentHealth <= 0)
    {
      Destroy(gameObject);
    }
  }
  // public void TakeDamage(float damage)
  // {
  //   Instantiate(enemy.textDMG, transform.position, Quaternion.identity);
  //   currentHealth -= damage;
  // }
}
