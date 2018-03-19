using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour {


	public GameObject reticle;

	public Color inactiveReticleColor = Color.gray;

	public Color activeReticleColor = Color.green;

	private GazeableObject currentGazeObject;

	private GazeableObject currentSelectedObject;

	private RaycastHit lastHit;

	// Use this for initialization
	void Start () {
		SetReticleColor (inactiveReticleColor);
	}

	// Update is called once per frame
	void Update () {

		ProcessGaze();
		CheckForInput (lastHit);

	}


	public void ProcessGaze()
	{
		Ray raycastRay = new Ray (transform.position, transform.forward);
		RaycastHit hitInfo;

		Debug.DrawRay (raycastRay.origin, raycastRay.direction * 100);

		if (Physics.Raycast (raycastRay, out hitInfo)) {

			//do something with the object
			//check if the object is interactable
			//get the game objet from the hitInfo
			GameObject hitObj = hitInfo.collider.gameObject;
			//get the gazeableObject from the hit object
			GazeableObject gazeObj = hitObj.GetComponentInParent<GazeableObject> ();
			//object has a gazeableobject component
			if (gazeObj != null) {

				//object we're looking is different
				if (gazeObj != currentGazeObject) {
					ClearCurrentObject ();
					currentGazeObject = gazeObj;
					currentGazeObject.OnGazeEnter (hitInfo);
					SetReticleColor (activeReticleColor);
				} else {
					currentGazeObject.OnGaze (hitInfo);
				}
			} else {
				ClearCurrentObject ();
			}

			lastHit = hitInfo;

			//check if the object is a new object
			//set the reticle color
		} else {
			ClearCurrentObject ();
		}


	}

	private void SetReticleColor(Color reticleColor)
	{
		//SET the color for the reticle
		reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);
	}

	private void CheckForInput(RaycastHit hitInfo)
	{
		//check for down
		if (Input.GetMouseButtonDown (0) && currentGazeObject != null) {
			currentSelectedObject = currentGazeObject;
			currentSelectedObject.OnPress (hitInfo);
		}
		//check for hold
		else if (Input.GetMouseButton(0) && currentSelectedObject != null){
			currentSelectedObject.OnHold(hitInfo);
		}
		//check for release
		else if (Input.GetMouseButtonUp(0) && currentSelectedObject != null){
			currentSelectedObject.OnRelease (hitInfo);
			currentSelectedObject = null;
		}
	}

	private void ClearCurrentObject()
	{
		if (currentGazeObject != null) 
		{
			currentGazeObject.OnGazeExit ();
			SetReticleColor (inactiveReticleColor);
			currentGazeObject = null;
		}
	}
}
