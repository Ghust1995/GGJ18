using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment_NPC : MonoBehaviour {

    GameObject Player;
    Rigidbody2D rb;
    public Vector2 Destination;
    private Mood currentMood;
    LayerMask mask = 0;

    public bool Angry = false;
    private bool isFinding = false;
    private MoodBehaviour[] col;
    public float velocityNPC = 0.012f;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        Destination = Random.insideUnitCircle * 5 + new Vector2(transform.position.x, transform.position.y);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

   


	// Update is called once per frame
	void Update () {

        currentMood = GetComponent<MoodBehaviour>().currentMood;


        transform.position = Vector2.MoveTowards(transform.position, Destination,velocityNPC);


        if (Vector2.Distance(Destination, transform.position) < 1 ) // && currentMood == Mood.Neutral)
        {
            SetNextDestination();
        }
        /*
        if (currentMood == Mood.Angry && !isFinding)
        {
            StartCoroutine(Find());
        }
        */



	}

    void SetNextDestination ()
    {
            Destination = Random.insideUnitCircle * 5;//+ new Vector2(transform.position.x, transform.position.y);
       
    }


    private IEnumerator Find ()
    {
        isFinding = true;
        yield return new WaitForSeconds(1);
        AngerTarget();
        Debug.Log("Quantas vezes ");
        isFinding = false;
    }




    void AngerTarget ()
    {
        if (Vector2.Distance(Player.transform.position,transform.position)< 3)
        {
            Destination = Player.transform.position;
        }
        else
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 2,mask.value);
            
           
            if ( col.Length != 1)
            {
                for (int i = 0; i < col.Length;i++)
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


