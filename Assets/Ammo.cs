using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

  // Use this for initialization
  public float TargetScale;
  public List<Color> PossibleColors;

  private float radius;
  private Vector2 center;
  void Start()
  {
    GetComponent<SpriteRenderer>().color = PossibleColors[Random.Range(0, PossibleColors.Count)];
    transform.localScale = Vector3.zero;
    radius = GetComponent<CircleCollider2D>().radius;
		center = (Vector2) transform.position + GetComponent<CircleCollider2D>().offset;
    StartCoroutine(Spawn());
  }

  public bool IsAlive = true;
  public bool IsGrowing = true;
  public float SpawnTime = 0.3f;

  public AnimationCurve spawnAnimation;
  public IEnumerator Spawn()
  {
    var near = Physics2D.OverlapCircleAll(center, radius);
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
  public float MaxReplicationRadius = 6f;
  public IEnumerator Replicate()
  {
    yield return new WaitForSeconds(Random.Range(0.8f, 1.2f) * ReplicationTime);
    if (IsAlive)
    {
      Instantiate(gameObject, transform.position + (Vector3)Random.insideUnitCircle.normalized * Random.Range(radius, MaxReplicationRadius), transform.rotation);
      yield return Replicate();
    }
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
		Debug.Log("HIT");
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
          var gun = collision.gameObject.GetComponent<Gun>();
          gun.Flowers++;
          Destroy(this.gameObject);
          break;
        }
      default:
        {
          break;
        }
    }
  }
}
