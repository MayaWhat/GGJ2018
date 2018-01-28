using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

	public static MusicScript Instance {
		get;
		private set;
	}

	[SerializeField]
	AudioSource music1;

	[SerializeField]
	AudioSource music2;

	[SerializeField]
	float maxVolume;

	// Use this for initialization
	void Start () {
		music1.Play();
		music2.Play();

		Reset();
	}

	void Reset() {
		StartCoroutine(FadeInChannel(music1, 0.1f));
		StartCoroutine(FadeOutChannel(music2, 0.1f));
		GameMasterScript.TheMaster.StateChanged += StateChangedHandler;
	}

	void Awake() {
		if(Instance != null && Instance != this) {
			Destroy(gameObject);
			Instance.Reset();
		}
		else {
			Instance = this;
		}

		DontDestroyOnLoad(gameObject);
	}

	
	
	// Update is called once per frame
	void Update () {
		
	}

	private void HandleCurrentState() {
		if(GameMasterScript.TheMaster.CurrentState == 0) {
			StartCoroutine(FadeInChannel(music1, 0.1f));
			StartCoroutine(FadeOutChannel(music2, 0.1f));
		}
		else {
			StartCoroutine(FadeInChannel(music2, 0.1f));
			StartCoroutine(FadeOutChannel(music1, 0.1f));
		}
	}

	public void StateChangedHandler(object sender, EventArgs e) {
		HandleCurrentState();
	}

	IEnumerator FadeInChannel(AudioSource channel, float speed) {
		for(float f = 0f; f < maxVolume; f += speed) {
			var volume = Mathf.Clamp(f, channel.volume, maxVolume);

			channel.volume = f;
			yield return null;
		}

		channel.volume = maxVolume;
	}

	IEnumerator FadeOutChannel(AudioSource channel, float speed) {
		for(float f = maxVolume; f >= 0f; f -= speed) {
			var volume = Mathf.Clamp(f, 0, channel.volume);

			channel.volume = f;
			yield return null;
		}

		channel.volume = 0;
	}
}
