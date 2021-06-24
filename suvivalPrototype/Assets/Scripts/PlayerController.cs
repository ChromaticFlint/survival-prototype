using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  // Gameplay Variables
  public float speed = 5.0f;
  public float dashDistance = 3.0f;
  public float dashCooldownTime = 1.0f;
  public float powerupDuration = 5.0f;
  public float projectileCooldown = 1.0f;

  // State Managers
  private bool hasDashed;
  public bool hasPowerup;
  private bool hasFired;
  public int enemyCount;

  // Map Boundaries
  private float maxHeight = 9.0f;
  private float maxWidth = 15.0f;

  // Movement Variables
  private float VerticalInput;
  private float HorizontalInput;
  private Vector3 lastMoveDirection;

  // Components and GameObjects
  private Rigidbody playerRb;
  public GameObject projectilePrefab;

  // Start is called before the first frame update
  void Start()
  {
    playerRb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    HandleMovement();
    PlayerDash();
    HandleProjectile();
    enemyCount = FindObjectsOfType<EnemyController>().Length;
  }

  void HandleMovement()
  {
    // Player Input and movement
    HorizontalInput = Input.GetAxis("Horizontal");
    VerticalInput = Input.GetAxis("Vertical");
    transform.Translate(Vector3.right * HorizontalInput * Time.deltaTime * speed);
    transform.Translate(Vector3.forward * VerticalInput * Time.deltaTime * speed);

    // Move direction
    lastMoveDirection = new Vector3(HorizontalInput, 0, VerticalInput).normalized;

    if (transform.position.x < (-maxWidth))
    {
      transform.position = new Vector3((-maxWidth), transform.position.y, transform.position.z);
    }

    if (transform.position.x > maxWidth)
    {
      transform.position = new Vector3(maxWidth, transform.position.y, transform.position.z);
    }

    // Keep player in bounds in the Z axis
    if (transform.position.z < (-maxHeight + 1))
    {
      transform.position = new Vector3(transform.position.x, transform.position.y, (-maxHeight + 1));
    }

    if (transform.position.z > maxHeight)
    {
      transform.position = new Vector3(transform.position.x, transform.position.y, maxHeight);
    }
  }

  void HandleProjectile()
  {
    if (!hasFired && enemyCount >= 1)
    {
      Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
      hasFired = true;
      StartCoroutine(FireCooldown());
    }

  }

  void PlayerDash()
  {
    if (Input.GetKeyDown(KeyCode.Space) && !hasDashed)
    {
      transform.position += lastMoveDirection * dashDistance;
      hasDashed = true;
      StartCoroutine(DashCooldown());
    }
  }

  IEnumerator DashCooldown()
  {
    yield return new WaitForSeconds(dashCooldownTime);
    hasDashed = false;
  }

  IEnumerator PowerupCooldownRoutine()
  {
    yield return new WaitForSeconds(powerupDuration);
    hasPowerup = false;
  }

  IEnumerator FireCooldown()
  {
    yield return new WaitForSeconds(projectileCooldown);
    hasFired = false;
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Powerup"))
    {
      hasPowerup = true;
      Destroy(other.gameObject);
      StartCoroutine(PowerupCooldownRoutine());
    }
  }
}
