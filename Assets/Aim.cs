using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

	public float MaxAngle;
  public Transform reference;
  private Camera _mainCamera;
  private Camera mainCamera
  {
    get
    {
      if (_mainCamera == null)
      {
        _mainCamera = Camera.main;
      }
      return _mainCamera;

    }
  }
  // Use this for initialization
  void Start()
  {
		Cursor.visible = false;

  }


  // Update is called once per frame
  void Update()
  {
    var position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    position.z = 0;
		position.y = Mathf.Min(Mathf.Tan(Mathf.Deg2Rad * MaxAngle) * Mathf.Abs(position.x), position.y);
		position.y = Mathf.Max(Mathf.Tan(-Mathf.Deg2Rad * MaxAngle) * Mathf.Abs(position.x), position.y);
    transform.position = position;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    var position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		var v = position.x > 0 ? Vector3.right : Vector3.left;
    Gizmos.DrawRay(reference.position, Quaternion.Euler(0, 0, MaxAngle) * v);
    Gizmos.DrawRay(reference.position, Quaternion.Euler(0, 0, -MaxAngle) * v);

    Gizmos.color = Color.green;
    Gizmos.DrawRay(reference.position, (transform.position).normalized);
  }
}
