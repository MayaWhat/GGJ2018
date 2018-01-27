using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiverScript : MonoBehaviour {

	public void ReceiveDamage() {
		if(DamagedReceived != null) {
			DamagedReceived.Invoke(this, new EventArgs());
		}
	}

	public event EventHandler DamagedReceived;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
