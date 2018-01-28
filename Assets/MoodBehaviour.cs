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
    FindObjectOfType<NPCManager>().AllNPCs.Add(this);

    switch (currentMood)
    {
      case Mood.Neutral:
        {
          GetComponent<SpriteRenderer>().color = Color.white;
          break;
        }
      case Mood.Angry:
        {
          GetComponent<SpriteRenderer>().color = Color.black;
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
    var color = GetComponent<SpriteRenderer>().color;
    for (var time = 0.0f; time < AnimationTime; time += Time.deltaTime)
    {
      GetComponent<SpriteRenderer>().color = Color.Lerp(color, Color.black, time);
      yield return null;
    }

    GetComponent<SpriteRenderer>().color = Color.black;
  }

  public void BecomeHappy()
  {
    StartCoroutine(BecomeHappyCoroutine());
  }
public float TimeToNeutral = 2;
  private IEnumerator BecomeHappyCoroutine()
  {
    currentMood = Mood.Happy;
    var color = GetComponent<SpriteRenderer>().color;
    for (var time = 0.0f; time < AnimationTime; time += Time.deltaTime)
    {
      GetComponent<SpriteRenderer>().color = Color.Lerp(color, Color.green, time);
      yield return null;
    }

    GetComponent<SpriteRenderer>().color = Color.green;
    yield return new WaitForSeconds(TimeToNeutral);
    yield return BecomeNeutralCoroutine();
  }
  public void BecomeNeutral()
  {
    StartCoroutine(BecomeNeutralCoroutine());
  }

  private IEnumerator BecomeNeutralCoroutine()
  {
    currentMood = Mood.Neutral;
    var color = GetComponent<SpriteRenderer>().color;
    for (var time = 0.0f; time < AnimationTime; time += Time.deltaTime)
    {
      GetComponent<SpriteRenderer>().color = Color.Lerp(color, Color.white, time);
      yield return null;
    }

    GetComponent<SpriteRenderer>().color = Color.white;
  }
  void OnCollisionEnter2D(Collision2D collision)
  {
    var tag = collision.gameObject.tag;
    switch (tag)
    {
      case "NPC":
        {
          var npcMood = collision.gameObject.GetComponent<MoodBehaviour>();
          if (currentMood == Mood.Angry && npcMood.currentMood == Mood.Neutral)
          {
            npcMood.BecomeAngry();
          }
          if (currentMood == Mood.Happy)
          {
            npcMood.BecomeHappy();
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
