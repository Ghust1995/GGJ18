﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  public Bullet bulletPrefab;

  public Animator anim;

  public ParticleSystem ps;

  public int flowersToAmmo = 5;
  public int ammo = 0;
  public int MaxAmmo = 4;
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
        ammo = Mathf.Clamp(ammo, 0, 4);
      }
    }
  }

  public Transform spawnPoint;
  public void Shoot(Vector2 position, Vector2 direction)
  {
    if (ammo > 0)
    {
      var bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)));
      bullet.direction = direction.normalized;
      FindObjectOfType<BoundCamera>().ScreenShake();
      anim.SetTrigger("Shoot");
      ps.Play();
      GetComponent<AudioController>().PlaySound();
      if(!HasInfiniteAmmo) {      
        ammo--;
        ammo = Mathf.Clamp(ammo, 0, 4);
      }
    }
  }
}
