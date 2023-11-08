using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
  public Vector3 updatedPosition;
  [SerializeField] GameObject player;
  // Start is called before the first frame update


  // Update is called once per frame
  void Update()
  {

    transform.position = updatedPosition;
  }
  public void SetUpdatedPosition(Vector3 newPos)
  {

    updatedPosition = new Vector3(newPos.x, transform.position.y, transform.position.z);
  }
}
