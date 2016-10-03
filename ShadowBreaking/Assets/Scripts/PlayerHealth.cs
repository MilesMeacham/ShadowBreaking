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
	private SpriteRenderer sprite_renderer;


	void Awake ()
	{
		playerMovement = GetComponent <PlayerMovement> ();

		currentHealth = startingHealth;

		damageListener = new UnityAction (DoDamage);
	}


	void Update ()
	{
		sprite_renderer = GetComponent<SpriteRenderer> ();

		damaged = false;
	}

	void OnEnable ()
	{
		EventManager.StartListening ("damage", damageListener);
	}

	void OnDisable ()
	{
		EventManager.StopListening ("damage", damageListener);
	}


	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;
		Debug.Log ("Player has been damaged.");

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}


	void Death ()
	{
		isDead = true;

		playerMovement.enabled = false;
		sprite_renderer.enabled = false;

		Debug.Log ("Player has died");
		EventManager.TriggerEvent ("PlayerDead");
	}

	void DoDamage()
	{
		TakeDamage (damage);
	}
}