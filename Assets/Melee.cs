using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

	public float AnimationTime;
	public float Radius;
	public float Angle;
	public Hitbox hitboxPrefab;

	public bool isShooting = false;
	public void Shoot(Vector2 position, Vector2 direction) {
		direction = direction.normalized;
		if(isShooting) return;
		var hitbox = Instantiate(hitboxPrefab, (Vector2) transform.position +  direction * Radius, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)));
		StartCoroutine(HitboxAnimation(hitbox, direction));
	}

	public IEnumerator HitboxAnimation(Hitbox hitbox, Vector2 direction) {
		isShooting = true;
		var rb = hitbox.GetComponent<Rigidbody2D>();
		for (var time = 0.0f; time < AnimationTime; time += Time.deltaTime) {
			rb.MovePosition((Vector2) transform.position +  direction * Radius);
			yield return null;
		}
		Destroy(hitbox.gameObject);
		isShooting = false;
	}
}


