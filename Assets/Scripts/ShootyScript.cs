using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyScript : MonoBehaviour {

	[SerializeField]
	ProjectileScript projectile;

	[SerializeField]
	int shootFrequency;

	[SerializeField]
	int currentShootCounter = 0;

	[SerializeField]
	float projectileSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if(currentShootCounter <= 0) {
			ShootyShoot();
			currentShootCounter = shootFrequency;
		}
		else {
			currentShootCounter--;
		}
	}

	void ShootyShoot() {
		var projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
		projectileInstance.Momentum = (transform.rotation * Vector3.up) * projectileSpeed;
	}
}
