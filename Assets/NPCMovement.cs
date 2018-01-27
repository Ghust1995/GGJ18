using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

  GameObject Player;
  public Vector2 Destination;
  LayerMask mask = 0;

  public bool Angry = false;
  private MoodBehaviour[] col;
  public Vector2 VelocityNPC = new Vector2(1, 0.5f);
  private Rect roamingArea;


  // Use this for initialization
  void Start()
  {
    Destination = Random.insideUnitCircle * 5 + new Vector2(transform.position.x, transform.position.y);
    Player = GameObject.FindGameObjectWithTag("Player");
    roamingArea = FindObjectOfType<WorldData>().Bounds;
    SetNextDestination();
  }

  // Update is called once per frame
  void Update()
  {
    var direction = -((Vector2)transform.position - Destination).normalized;
    transform.position += (Vector3) (Vector2.Scale(direction, VelocityNPC) * Time.deltaTime);

    if (Vector2.Distance(Destination, transform.position) < 0.5) // && currentMood == Mood.Neutral)
    {
      SetNextDestination();
    }
  }

  void OnDrawGizmos() {
      Gizmos.color = Color.white;
      Gizmos.DrawLine(transform.position, Destination);
  }

  void SetNextDestination()
  {
    Destination = RandomPointInSquare(roamingArea);
  }

  Vector2 RandomPointInSquare(Rect r){
        return r.position + new Vector2(
                (Random.value - 0.5f) * r.size.x,
                (Random.value - 0.5f) * r.size.y);
  }

  

  private IEnumerator Find()
  {
    yield return new WaitForSeconds(1);
    AngerTarget();
    Debug.Log("Quantas vezes ");
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


