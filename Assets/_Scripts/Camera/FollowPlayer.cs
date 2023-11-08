using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  CinemachineVirtualCamera cinemachineVirtualCamera;
  private void Awake()
  {
    cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
  }
  public void CameraFollow()
  {
    cinemachineVirtualCamera.Follow = FindAnyObjectByType<Hero>().transform;
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    CameraFollow();
  }
}
