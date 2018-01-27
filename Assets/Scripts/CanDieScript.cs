using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDieScript : MonoBehaviour {

	[SerializeField]
	DamageDealerScript damageReceiver;

	[SerializeField]
	int hp;

	// Use this for initialization
	void Start () {
		damageReceiver.DamagedReceived += HandleDamageReceived;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void HandleDamageReceived(object sender, EventArgs args)
	{
		hp--;
		if(hp <= 0) {
			Destroy(gameObject);
		}
	}
}
