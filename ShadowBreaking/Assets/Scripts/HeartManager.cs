using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeartManager : MonoBehaviour {

    public List<GameObject> hearts;
    int healthPerHeart = 20;

    // This will probably be handled by the health script. Need to get together with whoever is programming that and work this out.
    public int startingHealth = 100;

	private Character health;

	void Start ()
	{
		
        health = GameObject.Find ("Player_New").GetComponent<Character> ();
		startingHealth = health.maxHealth;
		DisplayCorrectNumberOfHearts (startingHealth);
	}


	public void DisplayCorrectNumberOfHearts (int currentHealth)
    {
        // Turn on the correct number of hearts at the beggining of the game
	    foreach (GameObject heart in hearts)
        {
			if (currentHealth >= healthPerHeart) 
			{
				heart.GetComponent<UIHearts> ().DisplayFullHeart ();
				currentHealth -= healthPerHeart;
			} 
			else if (currentHealth > 0 && currentHealth < healthPerHeart) 
			{
				heart.GetComponent<UIHearts> ().DisplayHalfHeart ();
				currentHealth -= healthPerHeart;
			}
			else
				heart.GetComponent<UIHearts> ().DisplayEmptyHeart ();
        }
	}
}
