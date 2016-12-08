using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 50;
	public int currentHealth;
    EnemyCreator EnemyController;
	GameObject Teleporter;
	SpriteRenderer tpRend;
	BoxCollider2D tpCollide;
	private UnityAction damageListener;
	public int maxSpawn = 7;
	public int deaths;

    public float knockbackTime = 0.1f;
    public EnemyMovement movement;
    public WhiteSkeli whiteMovement;
    public Sorcerer sorcerer;
    public HealerAI healerMovement;

    public Image healthBar;
    private float healthRemaining;

    bool isDead = false;

    public AudioClip[] hurtSounds;
    AudioSource[] enemySounds;
	
	void Start()
	{
        
		currentHealth = startingHealth;
        EnemyController = GameObject.Find("EnemyManager").GetComponent<EnemyCreator>();
        enemySounds = GetComponents<AudioSource>();
		Teleporter = GameObject.Find("Exit Portal");
		if(this.gameObject.name == "SorcererBoss" || this.gameObject.name == "SorcererBoss(Clone)" || Application.loadedLevelName == "Arena_Scene_Final")
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

    public void AddHealth (int amount)
    {
        currentHealth += amount;

        if (currentHealth > startingHealth)
            currentHealth = startingHealth;

        // This sets the fill amount to the amount of health remaining 
        healthRemaining = (float)currentHealth / (float)startingHealth;
        healthBar.fillAmount = healthRemaining;
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
        else if (healerMovement != null)
        {
            healerMovement.knockback = true;

            yield return new WaitForSeconds(knockbackTime);

            healerMovement.knockback = false;
            healerMovement.stunned = true;

            yield return new WaitForSeconds(knockbackTime);

            healerMovement.stunned = false;
        }

    }

    void Death()
	{
		if (isDead != true) 
		{
			isDead = true;
			if(Application.loadedLevelName == "Arena_Scene_Final")
			{
				EnemyController.addDeath();
				deaths = EnemyController.getDeaths();
				if(deaths == maxSpawn)
				{
					Debug.Log("You win. NPC and teleporter should spawn");
					tpRend = Teleporter.GetComponent<SpriteRenderer>();
					tpRend.enabled = true;
					//Teleporter.SetActive(true);
					tpCollide.enabled = true;
				}
			}
			else
			{
				if(Application.loadedLevelName == "Brady_Forest_Sandbox" && (this.gameObject.name == "SorcererBoss(Clone)" || this.gameObject.name == "SorcererBoss"))
				{
					//Debug.Log("Entering IMPORTANT FUNCTION");
					EnemyController.setBossDeathState("boss", true);
					tpRend = Teleporter.GetComponent<SpriteRenderer>();
					tpRend.enabled = true;
					tpCollide.enabled = true;
				}
				else if((Application.loadedLevelName == "Brady_Cave_Scene" || Application.loadedLevelName == "Cave_Final") && (this.gameObject.name == "SorcererBoss(Clone)" || this.gameObject.name == "SorcererBoss"))
				{
					EnemyController.setBossDeathState("miniboss", true);
				}
			}
			
			this.gameObject.SetActive(false);

			Debug.Log ("Enemy has died");
		}
	}
}