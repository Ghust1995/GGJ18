using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

	public float MaxAngle;
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
    var position = 2 * ((Vector2)mainCamera.ScreenToViewportPoint(Input.mousePosition) - 0.5f * Vector2.one);
		position.y = Mathf.Min(Mathf.Tan(-Mathf.Deg2Rad * MaxAngle) * Mathf.Abs(position.x), position.y);
		position.y = Mathf.Max(Mathf.Tan(Mathf.Deg2Rad * MaxAngle) * Mathf.Abs(position.x), position.y);
    transform.position = position;
  }

}
