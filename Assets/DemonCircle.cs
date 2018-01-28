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
  }

  public AnimationCurve fadeIn;

	void Update() {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("PentagramFire")) {
			c.enabled = true;
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("PentagramStartRev")) {
			c.enabled = false;
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("End")) {
			Destroy(this.gameObject);
		}
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
          collision.gameObject.GetComponent<PlayerDeath>().Die();
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
