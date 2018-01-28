using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

	public MoodBehaviour EvilNpcPrefab;
	public void Die() {
		Instantiate(EvilNpcPrefab, transform.position, transform.rotation);
		this.gameObject.SetActive(false);
	}
}
