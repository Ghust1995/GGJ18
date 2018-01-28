using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SadnessFXController : MonoBehaviour
{
	[Range(0, 1)]
  public float intensity;
  public float value;
  public float speed;
  private Material material;

  // Creates a private material used to the effect
  void Awake()
  {
    material = new Material(Shader.Find("Hidden/SadnessFX"));
  }

  // Postprocess the image
  void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    material.SetFloat("_Blend", intensity);
    material.SetFloat("_Speed", speed);
    material.SetFloat("_Value", value);
    Graphics.Blit(source, destination, material);
  }
}