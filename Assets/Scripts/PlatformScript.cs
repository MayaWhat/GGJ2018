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
		if(isEnabled) {
			StartCoroutine(Fade(sprite.color, Color.white));
		}
		else {
			StartCoroutine(Fade(sprite.color, new Color(0x18 / 255, 0x3F / 255, 0x3E / 255, 0.25f)));
		}
	}

	IEnumerator Fade(Color startColor, Color endColor) {
		for (float f = 0f; f < 1; f += 0.05f) {
			sprite.color = Color.Lerp(startColor, endColor, f);
			yield return null;
		}
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
