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
	Collider2D collisions;

	// Use this for initialization
	void Start () {
		HandleCurrentState();
		GameMasterScript.TheMaster.StateChanged += StateChangedHandler;
	}

	void SetColor(bool isEnabled) {
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, isEnabled ? 1f : 0f);
	}

	private void HandleCurrentState() {
		if(GameMasterScript.TheMaster.CurrentState == theState) {
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
