using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

  public Vector2 speed = new Vector2(10, 10);

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
    rb.MovePosition(rb.position + Time.deltaTime * Vector2.Scale(speed, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))));
  }
}
