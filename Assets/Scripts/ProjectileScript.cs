using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	[SerializeField]
	public Vector2 Momentum;
	[SerializeField]
	Rigidbody2D body;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		var velocityChange = Momentum - new Vector2(body.velocity.x, body.velocity.y);
		body.AddForce(velocityChange, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D coll) {
        Destroy(gameObject);        
    }
}
