using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 50;
	public int currentHealth;
    EnemyManager EnemyController;
	private UnityAction damageListener;

    public float knockbackTime = 0.1f;
    private EnemyMovement movement;

    public Image healthBar;
    private float healthRemaining;

    bool isDead = false;
	
	void Start()
	{
        movement = GetComponent<EnemyMovement>();
		currentHealth = startingHealth;
        EnemyController = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
	}

    /// <summary>
    /// Resets the health when the enemy is enabled (spawned)
    /// </summary>
    void OnEnable()
    {
        currentHealth = startingHealth;

        // This sets the fill amount to the amount of health remaining 
        healthRemaining = (float)currentHealth / (float)startingHealth;
        healthBar.fillAmount = healthRemaining;
    }
	
	public void TakeDamage (int amount)
	{
		currentHealth -= amount;


        // This sets the fill amount to the amount of health remaining 
        healthRemaining = (float)currentHealth / (float)startingHealth;
        healthBar.fillAmount = healthRemaining;

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