using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEyeScript : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		this.GetComponent<Camera>().stereoTargetEye = StereoTargetEyeMask.None;
	}
	

}
