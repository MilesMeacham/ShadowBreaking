using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public int damage = 10;

	PlayerMovement playerMovement;

	bool isDead;
	bool damaged;

	private UnityAction damageListener; //this is to test player death
	private UnityAction resurrectionListener;
	private SpriteRenderer sprite_renderer;

	//MILES ADDED
	private HeartManager heartManager;

	void Awake ()
	{
		playerMovement = GetComponent <PlayerMovement> ();

		currentHealth = startingHealth;

		damageListener = new UnityAction (DoDamage);
		resurrectionListener = new UnityAction (Resurrection);

		//MILES ADDED
		heartManager = FindObjectOfType<HeartManager> ().GetComponent<HeartManager> ();
	}


	void Update ()
	{
		sprite_renderer = GetComponent<SpriteRenderer> ();

		damaged = false;
	}

	void OnEnable ()
	{
		EventManager.StartListening ("damage", damageListener);
		EventManager.StartListening ("Resurrection", resurrectionListener);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("damage", damageListener);
		EventManager.StopListening ("Resurrection", resurrectionListener);
	}


	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		//MILES ADDED
		heartManager.DisplayCorrectNumberOfHearts(currentHealth);

		if(currentHealth <= 0 && !isDead)
		{
			EventManager.TriggerEvent("PlayerDead");
			//Death ();
		}
	}


	void DoDamage()
	{
		TakeDamage (damage);
	}


	void Death ()
	{
		if (isDead != true) {
			
			isDead = true;

			playerMovement.enabled = false;
			sprite_renderer.enabled = false;

			Debug.Log ("Player has died");
			EventManager.TriggerEvent ("PlayerDead");
		}
	}


	void Resurrection ()
	{
		if (isDead != false) {
			
			isDead = false;

			playerMovement.enabled = true;
			sprite_renderer.enabled = true;

			currentHealth = startingHealth;
		}
	}
}