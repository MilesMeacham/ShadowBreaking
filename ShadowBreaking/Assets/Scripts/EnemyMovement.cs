using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Rigidbody2D rbody;
	Animator anim;

	// AI VARIABLES:
	public float moveSpeed = 0.5f;
	public float chaseDistance = 3.0f;
	public float attackDistance = 1.0f;
	private float distanceToPlayer;
	private float xDiff;
	private float yDiff;
	private float xLoc;
	private float yLoc;

	private bool walking = false;

	private Vector2 currentEnemyPos;
	private Vector2 prevEnemyPos;

	private GameObject player;


	// START:
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	} // END START()

	// UPDATE:
	void Update() {
		distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

		//  If the distance between the mob and the player
		//  is less than or equal to the ChaseDistance,
		//  and greater than the AttackDistance,
		//  the mob moves towards the player.
		if (distanceToPlayer <= chaseDistance && distanceToPlayer > attackDistance) {
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

			currentEnemyPos = transform.position;

			walking = true;
		}


		// obtain difference between the current position and the past position to send to animator
		xDiff = currentEnemyPos.x - prevEnemyPos.x;
		yDiff = currentEnemyPos.y - prevEnemyPos.y;

		if (walking == true) {
			anim.SetBool ("IsWalking", true);
			anim.SetFloat ("Input_X", xDiff);
			anim.SetFloat ("Input_Y", yDiff);
		} else {
			anim.SetBool ("IsWalking", false);
			xLoc = player.transform.position.x - transform.position.x;
			yLoc = player.transform.position.y - transform.position.y;
			anim.SetFloat ("Input_X", xLoc);
			anim.SetFloat ("Input_Y", yLoc);
		}

		prevEnemyPos = currentEnemyPos;

		walking = false;
	} // END UPDATE()
} // END CLASS EnemyMovement