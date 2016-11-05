using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 50;
	public int currentHealth;
    EnemyManager EnemyController;
	private UnityAction damageListener;

    public float knockbackTime = 0.1f;
    private EnemyMovement movement;


    bool isDead = false;
	
	void Start()
	{
        movement = GetComponent<EnemyMovement>();
		currentHealth = startingHealth;
        EnemyController = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
	}
	
	public void TakeDamage (int amount)
	{
		currentHealth -= amount;
		Debug.Log ("Enemy has been damaged.");

        StartCoroutine(KnockbackCO());

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

    IEnumerator KnockbackCO()
    {
        movement.knockback = true;

        yield return new WaitForSeconds(knockbackTime);

        movement.knockback = false;
        movement.stunned = true;

        yield return new WaitForSeconds(knockbackTime);

        movement.stunned = false;
    }

    void Death ()
	{
		if (isDead != true) {
			
			isDead = true;
            //Destroy(gameObject);

            this.gameObject.SetActive(false);

			//EnemyController.UpdateDeath();
			
			Debug.Log ("Enemy has died");
		}
	}
}