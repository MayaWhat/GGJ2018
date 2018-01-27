using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour {

	[SerializeField]
	float movementSpeed;

	[SerializeField]
	Transform target;

	[SerializeField]
	Rigidbody2D body;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		var playerDirection = ((target.transform.position - transform.position).normalized);
		var velocityChange = (playerDirection * movementSpeed) - new Vector3(body.velocity.x, body.velocity.y, 0f);
		body.AddForce(velocityChange, ForceMode2D.Impulse);
	}
}
