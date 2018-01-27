using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public Vector2 speed = new Vector2(10, 10);
    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentacoes singulares 
        rb.MovePosition(rb.position + Time.deltaTime * Vector2.Scale(speed, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))));

        /*
          Camera.main.ScreenToWorldPoint(Input.mousePosition).x
    Input.GetAxisRaw("Horizontal")

    Input.GetAxisRaw("Horizontal") > 0 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x
         
         */

        if (Input.GetAxisRaw("Horizontal") != 0  || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("moving", true);
            //float onde = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;

                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x
                    && (Input.GetAxisRaw("Horizontal") > 0))
                {
                    anim.SetFloat("mouseP", 1);
                }

                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x
                    && (Input.GetAxisRaw("Horizontal") < 0))
                {
                    anim.SetFloat("mouseP", 1);
                }

                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x
                    && (Input.GetAxisRaw("Horizontal") > 0))
                {
                    anim.SetFloat("mouseP", -1);
                }

                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x
                    && (Input.GetAxisRaw("Horizontal") < 0))
                {
                    anim.SetFloat("mouseP", -1);
                }
        }
        else
        {
            anim.SetBool("moving", false);
        }
        
        



        
          if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x  > transform.position.x )
           {
               sprite.flipX = false;
           }
           else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x  < transform.position.x)
           {
               sprite.flipX = true;
           }
           

        // se ele solta ele ativa brevemente a animacao de idle
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("running", false);
        }

  }
}
