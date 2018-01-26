using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	[SerializeField]
	int theState = 0;

	[SerializeField]
	SpriteRenderer sprite;

	[SerializeField]
	GameMasterScript master;

	[SerializeField]
	Collider2D collisions;

	// Use this for initialization
	void Start () {
		HandleCurrentState();
	}

	void OnEnable() {
		master.StateChanged += StateChangedHandler;
	}

	void OnDisable() {
		master.StateChanged -= StateChangedHandler;
	}

	void SetColor(bool isEnabled) {
		switch (theState)
		{
			case 0:
				sprite.color = new Color(0f, 0f, 1f, isEnabled ? 1f : 0.2f);
				break;
			case 1:
				sprite.color = new Color(0f, 1f, 0f, isEnabled ? 1f : 0.2f);
				break;
			default:
				sprite.color = new Color(1f, 1f, 1f, isEnabled ? 1f : 0.2f);
				break;

		}
	}

	private void HandleCurrentState() {
		if(master.CurrentState == theState) {
			collisions.enabled = true;
			SetColor(true);
		}
		else {
			collisions.enabled = false;
			SetColor(false);
		}
	}

	public void StateChangedHandler(object sender, EventArgs e) {
		HandleCurrentState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
