using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	[SerializeField]
	GameObject cameraFocus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(cameraFocus != null) {
			var yLimit = Mathf.Max(cameraFocus.transform.position.y, GameMasterScript.TheMaster.LowerLevelBound + 12);

			transform.position = new Vector3(cameraFocus.transform.position.x, yLimit, transform.position.z);
		}
	}
}
