using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour {

    public float MinTimeChange = 1, MaxTimeCHange = 1;

    public float InitialTimeForSpawnNpc = 2f;
    public float TimeAdicition = 0.9f;
    public GameObject NPC;


    public int NPCCount;
    public int MaxNpcCount = 30;

	// Use this for initialization
	void Start () {
        KeepSpawningNPCCoroutine(InitialTimeForSpawnNpc);

	}


    IEnumerator KeepSpawningNPCCoroutine(float timeToSpawn)
    {
        float wait = Random.Range(timeToSpawn * MinTimeChange, timeToSpawn * MaxTimeCHange);
        yield return new WaitForSeconds(wait);

        if (NPCCount < MaxNpcCount)
        {
            Instantiate(NPC, transform.position, Quaternion.identity);
            NPCCount++;
        }
     
        yield return KeepSpawningNPCCoroutine(TimeAdicition * timeToSpawn);
    }



}
