using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D enemyRB;
    public Transform playerPos;
    void Start()
    {
        playerPos = FindObjectOfType<TreeMan>().transform;
    }

    // Update is called once per frame

}
