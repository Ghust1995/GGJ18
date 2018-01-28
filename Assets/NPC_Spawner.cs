using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour {

    public float MinTimeChange = 1, MaxTimeChange = 1;

    public float InitialTimeForSpawnNpc = 30f;
    public GameObject NPC;

    public List<Transform> spawnPoints;

    public int NPCCount;
    public int MaxNpcCount = 30;

	// Use this for initialization
	void Start () {
        StartCoroutine(KeepSpawningNPCCoroutine(InitialTimeForSpawnNpc));
	}


    IEnumerator KeepSpawningNPCCoroutine(float timeToSpawn)
    {
        float wait = Random.Range(timeToSpawn * MinTimeChange, timeToSpawn * MaxTimeChange);
        yield return new WaitForSeconds(wait);

        if (NPCCount < MaxNpcCount)
        {
            Debug.Log("New npc");
            var pos = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Instantiate(NPC, pos, Quaternion.identity);
            NPCCount++;
        }
     
        yield return KeepSpawningNPCCoroutine(timeToSpawn);
    }



}
