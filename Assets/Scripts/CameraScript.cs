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

			var cameraYSize = GetComponent<Camera>().orthographicSize;
			var cameraXSize = cameraYSize / 9 * 16;

			var xLimit = Mathf.Clamp(
				cameraFocus.transform.position.x, 
				GameMasterScript.TheMaster.LeftLevelBound + cameraXSize, 
				GameMasterScript.TheMaster.RightLevelBound - cameraXSize
			);

			var yLimit = Mathf.Clamp(
				cameraFocus.transform.position.y, 
				GameMasterScript.TheMaster.LowerLevelBound + cameraYSize, 
				GameMasterScript.TheMaster.UpperLevelBound - cameraYSize
			);

			transform.position = new Vector3(xLimit, yLimit, transform.position.z);
		}
	}
}
