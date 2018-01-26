using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
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

	public event EventHandler StateChanged;
}
