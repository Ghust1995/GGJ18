using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

	public Aim aim;
	public Transform shoulder;
	public SpriteRenderer sprite;

	public Vector2 baseShoulderPosition;
	// Use this for initialization
	void Start() {
		aim = FindObjectOfType<Aim>();
		baseShoulderPosition = shoulder.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		shoulder.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aim.transform.position.y, aim.transform.position.x));
		sprite.flipY = aim.transform.position.x < 0;
		var spos = baseShoulderPosition; 
		spos.x = (aim.transform.position.x < 0 ? -1 : 1 ) * spos.x;
		shoulder.transform.localPosition = spos;

	}
}
