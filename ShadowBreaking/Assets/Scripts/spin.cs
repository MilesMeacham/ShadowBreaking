using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {
    public float speed = 15.0f;
    public bool counterclockwise = false;

	// START:
	void Start () {
	
	}
	
	// UPDATE:
	void Update () {
        if (counterclockwise) this.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        else this.transform.Rotate(Vector3.back * Time.deltaTime * speed);
    }
}