using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatformScript : PlatformScript {

	PulseScript pulse;

	void Start () {
		pulse = GetComponent<PulseScript>();
		OnStart();
	}

	void Update() {

	}

	protected override void HandleEnabledState(bool isEnabled) {
		if(isEnabled) {
			pulse.enabled = true;
			foreach(var sprite in pulse.renderers) {
				StartCoroutine(Fade(sprite, sprite.color, Color.white));
			}
		}
		else {
			pulse.enabled = false;
			foreach(var sprite in pulse.renderers) {
				StartCoroutine(Fade(sprite, sprite.color, disabledColor));
			}
		}
	}
}
