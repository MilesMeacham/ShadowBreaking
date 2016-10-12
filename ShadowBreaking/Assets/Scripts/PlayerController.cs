using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Character currentChar;

    //Generic implementation of Health
    private int currentHealth;
    private int maxHealth;
    private int currentStamina;
    private int maxStamina;

    //Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        //Movement Section
        
        
            //Set Walking bool true
        currentChar.Move(movement_vector);
        

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
            //currentChar.onHit(10);
        }


    }
	
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Enemy")
		{
			Debug.Log("Enemy collide");
			EventManager.TriggerEvent("damage");
			
		}
    }
}
