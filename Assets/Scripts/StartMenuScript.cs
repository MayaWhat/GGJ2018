using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		var inputRestartGame = Input.GetAxis("RestartGame");

		if(inputRestartGame > 0) {
			var currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene("level1");
		}
	}
}
