using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AmmoShower : MonoBehaviour
{
  public float Distance = 6;

	private Gun _gun;
	public Gun Gun {
		get {
			if(_gun == null) {
				_gun = FindObjectOfType<Gun>();
			}
			return _gun;
		}
	}
  public int Ammo
  {
    get
    {
      return Gun.ammo;
    }
  }

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    int distances = Ammo - 1;
    float lateralSize = distances * Distance;
    float hWidth = lateralSize / 2;
    int i = 0;
    foreach (Transform c in transform)
    {
			c.gameObject.SetActive(Ammo > i);
      var p = c.localPosition;
      p.x = -hWidth + i * Distance;
      c.localPosition = p;
      i++;
    }

  }
}
