using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	// AI VARIABLES:

	public float attackDistance = 1.0f;
	public float waitTime = 1.5f;
	private float distanceToPlayer;

	private GameObject player;
	public GameObject projectile;

    public AudioClip[] attackSounds;
    AudioSource[] enemySounds;

    private ObjectPooler projectilePooler;

	// START:
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
        enemySounds = GetComponents<AudioSource>();

        projectilePooler = GameObject.Find("ProjectilePooler").GetComponent<ObjectPooler>(); ;

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
			if (waitTime <= 0 && projectile) {
                projectile = projectilePooler.GetPooledObject();
                projectile.transform.position = transform.position;
                projectile.SetActive(true);
                    //(projectile, transform.position, Quaternion.identity);
                enemySounds[1].clip = attackSounds[Mathf.RoundToInt(Random.value * (attackSounds.Length - 1))];
                enemySounds[1].Play();
				waitTime = 1.0f;
			}
		}
	} // END UPDATE()

	public float GetAttackDistance ()
	{
		return attackDistance;
	}
} // END CLASS EnemyAttack