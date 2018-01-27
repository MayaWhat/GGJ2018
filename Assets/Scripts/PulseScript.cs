using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScript : MonoBehaviour {

	[SerializeField]
	Color color1;

	[SerializeField]
	Color color2;

	[SerializeField]
	float rate;

	public SpriteRenderer[] renderers;

	Color startColor;
	Color endColor;
	bool doing = false;
	bool direction12 = true;

	// Use this for initialization
	void Start () {
		renderers = GetComponentsInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!doing) {
			doing = true;
			StartCoroutine(Fade(direction12 ? color1 : color2, direction12? color2 : color1));
			direction12 = !direction12;
		}
	}

	IEnumerator Fade(Color startColor, Color endColor) {
		for (float f = 0f; f < 1; f += rate) {
			foreach(var renderer in renderers) {
				if(!enabled) {
					break;
				}

				renderer.color = Color.Lerp(startColor, endColor, f);
			}

			if(!enabled) {
				break;
			}

			yield return null;
		}

		doing = false;
	}
}
