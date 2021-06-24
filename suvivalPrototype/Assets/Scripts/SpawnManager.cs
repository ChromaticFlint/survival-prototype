using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  // Gameplay Variables
  public int enemyCount;
  public int waveNumber = 1;
  public float spawnDelay = 4;
  public int enemiesToSpawn = 3;
  public float spawnMultiplier = 0.995f;
  public int currentSpawnPosition = 3;
  private Vector3[] spawnZones;
  private Vector3 spawnZoneZero = new Vector3(12.5f, 1.0f, 6.5f);
  private Vector3 spawnZoneOne = new Vector3(12.5f, 1.0f, -6.5f);
  private Vector3 spawnZoneTwo = new Vector3(-12.5f, 1.0f, -6.5f);
  private Vector3 spawnZoneThree = new Vector3(-12.5f, 1.0f, 6.5f);

  // State Controllers
  private bool readyToSpawn = true;

  // Componenets and Gameobjects
  public GameObject enemyPrefab;


  // Start is called before the first frame update
  void Start()
  {
    spawnZones = new Vector3[] { spawnZoneZero, spawnZoneOne, spawnZoneTwo, spawnZoneThree };
  }

  // Update is called once per frame
  void Update()
  {
    if (readyToSpawn)
    {
      StartCoroutine(SpawnTimer());
      readyToSpawn = false;
    }
  }

  private void SpawnEnemyWave()
  {

    Vector3 spawnPos = SwitchSpawnPosition();
    Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
  }

  private Vector3 SwitchSpawnPosition()
  {
    if (currentSpawnPosition < 3)
    {
      currentSpawnPosition++;
    }
    else
    {
      currentSpawnPosition = 0;
    }

    return spawnZones[currentSpawnPosition];
  }

  IEnumerator SpawnTimer()
  {
    spawnDelay = spawnDelay * spawnMultiplier;
    yield return new WaitForSeconds(spawnDelay);
    waveNumber++;
    SpawnEnemyWave();
    readyToSpawn = true;
  }
}
