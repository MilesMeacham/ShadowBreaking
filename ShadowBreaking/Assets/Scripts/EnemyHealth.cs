using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 50;
	public int currentHealth;
    EnemyManager EnemyController;
	GameObject Teleporter;
	SpriteRenderer tpRend;
	BoxCollider2D tpCollide;
	private UnityAction damageListener;

    public float knockbackTime = 0.1f;
    public EnemyMovement movement;
    public WhiteSkeli whiteMovement;
    public Sorcerer sorcerer;

    public Image healthBar;
    private float healthRemaining;

    bool isDead = false;

    public AudioClip[] hurtSounds;
    AudioSource[] enemySounds;
	
	void Start()
	{
        
		currentHealth = startingHealth;
        EnemyController = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        enemySounds = GetComponents<AudioSource>();
		Teleporter = GameObject.Find("Exit Level Totem");
		if(this.gameObject.name == "SorcererBoss" || this.gameObject.name == "SorcererBoss(Clone)")
		{
			tpRend = Teleporter.GetComponent<SpriteRenderer>();
			tpCollide = Teleporter.GetComponent<BoxCollider2D>();
		}
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

        if (enemySounds[2].clip != null)
        {
            enemySounds[2].clip = hurtSounds[Mathf.RoundToInt(Random.value * (hurtSounds.Length - 1))];
            enemySounds[2].Play();
        }

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
        if (movement != null)
        {
            movement.knockback = true;

            yield return new WaitForSeconds(knockbackTime);

            movement.knockback = false;
            movement.stunned = true;

            yield return new WaitForSeconds(knockbackTime);

            movement.stunned = false;
        }
        else if (whiteMovement != null)
        {
            whiteMovement.knockback = true;

            yield return new WaitForSeconds(knockbackTime);

            whiteMovement.knockback = false;
            whiteMovement.stunned = true;

            yield return new WaitForSeconds(knockbackTime);

            whiteMovement.stunned = false;
        }
        else if (sorcerer != null)
        {
            sorcerer.knockback = true;

            yield return new WaitForSeconds(knockbackTime);

            sorcerer.knockback = false;
            sorcerer.stunned = true;

            yield return new WaitForSeconds(knockbackTime);

            sorcerer.stunned = false;
        }
            
    }

    void Death ()
	{
		if (isDead != true) {
			
			isDead = true;
            //Destroy(gameObject);
			Debug.Log("This objects name is " + this.gameObject.name);
			if(this.gameObject.name == "SorcererBoss(Clone)" || this.gameObject.name == "SorcererBoss")
			{
				Debug.Log("Entering IMPORTANT FuNCTION");
				tpRend = Teleporter.GetComponent<SpriteRenderer>();
				tpRend.enabled = true;
				//Teleporter.SetActive(true);
				tpCollide.enabled = true;
			}

            this.gameObject.SetActive(false);

			//EnemyController.UpdateDeath();
			
			Debug.Log ("Enemy has died");
		}
	}
}