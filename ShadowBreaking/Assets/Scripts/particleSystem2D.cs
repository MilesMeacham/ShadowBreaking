using UnityEngine;
using System.Collections;

public class particleSystem2D : MonoBehaviour {
    public Rigidbody particle;
    public float speed = 0.1f;
    public float waitTime = 0.5f;
    private float originalWaitTime;

    // START:
    void Start () {
        originalWaitTime = waitTime;
    }
	
	// UPDATE:
	void Update () {
        //Instantiate(particle, this.transform.position, Quaternion.identity);

        waitTime -= Time.deltaTime;
        if (waitTime <= 0) {
            int x = Random.Range(-10, 10);
            int y = Random.Range(-10, 10);

            Rigidbody clone;
            clone = (Rigidbody)Instantiate(particle, this.transform.position, Quaternion.identity);
            clone.velocity = this.transform.TransformDirection(x * speed, y * speed, 0);
            Destroy(clone, 3);
            waitTime = originalWaitTime;
        }
    }
}