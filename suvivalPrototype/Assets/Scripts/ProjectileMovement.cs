using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
  // Gameplay Variables
  public float speed = 30.0f;
  private int enemyCount;
  private Vector3 closestEnemyDirection;

  // State Controllers

  // Components and Gameobjects
  private Rigidbody projectileRb;
  private GameObject player;

  // Start is called before the first frame update
  void Start()
  {
    projectileRb = GetComponent<Rigidbody>();
    player = GameObject.Find("Player");
  }

  // Update is called once per frame
  void Update()
  {
    enemyCount = FindObjectsOfType<EnemyController>().Length;
    if (enemyCount > 0)
    {
      FireProjectile();
    }
  }

  public GameObject FindClosestEnemy()
  {
    GameObject[] gameObjectsArray;
    gameObjectsArray = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject closest = null;
    float distance = Mathf.Infinity;
    Vector3 position = transform.position;
    foreach (GameObject gameObject in gameObjectsArray)
    {
      Vector3 diff = gameObject.transform.position - position;
      float curDistance = diff.sqrMagnitude;
      if (curDistance < distance)
      {
        closest = gameObject;
        distance = curDistance;
      }
    }
    return closest;
  }

  void FireProjectile()
  {
    Vector3 closestEnemyDirection = (FindClosestEnemy().transform.position - transform.position).normalized;
    projectileRb.AddForce(closestEnemyDirection * speed);
  }
}
