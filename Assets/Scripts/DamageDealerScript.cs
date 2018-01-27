using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealerScript : MonoBehaviour {

	[SerializeField]
	public int DamageAmount;

	[SerializeField]
	public bool ReceiveDamageIfAbove;

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
