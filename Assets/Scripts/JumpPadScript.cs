using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour {

	[SerializeField]
	float jumpVelocityBoost;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        var rigidBody = other.GetComponent<Rigidbody2D>();
		if(rigidBody != null) {
			var velocityIncrease = jumpVelocityBoost - rigidBody.velocity.y;
			rigidBody.AddForce(new Vector2(0, velocityIncrease), ForceMode2D.Impulse);
		}
	}
}
