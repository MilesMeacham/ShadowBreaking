using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour {

	PlayerHealth playerHealth;


	void Update () {
		if (Input.GetKeyDown ("q")) {
			EventManager.TriggerEvent ("test");
		}
		if (Input.GetKeyDown ("p")) {
			EventManager.TriggerEvent ("damage");
		}
	}
}
