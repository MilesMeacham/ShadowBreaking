using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

	private Character character;

	public int bumpDamage = 10;

	// Use this for initialization
	void Start () 
	{
		character = GetComponent<Character> ();
	}
	
	void OnCollisionEnter2D (Collision2D collider)
	{
		if (collider.gameObject.tag == "Enemy") 
		{
			Debug.Log ("Enemy Detected");
			character.TakeDamage (bumpDamage);
		}

	}
}
