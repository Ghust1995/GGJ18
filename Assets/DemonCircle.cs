using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCircle : MonoBehaviour
{

  public float Delay = 1.0f;
  public float ActiveTime = 0.3f;
  public Animator anim;

  public Collider2D c;
  public SpriteRenderer s;

  // Use this for initialization
  void Start()
  {
    anim = GetComponent<Animator>();
    c = GetComponent<Collider2D>();
    s = GetComponent<SpriteRenderer>();
    c.enabled = false;
    StartCoroutine(DelayCoroutine());

  }

  public AnimationCurve fadeIn;

  IEnumerator DelayCoroutine()
  {
    for (float time = 0.0f; time < Delay; time += Time.deltaTime)
    {
      Color c = Color.white;
      c.a = time / Delay;
      s.color = c;
      yield return null;
    }
    s.color = Color.white;
    anim.SetTrigger("Trigger");
    c.enabled = true;
    yield return new WaitForSeconds(ActiveTime);
    Destroy(this.gameObject);
  }

  // Update is called once per frame
  void OnCollisionEnter2D(Collision2D collision)
  {
    var tag = collision.gameObject.tag;
    switch (tag)
    {
      case "NPC":
        {
          var npcMood = collision.gameObject.GetComponent<MoodBehaviour>();
          if (npcMood.currentMood == Mood.Neutral)
          {
            npcMood.BecomeAngry();
          }
          break;
        }
      case "Player":
        {
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
