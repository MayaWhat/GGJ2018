using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour {

	[SerializeField]
	float jumpVelocityBoost;

	Animator animator;

	AudioSource audio;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        var rigidBody = other.GetComponent<Rigidbody2D>();
		if(rigidBody != null) {
			var velocityIncrease = jumpVelocityBoost - rigidBody.velocity.y;
			animator.SetBool("IsBounced", true);
			audio.Play();
			rigidBody.AddForce(new Vector2(0, velocityIncrease), ForceMode2D.Impulse);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		var rigidBody = other.GetComponent<Rigidbody2D>();
		if(rigidBody != null) {
			animator.SetBool("IsBounced", false);
		}
	}
}
