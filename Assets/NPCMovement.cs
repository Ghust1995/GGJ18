﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

  GameObject Player;
  public Vector2 Destination;

  public Transform Target;
  private Mood currentMood;
  private SpriteRenderer sprite;
  LayerMask mask = 0;

  // bool tempDestiny = false;
  public bool HaveTarget = false;
  private Collider[] col;

  public Vector2 VelocityNPC = new Vector2(1, 0.5f);
  private Rect roamingArea;
  private Rect friendlyArea;


  // Use this for initialization
  void Start()
  {
    Player = GameObject.FindGameObjectWithTag("Player");
    roamingArea = FindObjectOfType<WorldData>().Bounds;
    friendlyArea = FindObjectOfType<WorldData>().extendedBounds;
    sprite = GetComponent<SpriteRenderer>();
    if (!HaveTarget && Vector2.Distance(Destination,Vector2.zero) < 0.01)
    {
      SetNextDestination();
    }
  }

  // Update is called once per frame
  void Update()
  {
    currentMood = GetComponent<MoodBehaviour>().currentMood;

    var direction = -((Vector2)transform.position - Destination).normalized;
    transform.position += (Vector3)(Vector2.Scale(direction, VelocityNPC) * Time.deltaTime);
    sprite.flipX = direction.x < 0;

    if (currentMood == Mood.Neutral && Vector2.Distance(Destination, transform.position) < ReachDistance)
    {
      var inside = Mathf.Abs(transform.position.x - roamingArea.position.x) < roamingArea.size.x / 2 && Mathf.Abs(transform.position.y - roamingArea.position.y) < roamingArea.size.y / 2;
      if (inside)
      {
        wantExit = true;
        SetNextDestination();
      }
      else
      {
        Destroy(gameObject);
        FindObjectOfType<NPC_Spawner>().NPCCount--;
      }
    }
    if (currentMood == Mood.Angry && HaveTarget == false)
    {
      SearchTarget();
    }
    else if (currentMood == Mood.Angry && HaveTarget == true)
    {
      AttackTarget();
    }
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.white;
    Gizmos.DrawLine(transform.position, Destination);
    if (currentMood != Mood.Angry) return;
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, SearchRadius);
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, GiveUpDistance);

  }
  public bool wantExit = false;
  public void SetNextDestination()
  {
    if (wantExit && currentMood == Mood.Neutral)
    {
      Destination = RandomPointInSquare(friendlyArea);
    }
    else
    {
      Destination = RandomPointInSquare(roamingArea);
    }
  }

  Vector2 RandomPointInSquare(Rect r)
  {
    return r.position + new Vector2(
            (Random.value - 0.5f) * r.size.x,
            (Random.value - 0.5f) * r.size.y);
  }



  public float SearchRadius = 3;
  public float ReachDistance = 3;
  private void SearchTarget()
  {
    Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, SearchRadius);

    bool didSomething = false;
    for (int i = 0; i < col.Length; i++)
    {
      if (col[i].gameObject == gameObject)
      {
        continue;
      }

      if (col[i].tag == "Player" && col[i].GetComponent<IsTarget>().isTarget == false)
      {
        col[i].GetComponent<IsTarget>().isTarget = true;
        Destination = col[i].transform.position;
        Target = col[i].transform;
        HaveTarget = true;
        didSomething = true;
        break;
      }

      if (col[i].tag == "NPC" && col[i].GetComponent<MoodBehaviour>().currentMood == Mood.Neutral
          && col[i].GetComponent<IsTarget>().isTarget == false)
      {
        col[i].GetComponent<IsTarget>().isTarget = true;
        Destination = col[i].transform.position;
        Target = col[i].transform;
        HaveTarget = true;
        didSomething = true;
        break;
      }
    }

    if (!didSomething && Vector2.Distance(transform.position, Destination) < ReachDistance)
    {
      var inside = Mathf.Abs(transform.position.x - roamingArea.position.x) < roamingArea.size.x / 2 && Mathf.Abs(transform.position.y - roamingArea.position.y) < roamingArea.size.y / 2;
      if (inside)
      {
        wantExit = true;
        SetNextDestination();
      }
      else
      {
        Destroy(gameObject);
        FindObjectOfType<NPC_Spawner>().NPCCount--;
      }
    }
  }

  public float GiveUpDistance = 4;
  private void AttackTarget()
  {
    if (Target != null)
    {
      if (Target.tag == "NPC")
      {
        if (Target.GetComponent<MoodBehaviour>().currentMood != Mood.Neutral)
        {
          HaveTarget = false;
          Target = null;
        }
        else
        {
          Destination = Target.transform.position;
        }
      }
      else if (Target.tag == "Player")
      {
        if (Vector2.Distance(transform.position, Target.position) < GiveUpDistance)
        {
          Destination = Target.position;
        }
        else
        {
          HaveTarget = false;
          Target = null;
        }
      }
    }
    else
    {
      HaveTarget = false;
    }
  }
}


