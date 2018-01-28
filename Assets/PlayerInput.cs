using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
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
	void Awake () {
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

  void OnDrawGizmos()
  {
        /*
    Gizmos.color = Color.red;
    var position = 2 * ((Vector2)mainCamera.ScreenToViewportPoint(Input.mousePosition) - 0.5f * Vector2.one);
		var v = position.x < 0 ? Vector3.right : Vector3.left;
    Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -aim.MaxAngle) * v * 100);
    Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, aim.MaxAngle) * v * 100);

    Gizmos.color = Color.green;
    Gizmos.DrawRay(transform.position, (aim.transform.position).normalized * 100);
  */
    }
}
