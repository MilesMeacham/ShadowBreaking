using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Character : MonoBehaviour {

    //test
    public Rigidbody2D rbody;
	private EnemyCreator enemyCreator; //new
    private bool isRunning = false;
    Animator anim;
    public Vector2 restedPos;
    //Lock controller during acting (any state other than the player having direct control is acting.)
    private bool isActing = false;
    private bool isBlocking = false;
    private bool isDodging = false;
    private bool knockback = false;
	private bool isInvuln = false;
    private int currentHealth;
	private float currentStamina;
    private float maxStamina = 100f;
	private float dodgeCost = 25f;
	private int maxPotions = 2;
	private int potionAmt;
	private int potionHealAmt = 50;
	
	private HeartManager heartManager;
	public float invincibilityTime = 0.5f;
	private bool invincible = false;
	private float dodgeCooldownTime = 0.5f;
	private bool dodgeOnCooldown = false;

    public int maxHealth = 100;    
    public float walkspeed = 1.8f; //was 1.5f
	private float slowWalkSpeed;
	private float maxWalkSpeed;
    public float runspeed = 2.3f; //was 2
	private float slowRunSpeed;
	private float maxRunSpeed;
    public float dodgeSpeed = 2.75f; //was 2.5f
    public float knockbackSpeed = 2;

    public AudioClip[] footsteps;
    public AudioClip spinAttack;
    public AudioClip playerHurt;
    AudioSource[] playerSounds;

    private Vector2 lastDirection;
    public float dodgeTime = 0.4f;
    public float knockbackTime = 0.2f;
	
	public Text restedText; 
	public Text potionText;
	
	private Rect staminaBar;
	private Texture2D staminaTexture;
    
    
    void Start () {
        rbody = GetComponentInChildren<Rigidbody2D>();
		potionAmt = maxPotions;
		potionText.text = ": " + maxPotions.ToString();
        currentHealth = maxHealth;
		currentStamina = maxStamina;
		maxWalkSpeed = walkspeed;
		maxRunSpeed = runspeed;
		slowWalkSpeed = walkspeed * 0.6f;
		slowRunSpeed = runspeed * 0.6f;
        anim = GetComponent<Animator>();
        playerSounds = GetComponents<AudioSource> ();
        playerSounds[0].clip = footsteps[0];
        playerSounds[1].clip = spinAttack;
        playerSounds[2].clip = playerHurt;
		staminaBar = new Rect(Screen.width/50, Screen.height/8, Screen.width*2, Screen.height/30);
		staminaTexture = new Texture2D(1,1);
		staminaTexture.SetPixel(0,0,Color.green);
		staminaTexture.Apply();
		
		heartManager = FindObjectOfType<HeartManager> ().GetComponent<HeartManager> ();
		if(Application.loadedLevelName != "Castle"){
			enemyCreator = GameObject.Find("EnemyManager").GetComponent<EnemyCreator>(); 
		}
    }


    /// <summary>
    /// Handles movement.
    /// </summary>
    /// <param name="movement_vector"></param>
    public void Move(Vector2 movement_vector)
    {
        if (movement_vector != Vector2.zero && isActing != true)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("Input_X", movement_vector.x);
            anim.SetFloat("Input_Y", movement_vector.y);

            // This is used for the dodge. He will dodge in the last direction he was moving.
            lastDirection = movement_vector;

            if (isRunning)
            {
                rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * runspeed);
            }
            else
            {
                rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * walkspeed);
            }

            if (!playerSounds[0].isPlaying)
                playerSounds[0].Play();
        }
        else if(knockback == true)
        {
            Debug.Log("knockback");
            rbody.MovePosition(rbody.position + -lastDirection * Time.deltaTime * knockbackSpeed);
        }
        else if(isDodging == true && knockback == false)
        {
            Debug.Log("isDodging");
            rbody.MovePosition(rbody.position + lastDirection * Time.deltaTime * dodgeSpeed);
        }
        else 
        {
            playerSounds[0].Stop();
            anim.SetBool("IsWalking", false);
        }
    }

	void Update()
	{
		//recharge stamina
		if(currentStamina <= maxStamina && isRunning == false)
			currentStamina += (Time.deltaTime * 5);
	}


    /// <summary>
    /// Toggles whether or not the player is walking or running.
    /// </summary>
    public void ToggleSpeed()
    {
        isRunning = !isRunning;
        if (isRunning)
            playerSounds[0].clip = footsteps[1];
        else
            playerSounds[0].clip = footsteps[0];
    }
	
	public void ToggleInvuln()
	{
		isInvuln = !isInvuln;
	}

    /// <summary>
    /// When the character is hit.
    /// </summary>
    /// 
    public bool TakeDamage(int damage)
    {
        // Don't take damage if player is block or invincible
        if (isBlocking || invincible || isInvuln)
        {
            return false;
        }
        
        currentHealth -= damage;
        playerSounds[2].Play();
        // Access heartManager and display the correct number of hearts
		heartManager.DisplayCorrectNumberOfHearts(currentHealth);
        
        isDead();

		StartCoroutine (Invincibility ());
        StartCoroutine(KnockbackCO());
        return true;
    }

	IEnumerator Invincibility()
	{
		invincible = true;
		yield return new WaitForSeconds (invincibilityTime);
		invincible = false;
	}

    public bool ActionOne()
    {
        //Implement based on equiped item.
		anim.SetTrigger ("Attack");
        playerSounds[1].Play();
        return true;
    }

    public bool ActionTwo()
    {
        //Implement based on equiped item.

        return true;
    }

    public void Dodge()
    {
        //Implement based on current movement direction
        if (!knockback && !isDodging) 
		{
			if(dodgeOnCooldown == false && currentStamina >= dodgeCost)
			{
				currentStamina -= dodgeCost;
				StartCoroutine(DodgeCO());
				StartCoroutine(DodgeCooldown());
			}
		}

    }
	
	public void Heal()
	{
		if(potionAmt != 0)
		{
			potionAmt -= 1;
			if(currentHealth + potionHealAmt <= maxHealth)
				currentHealth += potionHealAmt;
			else
				currentHealth = maxHealth;
			heartManager.DisplayCorrectNumberOfHearts(currentHealth);
			potionText.text = ": " + potionAmt.ToString();
		}
	}

    IEnumerator DodgeCO()
    {
        isActing = true;
        invincible = true;
        isDodging = true;
        
        yield return new WaitForSeconds(dodgeTime);

        isActing = false;
        invincible = false;
        isDodging = false;
    }

    IEnumerator KnockbackCO()
    {
        isActing = true;
        knockback = true;

        yield return new WaitForSeconds(knockbackTime);

        isActing = false;
        knockback = false;
    }

	IEnumerator DodgeCooldown()
	{
		dodgeOnCooldown = true;
		Debug.Log("Dodge on cooldown");
		yield return new WaitForSeconds(dodgeCooldownTime);
		
		dodgeOnCooldown = false;
		Debug.Log("Dodge off cooldown.");
	}


    /// <summary>
    /// Checks if the player's health is currently 0.
    /// </summary>
    /// <returns></returns>
    public bool isDead()
    {
        if(currentHealth <= 0)
        {
            isActing = true;
            EventManager.TriggerEvent("PlayerDead");
            return true;
        }
        else
        {
            return false;
        }

    }

    public void Resurrection()
    {
        currentHealth = maxHealth;
		potionAmt = maxPotions;
		potionText.text = ": " + maxPotions.ToString();
		heartManager.DisplayCorrectNumberOfHearts(currentHealth); //reset UI hearts to full
        isActing = false;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag("Bonfire"))
        {
			Debug.Log("Touched bonfire collider");
            restedPos = new Vector2(transform.position.x, transform.position.y);
			currentHealth = maxHealth;
			currentStamina = maxStamina;
			potionAmt = maxPotions;
			potionText.text = ": " + maxPotions.ToString();
			heartManager.DisplayCorrectNumberOfHearts(currentHealth); //reset UI hearts to full
			enemyCreator.ResetAllEnemies(); //new
			StartCoroutine(Timer());
        }
		else if(other.gameObject.CompareTag("Water"))
		{
			Debug.Log("In water.");
			walkspeed = slowWalkSpeed;
			runspeed = slowRunSpeed;
		}
		else if(other.gameObject.CompareTag("Teleporter"))
		{
			Debug.Log("Teleporting to next level");
			if(Application.loadedLevelName == "Arena_Scene_Final")
				SceneManager.LoadScene(2);
			else if(Application.loadedLevelName == "Ultimate_Forest")
				SceneManager.LoadScene(3);
			else if(Application.loadedLevelName == "Cave_Final")
				SceneManager.LoadScene(4);
		}
    }
	
	
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Water"))
		{
			Debug.Log("In water.");
			walkspeed = maxWalkSpeed;
			runspeed = maxRunSpeed;
		}
	}
	
	IEnumerator Timer()
	{
		restedText.enabled = true;
		yield return new WaitForSeconds(3);
		restedText.enabled = false;
	}

	void OnGUI()
	{
		float ratio = currentStamina/maxStamina;
		float rectWidth = ratio * Screen.width/4;
		staminaBar.width = rectWidth;
		GUI.DrawTexture(staminaBar, staminaTexture);
	}
}
