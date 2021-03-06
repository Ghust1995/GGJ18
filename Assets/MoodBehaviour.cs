﻿using System.Collections;
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
  public ParticleSystem HappyParticles;

  public Animator anim;
  public Mood currentMood;
  public bool isPlayer;

  public int NumNpcs = 11;

  // Use this for initialization
  void Start()
  {
    FindObjectOfType<NPCManager>().AllNPCs.Add(this);
    anim = GetComponent<Animator>();
    anim.SetInteger("NPC", Random.Range(1, NumNpcs + 1));
    anim.SetBool("IsPlayer", isPlayer);

    switch (currentMood)
    {
      case Mood.Neutral:
        {
          break;
        }
      case Mood.Angry:
        {
          anim.SetTrigger("ToDevil");
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
        Debug.Log(anim.GetCurrentAnimatorClipInfo(1));
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
    anim.SetTrigger("ToDevil");
    for (var time = 0.0f; time < AnimationTime; time += Time.deltaTime)
    {
      yield return null;
    }
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
    HappyParticles.Play();
    anim.SetTrigger("ToNormal");
    gameObject.GetComponent<NPCMovement>().SetNextDestination();
    for (var time = 0.0f; time < AnimationTime; time += Time.deltaTime)
    {
      yield return null;
    }

    //GetComponent<SpriteRenderer>().color = Color.green;
    yield return new WaitForSeconds(TimeToNeutral);
    HappyParticles.Stop();
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
      yield return null;
    }

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
          if (currentMood == Mood.Angry)
          {
            collision.gameObject.GetComponent<PlayerDeath>().Die();
          }
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
