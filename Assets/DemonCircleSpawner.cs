using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCircleSpawner : MonoBehaviour
{


  public DemonCircle demonCirclePrefab;
  private Rect possibleArea;

  public float MinTimeChange = 1, MaxTimeCHange = 1;
  public int MaxToSpawn = 3;

  public float InitialTimeToSpawn = 3;
  public float TimeToSpawnDecrement = 0.9f;

  // Use this for initialization
  void Start()
  {
    possibleArea = FindObjectOfType<WorldData>().Bounds;
    StartCoroutine(KeepSpawningCoroutine(InitialTimeToSpawn));
  }


	public float chanceToSpawnNearby = 0.5f;
	public float DelayFix = 1.1f;
  IEnumerator KeepSpawningCoroutine(float timeToSpawn)
  {
    float wait = Random.Range(timeToSpawn * MinTimeChange, timeToSpawn * MaxTimeCHange);
    Debug.Log(wait);
    yield return new WaitForSeconds(wait);
    int numToSpawn = (int)Mathf.Floor(Mathf.Sqrt(Random.Range(1, (MaxToSpawn + 1) * (MaxToSpawn + 1))));
		bool spawnedNearby = false;
    for (int i = 0; i < numToSpawn; i++)
    {
      var pos = RandomPointInSquare(possibleArea);
			if(!spawnedNearby && Random.value < chanceToSpawnNearby) {
				var p = FindObjectOfType<PlayerMover>();
				if(p != null) {
					pos = (Vector2)p.transform.position + p.deltaMove / Time.deltaTime * demonCirclePrefab.Delay * DelayFix;
					spawnedNearby = true;
				}
			}
      Instantiate(demonCirclePrefab, pos, Quaternion.identity);
    }
    yield return KeepSpawningCoroutine(TimeToSpawnDecrement * timeToSpawn);
  }

  Vector2 RandomPointInSquare(Rect r)
  {
    return r.position + new Vector2(
            (Random.value - 0.5f) * r.size.x,
            (Random.value - 0.5f) * r.size.y);
  }

}
