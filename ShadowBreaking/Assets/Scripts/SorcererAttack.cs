using UnityEngine;
using System.Collections;

public class SorcererAttack : MonoBehaviour {
	// AI VARIABLES:

	public float attackDistance = 1.0f;
	public float waitTime = 0.5f;
	private float distanceToPlayer;

	private GameObject player;

    //public AudioClip[] attackSounds;
    //AudioSource attackSound;

	// START:
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
        //attackSound = GetComponent<AudioSource>();
	} // END START()

	// UPDATE:
	void Update() {
		distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

		//  If the distance between the mob and the player
		//  is less than or equal to the AttackDistance,
		//  the mob launches a projectile at the player.
		if (distanceToPlayer <= attackDistance) {
			Debug.Log(this.gameObject.name + " is now in range! Ready to shoot player!");
			transform.position = transform.position;

			waitTime -= Time.deltaTime;
			if (waitTime <= 0) {
				
                //attackSound.clip = attackSounds[Mathf.RoundToInt(Random.value * (attackSounds.Length - 1))];
                //attackSound.Play();
				waitTime = 1.0f;
			}
		}
	} // END UPDATE()

	public float GetAttackDistance ()
	{
		return attackDistance;
	}
} // END CLASS EnemyAttack