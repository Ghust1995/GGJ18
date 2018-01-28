using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

	// Use this for initialization

	public List<MoodBehaviour> AllNPCs;
	SadnessFXController sadnessFX;
	public float loseFrac;
	public float frac;
	public AnimationCurve curve;
	public float count;
	public float angryCount;
	void Awake() {
		sadnessFX = FindObjectOfType<SadnessFXController>();
	}
	
	// Update is called once per frame
	void Update () {
		count = AllNPCs.Count;
		angryCount = AllNPCs.FindAll((npc) => npc.currentMood == Mood.Angry).Count;
		frac = angryCount/count;
		sadnessFX.intensity = curve.Evaluate(frac/loseFrac);	
	}
}
