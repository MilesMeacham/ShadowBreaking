using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 50;
	public int currentHealth;
    EnemyManager EnemyController;
	private UnityAction damageListener;
	
	bool isDead = false;
	
	void Start()
	{
		currentHealth = startingHealth;
        EnemyController = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
	}
	
	/*void OnEnable ()
	{
		EventManager.StartListening ("damage", damageListener);
	}
	
	void OnDisable ()
	{
		EventManager.StopListening ("damage", damageListener);
	} */
	
	public void TakeDamage (int amount)
	{
		//damaged = true;

		currentHealth -= amount;
		Debug.Log ("Enemy has been damaged.");

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}
	
	void Death ()
	{
		if (isDead != true) {
			
			isDead = true;
            Destroy(gameObject);
			
			EnemyController.UpdateDeath();
			
			Debug.Log ("Enemy has died");


		}
	}
}