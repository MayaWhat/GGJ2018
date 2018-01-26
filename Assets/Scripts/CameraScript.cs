using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	[SerializeField]
	GameObject cameraFocus;

	[SerializeField]
	GameMasterScript master;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(cameraFocus != null) {
			var yLimit = Mathf.Max(cameraFocus.transform.position.y, master.LowerLevelBound);

			transform.position = new Vector3(cameraFocus.transform.position.x, yLimit, transform.position.z);
		}
	}
}
