using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

  GameObject Player;
  Rigidbody2D rb;
  public Vector2 Destination;
  private Mood currentMood;
  LayerMask mask = 0;

  public bool Angry = false;
  private bool isFinding = false;
  private MoodBehaviour[] col;
  public float VelocityNPC = 0.012f;
  private Vector2 roamingArea;


  // Use this for initialization
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    Destination = Random.insideUnitCircle * 5 + new Vector2(transform.position.x, transform.position.y);
    Player = GameObject.FindGameObjectWithTag("Player");
    roamingArea = FindObjectOfType<WorldData>().RoamingArea;
    SetNextDestination();
  }

  // Update is called once per frame
  void Update()
  {
    currentMood = GetComponent<MoodBehaviour>().currentMood;

    transform.position = Vector2.MoveTowards(transform.position, Destination, VelocityNPC);

    if (Vector2.Distance(Destination, transform.position) < 0.01) // && currentMood == Mood.Neutral)
    {
      SetNextDestination();
    }
  }

  void SetNextDestination()
  {
    Destination = RandomPointInSquare(Vector2.zero, roamingArea);
  }

  Vector2 RandomPointInSquare(Vector2 center, Vector2 size){
        return center + new Vector2(
                (Random.value - 0.5f) * size.x,
                (Random.value - 0.5f) * size.y);
  }

  

  private IEnumerator Find()
  {
    isFinding = true;
    yield return new WaitForSeconds(1);
    AngerTarget();
    Debug.Log("Quantas vezes ");
    isFinding = false;
  }

  void AngerTarget()
  {
    if (Vector2.Distance(Player.transform.position, transform.position) < 3)
    {
      Destination = Player.transform.position;
    }
    else
    {
      Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 2, mask.value);

      if (col.Length != 1)
      {
        for (int i = 0; i < col.Length; i++)
        {
          if (col[i].gameObject == gameObject)
          {
            continue;
          }

          if (col[i].tag == "NPC" && col[i].GetComponent<MoodBehaviour>().currentMood == Mood.Neutral)
          {
            Destination = col[i].transform.position;
            break;
          }

        }
      }
    }
  }
}


