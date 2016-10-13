using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour {

	PlayerHealth playerHealth;


	void Update () {

		//this is to test damaging the player and triggeres the 'damage' event anytime the "p" key is pressed on the keyboard
		//Add to a Test gameobject

		if (Input.GetKeyDown ("p")) {
			EventManager.TriggerEvent ("damage");
		}
	}
}
