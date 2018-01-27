using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

  Vector2 speedHorizontal = new Vector2(2, 0);
  Vector2 speedVertical = new Vector2(0, 1);

  Vector2 speedDiagonalUR = new Vector2(1, 1);
  Vector2 speedDiagonalUL = new Vector2(-1, 1);
  Vector2 speedDiagonalDR = new Vector2(1, -1);
  Vector2 speedDiagonalDL = new Vector2(-1, -1);


  int speed = 2;


  Rigidbody2D rb;



  // Use this for initialization
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    //Movimentacoes singulares 
    if (Input.GetKey(KeyCode.A))
    {
      rb.MovePosition(rb.position - speedHorizontal * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.D))
    {
      rb.MovePosition(rb.position + speedHorizontal * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.W))
    {
      rb.MovePosition(rb.position + speedVertical * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.S))
    {
      rb.MovePosition(rb.position - speedVertical * Time.deltaTime);
    }

    //Diagonais 
    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
    {
      rb.MovePosition(rb.position + speedDiagonalUR * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
    {
      rb.MovePosition(rb.position + speedDiagonalUL * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
    {
      rb.MovePosition(rb.position + speedDiagonalDL * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
    {
      rb.MovePosition(rb.position + speedDiagonalDR * Time.deltaTime);
    }
  }
}
