using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

  // Use this for initialization
  public float TargetScale;
  //public List<Color> PossibleColors;

  public WorldData worldData;

  public Animator animator;

  private float radius;
  private Vector2 center;

  public Sprite seed;

  void Awake()
  {
    this.First = this;

    GetComponent<SpriteRenderer>().sprite = seed;
  }
  void Start() {
    GetComponent<SpriteRenderer>().sprite = seed;
    worldData = FindObjectOfType<WorldData>();
    //GetComponent<SpriteRenderer>().enabled = false;
    radius = GetComponent<CircleCollider2D>().radius;
    center = (Vector2)transform.position + GetComponent<CircleCollider2D>().offset;
    animator = GetComponent<Animator>();
    StartCoroutine(Spawn());
  }

  public bool IsAlive = true;
  public int MaxFlowersInGroup = 20;
  public int FlowersInGroup = 0;
  public Ammo First;
  public bool isFirst = false;
  public bool IsGrowing = true;
  public float SpawnTime = 0.3f;

  public AnimationCurve spawnAnimation;
  public IEnumerator Spawn()
  {
    var near = Physics2D.OverlapCircleAll(center, radius);
    var inside = Mathf.Abs(transform.position.x - worldData.Bounds.position.x) < worldData.Bounds.size.x / 2 && Mathf.Abs(transform.position.y - worldData.Bounds.position.y) < worldData.Bounds.size.y / 2;
    if (near.Length > 1 || !inside)
    {
      Destroy(this.gameObject);
      yield break;
    }

    //GetComponent<SpriteRenderer>().enabled = true;
    animator.SetInteger("Flower", Random.Range(1, 7));
    //for (var time = 0.0f; time < SpawnTime; time += Time.deltaTime)
    //{
      //yield return null;
    //}
    //yield return animator.wa
    IsGrowing = false;
    this.First.FlowersInGroup++;
    if (this.First.FlowersInGroup < MaxFlowersInGroup)
    {
      yield return Replicate();
    }
  }

  public float ReplicationTime = 10.0f;
  public float MaxReplicationRadius = 6f;
  public IEnumerator Replicate()
  {
    yield return new WaitForSeconds(Random.Range(0.8f, 1.2f) * ReplicationTime);
    if (IsAlive)
    {
      var spawnPosition = transform.position + (Vector3)Random.insideUnitCircle.normalized * Random.Range(radius, MaxReplicationRadius);
      var x = worldData.Bounds.Contains((Vector2)spawnPosition);
      var inside = Mathf.Abs(spawnPosition.x - worldData.Bounds.position.x) < worldData.Bounds.size.x / 2 && Mathf.Abs(spawnPosition.y - worldData.Bounds.position.y) < worldData.Bounds.size.y / 2;
      if (inside)
      {
        var go = Instantiate(gameObject, spawnPosition, transform.rotation);
        go.GetComponent<Ammo>().isFirst = false;
        go.GetComponent<Ammo>().First = First;
      }
      if (this.First.FlowersInGroup < MaxFlowersInGroup)
      {
        yield return Replicate();
      }
    }
  }

  public void Update()
  {
    if (!isFirst && First != null)
    {
      this.FlowersInGroup = this.First.FlowersInGroup;
    }
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    var tag = collision.gameObject.tag;
    switch (tag)
    {
      case "NPC":
        {
          if (collision.gameObject.GetComponent<MoodBehaviour>().currentMood == Mood.Angry)
          {
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
