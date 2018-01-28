using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

  GameObject Player;
  Rigidbody2D rb;
  public Vector2 Destination;
    private Transform Target;
  private Mood currentMood;
  LayerMask mask = 0;

   // bool tempDestiny = false;
    public bool Angry = false;
  private bool HaveTarget = false;
  private Collider [] col;
  public Vector2 VelocityNPC = new Vector2(1, 0.5f);
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

        var direction = -((Vector2)transform.position - Destination).normalized;
        transform.position += (Vector3) (Vector2.Scale(direction, VelocityNPC) * Time.deltaTime);

        if (  currentMood == Mood.Neutral && Vector2.Distance(Destination, transform.position) < 0.1)
        {
          SetNextDestination();
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

  void OnDrawGizmos() {
      Gizmos.color = Color.white;
      Gizmos.DrawLine(transform.position, Destination);
       
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



  private void SearchTarget()
  {
        Collider2D [] col = Physics2D.OverlapCircleAll(transform.position, 3 );

            for (int i =0; i<col.Length ; i++)
            {


                if (col[i].gameObject == gameObject)
                {
                        continue;
                }

                if (col[i].tag == "Player"  && col[i].GetComponent<IsTarget>().isTarget == false)
                {
                        col[i].GetComponent<IsTarget>().isTarget = true;
                        Destination = col[i].transform.position;
                        Target = col[i].transform;
                        HaveTarget = true;
                        break;
                }

                if (col[i].tag == "NPC" && col[i].GetComponent<MoodBehaviour>().currentMood == Mood.Neutral
                    && col[i].GetComponent<IsTarget>().isTarget == false)
                    {
                        col[i].GetComponent<IsTarget>().isTarget = true;
                        Destination = col[i].transform.position;
                        Target = col[i].transform;
                        HaveTarget = true;
                        break;

                    }

                if (col[i].tag == "NPC" && col[i].GetComponent<MoodBehaviour>().currentMood == Mood.Neutral
                && col[i].GetComponent<IsTarget>().isTarget == true)
                {
                    continue;

                }

                if (i == col.Length - 1)
                {
                    if (Vector2.Distance(transform.position, Destination) < 2)
                    {
                        SetNextDestination();
                    }
                    break;
                }

                if (col[i].tag == "NPC" && col[i].GetComponent<MoodBehaviour>().currentMood == Mood.Angry)
                        {
                            continue;
                        }

                
            }
        
        

            
        
       
  }

    private void AttackTarget ()
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
                if (Vector2.Distance(transform.position, Target.position) < 4)
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
        
       

    }




}


