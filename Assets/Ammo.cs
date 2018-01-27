using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

  // Use this for initialization
  public float TargetScale;
  public List<Color> PossibleColors;

  public float Radius;
  void Start()
  {
    GetComponent<SpriteRenderer>().color = PossibleColors[Random.Range(0, PossibleColors.Count)];
    transform.localScale = Vector3.zero;
    Radius = GetComponent<CircleCollider2D>().radius;
    StartCoroutine(Spawn());
  }

  public bool IsAlive = true;
  public bool IsGrowing = true;
  public float SpawnTime = 0.3f;

  public AnimationCurve spawnAnimation;
  public IEnumerator Spawn()
  {
    var near = Physics2D.OverlapCircleAll(transform.position, Radius);
    if (near.Length > 1)
    {
      Destroy(this.gameObject);
      yield break;
    }
    for (var time = 0.0f; time < SpawnTime; time += Time.deltaTime)
    {
      transform.localScale = Vector2.one * Mathf.Lerp(0, TargetScale, spawnAnimation.Evaluate(time / SpawnTime));
      yield return null;
    }
    transform.localScale = Vector2.one * TargetScale;
    IsGrowing = false;
    yield return Replicate();
  }

  public float ReplicationTime = 10.0f;
  public float ReplicationRadius = 0.1f;
  public IEnumerator Replicate()
  {
    yield return new WaitForSeconds(Random.Range(0.8f, 1.2f) * ReplicationTime);
    if (IsAlive)
    {
      Instantiate(gameObject, transform.position + (Vector3)Random.insideUnitCircle.normalized * Random.Range(2 * Radius, ReplicationRadius), Quaternion.identity);
      yield return Replicate();
    }
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    var tag = collision.gameObject.tag;
    switch (tag)
    {
      case "NPC":
        {
					if(collision.gameObject.GetComponent<MoodBehaviour>().currentMood == Mood.Angry) {
						Destroy(this.gameObject);
					}
          break;
        }
      case "Player":
        {
          Destroy(this.gameObject);
          var gun = collision.gameObject.GetComponent<Gun>();
          gun.Flowers++;
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
