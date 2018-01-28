using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  public Bullet bulletPrefab;

  public int flowersToAmmo = 5;
  public int ammo = 0;
  public bool HasInfiniteAmmo = false;

  private int _flowers = 0;
  public int Flowers
  {
    get
    {
      return _flowers;
    }
    set
    {
      _flowers = value;
      while (_flowers > flowersToAmmo)
      {
        _flowers -= flowersToAmmo;
        ammo++;
      }
    }
  }

  public void Shoot(Vector2 position, Vector2 direction)
  {
    if (ammo > 0)
    {
      var bullet = Instantiate(bulletPrefab, position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)));
      bullet.direction = direction.normalized;
      FindObjectOfType<BoundCamera>().ScreenShake();
      if(!HasInfiniteAmmo) {      
        ammo--;
      }
    }
  }
}
