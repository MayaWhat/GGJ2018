using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMasterScript : MonoBehaviour {

	public static GameMasterScript TheMaster {
		get;
		private set;
	}

	public int LowerLevelBound;
	public int UpperLevelBound;
	public int LeftLevelBound;
	public int RightLevelBound;

	[SerializeField]
	int StartingState;

	[SerializeField]
	Color spookyColour = new Color(1f, 0.0f, 0.65f, 0.15f);

	[SerializeField]
	private int _currentState;
	public int CurrentState {
		get
		{
			return _currentState;
		}
		set
		{
			_currentState = value;
			if(value == 0) {
				StartCoroutine(Fade(spookyVision, spookyColour, new Color(1f, 1f, 1f, 0f)));
			}
			else {
				StartCoroutine(Fade(spookyVision, new Color(1f, 1f, 1f, 0f), spookyColour));
			}


			if(StateChanged != null) {
				StateChanged.Invoke(this, new EventArgs());
			}
		}
	}

	[SerializeField]
	Text diedText;

	[SerializeField]
	string firstScene;

	[SerializeField]
	SpriteRenderer spookyVision;

	public void WeDied() {
		diedText.enabled = true;
	}

	void Awake() {
		if(GameMasterScript.TheMaster != null) {
			Destroy(this);
			return;
		}

		GameMasterScript.TheMaster = this;
	}

	protected IEnumerator Fade(SpriteRenderer renderer, Color startColor, Color endColor) {
		for (float f = 0f; f < 1; f += 0.05f) {
			renderer.color = Color.Lerp(startColor, endColor, f);
			yield return null;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleRestart() {
		var inputRestart = Input.GetAxis("Restart");

		if(inputRestart > 0) {
			var currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(currentScene.name);
		}

		var inputRestartGame = Input.GetAxis("RestartGame");

		if(inputRestartGame > 0) {
			var currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(firstScene);
		}
	}

	void FixedUpdate() {
		HandleRestart();
	}

	public event EventHandler StateChanged;
}
