using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	[SerializeField]
	int theState = 0;

	[SerializeField]
	protected SpriteRenderer sprite;

	[SerializeField]
	Collider2D collisions;

	// Use this for initialization
	void Start () {
		OnStart();
	}

	protected void OnStart() {
		HandleCurrentState();
		GameMasterScript.TheMaster.StateChanged += StateChangedHandler;
	}

	protected Color disabledColor = new Color(0x18 / 255, 0x3F / 255, 0x3E / 255, 0.25f);

	protected virtual void HandleEnabledState(bool isEnabled) {
		if(isEnabled) {
			StartCoroutine(Fade(sprite, sprite.color, Color.white));
		}
		else {
			StartCoroutine(Fade(sprite, sprite.color, disabledColor));
		}
	}

	protected IEnumerator Fade(SpriteRenderer renderer, Color startColor, Color endColor) {
		for (float f = 0f; f < 1; f += 0.05f) {
			renderer.color = Color.Lerp(startColor, endColor, f);
			yield return null;
		}
	}

	private void HandleCurrentState() {
		if(GameMasterScript.TheMaster.CurrentState == theState) {
			collisions.enabled = true;
			HandleEnabledState(true);
		}
		else {
			collisions.enabled = false;
			HandleEnabledState(false);
		}
	}

	public void StateChangedHandler(object sender, EventArgs e) {
		HandleCurrentState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
