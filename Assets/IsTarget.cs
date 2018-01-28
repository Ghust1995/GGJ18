using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTarget : MonoBehaviour {

  public  bool isTarget = false;
    public Mood currentMood; 
    public int seekers = 0;

    private void Update()
    {
        var c = GetComponent<MoodBehaviour>();

        if (c != null)
        {
            currentMood = c.currentMood;

            if (currentMood == Mood.Angry)
            {
                isTarget = false;
            }
        }

         
      


        
    }
}
