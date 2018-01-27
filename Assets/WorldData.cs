using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour
{
  public Rect Bounds;
  public void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireCube(Bounds.position, Bounds.size); 
  }
}
