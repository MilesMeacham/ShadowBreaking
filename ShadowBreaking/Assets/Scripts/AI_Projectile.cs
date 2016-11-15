using UnityEngine;
using System.Collections;

public class AI_Projectile : MonoBehaviour {

    public int MoveSpeed = 1;
    public GameObject Player;
	private Vector2 playerPos;
    private float projectileLifeSpan = 2f;

    void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        playerPos = Player.transform.position; //fire at players last position

        StartCoroutine(ProjectileDestroyCO());  
    }
	
	// UPDATE:
	void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, MoveSpeed * Time.deltaTime);
    } // END UPDATE

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") //If projectile collides with Player
		{
            // Destroy(gameObject);

            gameObject.SetActive(false);
            Player.GetComponent<Character>().TakeDamage(10); //Signals the TakeDamage function in the Character script attached to Player
		}
    }

    public IEnumerator ProjectileDestroyCO()
    {

        yield return new WaitForSeconds(projectileLifeSpan);
        gameObject.SetActive(false);
        Debug.Log("Bullet Destroyed");
    }

} // END CLASS AI_Projectile

