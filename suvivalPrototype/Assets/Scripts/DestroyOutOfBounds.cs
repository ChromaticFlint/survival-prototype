using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

  private float zBound = 10;
  private float xBound = 16;

  // Update is called once per frame
  void Update()
  {
    if (transform.position.z > zBound)
    {
      Destroy(gameObject);
    }
    else if (transform.position.z < -zBound)
    {
      Destroy(gameObject);
    }
    else if (transform.position.x > xBound)
    {
      Destroy(gameObject);
    }
    else if (transform.position.x < -xBound)
    {
      Destroy(gameObject);
    }
  }
}
