using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour {

	[SerializeField]
	float jumpVelocityBoost;

	List<Collider2D> affectedColliders;

	// Use this for initialization
	void Start () {
		affectedColliders = new List<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        var rigidBody = other.GetComponent<Rigidbody2D>();
		if(rigidBody != null && !affectedColliders.Contains(other)) {
			affectedColliders.Add(other);
			rigidBody.AddForce(new Vector2(0, jumpVelocityBoost), ForceMode2D.Impulse);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(affectedColliders.Contains(other)) {
			affectedColliders.Remove(other);
		}
	}
}
