using UnityEngine;
using System.Collections;

public class AI_Projectile : MonoBehaviour {
    public int MoveSpeed = 1;
    private GameObject Player;
	private Vector2 playerPos;

    // START:
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
		playerPos = Player.transform.position;
        Destroy(gameObject, 3);
    } // END START
	
	// UPDATE:
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, MoveSpeed * Time.deltaTime);
    } // END UPDATE

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
		{
            Destroy(gameObject);
			EventManager.TriggerEvent("damage");
		}
    }
} // END CLASS AI_Projectile