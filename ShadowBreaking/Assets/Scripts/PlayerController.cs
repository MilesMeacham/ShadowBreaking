﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Character currentChar;

    public Canvas mainHUD;
    HUDUI hudController;

    AudioSource walkSound;

    //Generic implementation of Health
    private int currentHealth;
    private int maxHealth;
    //private int currentStamina;
    //private int maxStamina;

	//public Text restedText; 

    //Use this for initialization
    void Start () {
        hudController = mainHUD.GetComponent<HUDUI>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            hudController.Pause();
        if (hudController.gamePaused)
            return;

        
        

        //Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentChar.ToggleSpeed();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentChar.ToggleSpeed();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Dodge.");
            currentChar.Dodge();
        }


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Action One");
            currentChar.ActionOne();
        }

        if (Input.GetMouseButtonDown(1))
        {

            Debug.Log("Action Two");
            currentChar.ActionTwo();
        }

        //Key for Testing Death
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentChar.TakeDamage(110);
        }


    }

    // Use this for any key input that is constantly held down. This will make it so it doesn't change speed
    // with different framerates.
    void FixedUpdate()
    {
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // This prevents double speed when moving diagonal
        movement_vector.Normalize();

        //Set Walking bool true
        currentChar.Move(movement_vector);

    }
	
    /*void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Enemy")
		{
			Debug.Log("Enemy collide");
			EventManager.TriggerEvent("damage");
			
		}
		 else if (col.gameObject.CompareTag("Bonfire"))
        {
            //restedPos = new Vector2(transform.position.x, transform.position.y);
			StartCoroutine(Timer());
        }
    } */
	
	
	/*IEnumerator Timer()
	{
		restedText.enabled = true;
		yield return new WaitForSeconds(3);
		restedText.enabled = false;
	} */
}
