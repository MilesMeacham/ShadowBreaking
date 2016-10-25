using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class Character : MonoBehaviour {

    
    public Rigidbody2D rbody;
    private bool isRunning = false;
    Animator anim;
    Vector2 restedPos;
    //Lock controller during acting (any state other than the player having direct control is acting.)
    private bool isActing = false;
    private bool isBlocking = false;
    private bool isDodging = false;
    private int currentHealth;
	
	private HeartManager heartManager;
	public float invincibilityTime = 0.5f;
	private bool invincible = false;

    public int maxHealth = 100;    
    public float walkspeed = 1.5f;
    public float runspeed = 2;
    public float dodgeSpeed = 2.5f;

    public AudioClip[] footsteps;
    AudioSource moveSound;

    private Vector2 lastDirection;
    public float dodgeTime = 0.4f;
    
    
    void Start () {
        rbody = GetComponentInChildren<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        moveSound = GetComponent<AudioSource> ();
        moveSound.clip = footsteps[0];
		
		heartManager = FindObjectOfType<HeartManager> ().GetComponent<HeartManager> ();
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

            if (!moveSound.isPlaying)
                moveSound.Play();
        }
        else if(isDodging == true)
        {
            Debug.Log("isDodging");
            rbody.MovePosition(rbody.position + lastDirection * Time.deltaTime * dodgeSpeed);
        }
        else 
        {
            moveSound.Stop();
            anim.SetBool("IsWalking", false);
        }
    }



    /// <summary>
    /// Toggles whether or not the player is walking or running.
    /// </summary>
    public void ToggleSpeed()
    {
        isRunning = !isRunning;
        if (isRunning)
            moveSound.clip = footsteps[1];
        else
            moveSound.clip = footsteps[0];
    }

    /// <summary>
    /// When the character is hit.
    /// </summary>
    /// 
    public bool TakeDamage(int damage)
    {
        // Don't take damage if player is block or invincible
        if (isBlocking || invincible)
        {
            return false;
        }
        
        currentHealth -= damage;
        // Access heartManager and display the correct number of hearts
		heartManager.DisplayCorrectNumberOfHearts(currentHealth);

        isDead();

		StartCoroutine (Invincibility ());
        
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

        return true;
    }

    public bool ActionTwo()
    {
        //Implement based on equiped item.

        return true;
    }

    public void Dodge()
    {
        //Implement based on current movement direction, and somehow make it over time.
        StartCoroutine(DodgeCO());

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
		heartManager.DisplayCorrectNumberOfHearts(currentHealth); //reset UI hearts to full
        isActing = false;
    }




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bonfire"))
        {
            restedPos = new Vector2(transform.position.x, transform.position.y);
        }
    }


}
