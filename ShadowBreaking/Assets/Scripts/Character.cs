using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class Character : MonoBehaviour {

    
    public Rigidbody2D rbody;
    private bool isWalking = false;
    private bool isRunning = false;
    Animator anim;
    Vector2 restedPos;
    //Lock controller during acting (any state other than the player having direct control is acting.)
    private bool isActing = false;
    private bool isBlocking = false;
    private int currentHealth;

    public int maxHealth = 100;    
    public float walkspeed = 1;
    public float runspeed = 3;

    public AudioClip[] footsteps;
    AudioSource moveSound;
   
    
    void Start () {
        rbody = GetComponentInChildren<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        moveSound = GetComponent<AudioSource> ();
        moveSound.clip = footsteps[0];
    }

	void Update ()
	{

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
        if (isBlocking)
        {
            return false;
        }
        
        currentHealth -= damage;
        isDead();
        
        return true;
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
