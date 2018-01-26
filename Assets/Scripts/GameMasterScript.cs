using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour {

	public static GameMasterScript TheMaster {
		get;
		private set;
	}

	public int LowerLevelBound;

	[SerializeField]
	int StartingState;

	bool canToggle = true;

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
			if(StateChanged != null) {
				StateChanged.Invoke(this, new EventArgs());
			}
		}
	}

	void Awake() {
		if(GameMasterScript.TheMaster != null) {
			Destroy(this);
			return;
		}

		GameMasterScript.TheMaster = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleToggle() {
		var inputToggle = Input.GetAxis("ToggleState"); 

        if(!canToggle) {
            if(inputToggle == 0) {
                canToggle = true;
            }
            else {
                return;
            }
        }

        if (inputToggle != 0)
        {
            CurrentState = (CurrentState + 1) % 2;
			canToggle = false;
        }  
	}

	void HandleRestart() {
		var inputRestart = Input.GetAxis("Restart");

		if(inputRestart > 0) {
			var currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(currentScene.name);
		}
	}

	void FixedUpdate() {
		HandleToggle();
		HandleRestart();
	}

	public event EventHandler StateChanged;
}
