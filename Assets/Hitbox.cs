using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D collision)
  {
    var tag = collision.gameObject.tag;
    switch (tag)
    {
      case "NPC":
        {
          var npcMood = collision.gameObject.GetComponent<MoodBehaviour>();
					npcMood.BecomeHappy();
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
