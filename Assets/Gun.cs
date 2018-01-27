using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public Bullet bulletPrefab;
	public void Shoot(Vector2 position, Vector2 direction) {
		var bullet = Instantiate(bulletPrefab, position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)));
		bullet.direction = direction.normalized;
	}
}
