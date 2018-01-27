using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public Gun weapon;
	public Melee melee;
	public Aim aim;
	private Camera _mainCamera;
	private Camera mainCamera {
		get {
			if(_mainCamera == null) {
				_mainCamera = Camera.main;
			}
			return _mainCamera;

		}
	}
	void Start () {
		weapon = GetComponent<Gun>();
		melee = GetComponent<Melee>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			weapon.Shoot(transform.position, (Vector2) (aim.transform.position));
		}
		if(Input.GetMouseButtonDown(1)) {
			melee.Shoot(transform.position, (Vector2) (aim.transform.position));
		}
	}

}
