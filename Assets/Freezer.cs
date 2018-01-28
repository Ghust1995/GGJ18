using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{

  public IEnumerator FreezeCoroutine(float freezeTime)
  {
    Time.timeScale = 0.0f;
    float pauseEndTime = Time.realtimeSinceStartup + freezeTime;
    while (Time.realtimeSinceStartup < pauseEndTime)
    {
      yield return null;
    }
    Time.timeScale = 1;
  }

  public float defaultFreezeTime = 0.0f;
  public void Freeze(float freezeTime)
  {
    StartCoroutine(FreezeCoroutine(freezeTime));
  }
  public void Freeze()
  {
    StartCoroutine(FreezeCoroutine(defaultFreezeTime));
  }
}
