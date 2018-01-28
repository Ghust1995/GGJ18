using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoundCamera : MonoBehaviour
{

  public Vector2 min;
  public Vector2 max;

  public Transform pusher;

  Vector2 push;

  public Vector2 Offset;
  [Range(0, 1)]
  public float pushP;

  Vector2 map = new Vector2(3512 / 4, 1436 / 4);

  // Use this for initialization
  void Initialize()
  {

    var vertExten = Camera.main.orthographicSize;
    var horiExten = vertExten * Screen.width / Screen.height;

    max.x = map.x / 2 - horiExten;
    max.y = map.y / 2 - vertExten;

    min.x = horiExten - map.x / 2;
    min.y = vertExten - map.y / 2;

    push.y = pushP * vertExten;
    push.x = pushP * horiExten;

    //print(vertExten + "    " + horiExten);
    // print(min.x + " MIN x " + min.y + " MIN Y " + max.x + " Max x " + max.y + " max.y ");
  }


  private void Start()
  {
    Initialize();
  }

  private void Awake()
  {
    Initialize();
  }



  // Update is called once per frame
  void LateUpdate()
  {

    Initialize();
    var cameraP = transform.position;

    var pushed = pusher.position - (transform.position + (Vector3)Offset);
    var pushD = new Vector2(Mathf.Abs(pushed.x), Mathf.Abs(pushed.y));
    var pushS = new Vector2(Mathf.Sign(pushed.x), Mathf.Sign(pushed.y));
    
    if (pushD.x > push.x / 2)
    {
      cameraP.x += pushed.x - pushS.x * push.x/2;
    }
    if (pushD.y > push.y / 2)
    {
      cameraP.y += pushed.y - pushS.y * push.y/2;
    }


    cameraP.x = Mathf.Clamp(cameraP.x, min.x, max.x);
    cameraP.y = Mathf.Clamp(cameraP.y, min.y, max.y);
    transform.position = cameraP;

  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(Vector2.zero, new Vector2(map.x, map.y));
    Gizmos.color = Color.blue;
    Gizmos.DrawWireCube(Vector2.zero + Offset, new Vector2(max.x - min.x, max.y - min.y));
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(transform.position + (Vector3)Offset, new Vector2(push.x, push.y));
    Gizmos.color = Color.magenta;
    Gizmos.DrawSphere(transform.position + (Vector3)Offset, 10);
  }


}
