using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour
{
  public Rect Bounds;
    public Rect extendedBounds;
    public void OnDrawGizmosSelected()
  {
        Gizmos.color = Color.red;
    Gizmos.DrawWireCube(Bounds.position, Bounds.size);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(extendedBounds.position, extendedBounds.size);
    }
}
