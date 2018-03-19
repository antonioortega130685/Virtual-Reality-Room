using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsButton : GazeableButton {


	public Object prefab;

	public override void OnPress(RaycastHit hitInfo){

		base.OnPress (hitInfo);

		// Set player mode to place furniture
		Player.instance.activeMode = InputMode.OBJECTS;

		// Set active furniture prefab to this prefab
		Player.instance.activeObjectsPrefab = prefab;
	}
}
