using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour
{
  public Vector2 RoamingArea;
  public void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireCube(Vector3.zero, new Vector3(RoamingArea.x, RoamingArea.y, 2));
  }
}
