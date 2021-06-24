using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  // Gameplay Variables
  public float baseSpeed = 4.0f;
  public float currentSpeed;
  public float speedScale = 1.0f;
  public float turnSpeed = 20.0f;
  public int minHealth = 1;
  public int maxHealth = 3;
  public int currentHealth;
  private int powerupCount;

  // State Management

  // Components and Gameobjects
  public GameObject[] powerUpArray;
  private Rigidbody enemyRb;
  private GameObject player;
  private GameObject powerup;

  // Start is called before the first frame update
  void Start()
  {
    enemyRb = GetComponent<Rigidbody>();
    player = GameObject.Find("Player");
    currentHealth = minHealth;
    currentSpeed = baseSpeed;
    powerupCount = powerUpArray.Length;
  }

  void Awake()
  {
    InvokeRepeating("scaleSpeed", speedScale, speedScale);
  }

  // Update is called once per frame
  void Update()
  {
    transform.LookAt(player.transform.position);
    transform.position += (transform.forward * currentSpeed * Time.deltaTime);
    if (currentHealth == 0)
    {
      Destroy(gameObject);
      powerup = GetRandomPowerup();
      Instantiate(powerup, transform.position, powerup.transform.rotation);
    }
  }

  void scaleSpeed()
  {
    currentSpeed = currentSpeed * 1.01f;
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Projectile"))
    {
      currentHealth--;
      Destroy(other.gameObject);
    }
  }

  public GameObject GetRandomPowerup()
  {
    int randomPowerup = Random.Range(0, powerupCount);
    Debug.Log(randomPowerup);

    return powerUpArray[randomPowerup];
  }
}
