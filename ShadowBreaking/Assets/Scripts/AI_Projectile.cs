using UnityEngine;
using System.Collections;

public class AI_Projectile : MonoBehaviour {
    public int MoveSpeed = 1;
    public GameObject Player;
	private Vector2 playerPos;

    // START:
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
		playerPos = Player.transform.position; //fire at players last position
        Destroy(gameObject, 3); //Destroy self after 3 seconds
    } // END START
	
	// UPDATE:
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, MoveSpeed * Time.deltaTime);
    } // END UPDATE

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") //If projectile collides with Player
		{
            Destroy(gameObject);
			Player.GetComponent<Character>().TakeDamage(10); //Signals the TakeDamage function in the Character script attached to Player
		}
    }
} // END CLASS AI_Projectile