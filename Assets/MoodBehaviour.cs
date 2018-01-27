using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mood
{
  Neutral = 1,
  Angry = 2,
  Happy = 3,
}

[RequireComponent(typeof(Collider2D))]
public class MoodBehaviour : MonoBehaviour
{

  public Mood currentMood;

  // Use this for initialization
  void Start()
  {

    switch (currentMood)
    {
      case Mood.Neutral:
        {
          GetComponent<SpriteRenderer>().color = Color.white;
          break;
        }
      case Mood.Angry:
        {
          GetComponent<SpriteRenderer>().color = Color.red;
          break;
        }
      case Mood.Happy:
        {
          GetComponent<SpriteRenderer>().color = Color.green;
          break;
        }
      default:
        {
          GetComponent<SpriteRenderer>().color = Color.magenta;
          break;
        }
    }

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void BecomeAngry()
  {
    StartCoroutine(BecomeAngryCoroutine());
  }

  public float AnimationTime = 0.5f;
  private IEnumerator BecomeAngryCoroutine()
  {
		currentMood = Mood.Angry;
        //MODIFICADO AQUI
        gameObject.layer = 8;
    var color = GetComponent<SpriteRenderer>().color;
    for (var time = 0.0f; time < AnimationTime; time += Time.deltaTime)
    {
      GetComponent<SpriteRenderer>().color = Color.Lerp(color, Color.red, time);
      yield return null;
    }

      GetComponent<SpriteRenderer>().color = Color.red;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    var tag = collision.gameObject.tag;
    switch (tag)
    {
      case "NPC":
        {
          var npcMood = collision.gameObject.GetComponent<MoodBehaviour>();
          if (currentMood == Mood.Angry)
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
