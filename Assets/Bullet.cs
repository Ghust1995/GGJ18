using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
  public float speed;
  public float range;
  private float walkedDistance;
  public Vector2 direction;
  public Rigidbody2D rb;

  public Color MinShadowColor;
  private Color baseColor;
  private Vector3 baseScale;

  public float FinalScale;
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    baseColor = GetComponent<SpriteRenderer>().color;
    baseScale = transform.localScale;
    GetComponent<SpriteRenderer>().color = Color.Lerp(baseColor, MinShadowColor, shadowFalloff.Evaluate(0));
  }

  public AnimationCurve shadowFalloff;
  private bool isStopped = false;
  void Update()
  {
    if (!isStopped)
    {
      transform.position += speed * (Vector3)direction * Time.deltaTime;
    }
    walkedDistance += speed * Time.deltaTime;
    Debug.Log(walkedDistance);
    GetComponent<SpriteRenderer>().color = Color.Lerp(baseColor, MinShadowColor, shadowFalloff.Evaluate(walkedDistance / range));
    transform.localScale = baseScale * Mathf.Lerp(baseScale.x, FinalScale * baseScale.x, 1.0f - shadowFalloff.Evaluate(walkedDistance / range));
    if (walkedDistance > range)
    {
      StartCoroutine(KeepCorpse());
    }
  }

  public float KeepAliveTime = 10;

  private IEnumerator KeepCorpse()
  {
		isStopped = true;
    Destroy(GetComponent<Collider>());
    yield return new WaitForSeconds(KeepAliveTime);
    Destroy(this.gameObject);
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    var tag = collision.gameObject.tag;
    switch (tag)
    {
      case "NPC":
        {
          var npcMood = collision.gameObject.GetComponent<MoodBehaviour>();
          npcMood.BecomeHappy();
          StartCoroutine(KeepCorpse());
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
