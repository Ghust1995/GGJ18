using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

  public Vector2 speed = new Vector2(10, 10);
  private Rect bounds;
  // Use this for initialization
  void Start()
  {
    bounds = FindObjectOfType<WorldData>().Bounds;
  }

  // Update is called once per frame
  void Update()
  {
    //Movimentacoes singulares 
    var initialPosition = transform.position;
    var nextPosition = transform.position;
    var deltaMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    nextPosition += (Vector3)(Time.deltaTime * Vector2.Scale(speed, deltaMove));
    var relativeToBounds = (transform.position - (Vector3)bounds.position);
    var relAbs = new Vector2(Mathf.Abs(relativeToBounds.x), Mathf.Abs(relativeToBounds.y));
    var relSign = new Vector2(Mathf.Sign(relativeToBounds.x), Mathf.Sign(relativeToBounds.y));
    if(relAbs.x > bounds.size.x/2 && relSign.x == deltaMove.x) {
      nextPosition.x = initialPosition.x;
    }
    if(relAbs.y > bounds.size.y/2 && relSign.y == deltaMove.y) {
      nextPosition.y = initialPosition.y; 
    }
    transform.position = nextPosition;
  }
}
