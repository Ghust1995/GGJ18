using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToMenu : MonoBehaviour {

public bool CanClickToMenu = false;
	void Update () {
		if(!CanClickToMenu) return;
		if(Input.GetMouseButtonDown(0)) {
			SceneManager.LoadScene(0);
		}
	}
}
