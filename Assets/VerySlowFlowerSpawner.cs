using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySlowFlowerSpawner : MonoBehaviour
{
  public float MinTimeChange = 1, MaxTimeChange = 1;

  public float SpawnTime = 2f;
  public GameObject flowerPrefab;

	public Rect possibleArea;

  // Use this for initialization
  void Start()
  {
    possibleArea = FindObjectOfType<WorldData>().Bounds;
    StartCoroutine(SpawnFlowerCoroutine(SpawnTime));
  }
  Vector2 RandomPointInSquare(Rect r)
  {
    return r.position + new Vector2(
            (Random.value - 0.5f) * r.size.x,
            (Random.value - 0.5f) * r.size.y);
  }

  IEnumerator SpawnFlowerCoroutine(float timeToSpawn)
  {
    float wait = Random.Range(timeToSpawn * MinTimeChange, timeToSpawn * MaxTimeChange);
    yield return new WaitForSeconds(wait);
    var pos = RandomPointInSquare(possibleArea);
    Instantiate(flowerPrefab, pos, Quaternion.identity);
    yield return SpawnFlowerCoroutine(timeToSpawn);
  }


}
