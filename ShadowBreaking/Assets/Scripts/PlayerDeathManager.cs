using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerDeathManager : MonoBehaviour {

	//Needs to be added to Game Manager _GM


	private GameObject player;
	private UnityAction deathListener; //this is the action that will be triggered if PlayerDead has been triggered
	private SpriteRenderer sprite_renderer;

	Camera mainCamera;


	Vector2 initialPos;
	Quaternion initialRotation;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player"); //this will find the Player object
		initialPos = player.transform.position; //sets the initial position of the player when the scene starts
	}

	void Awake ()
	{
		deathListener = new UnityAction (DeathFunctions); //DeathFunctions function will be called when deathListener is triggered
	}
	
	void OnEnable ()
	{
		EventManager.StartListening ("PlayerDead", deathListener); //start listening for PlayerDead

	}

	void OnDisable ()
	{
		EventManager.StopListening ("PlayerDead", deathListener); //stop listening for PlayerDead
	}

	void DeathFunctions () // contains functions that will be called when the player dies
	{
		//ResetPlayerLevel ();
		ResetPlayer ();


		EventManager.TriggerEvent ("Resurrection");
	}


	IEnumerator ResetPlayerLevel() // trying to get fading to work when player dies then "resurrects"
	{
		Debug.Log("PlayerDead has been triggered");

		float fadeTime = GameObject.Find ("_GM").GetComponent<Fader> ().BeginFade (-1);
		yield return new WaitForSeconds (fadeTime);
		player.transform.position = new Vector2(initialPos.x,initialPos.y);
	}

	void ResetPlayer() // player dies and player position is set to its initial position.
	{
		Debug.Log("PlayerDead has been triggered");

		player.transform.position = new Vector2(initialPos.x,initialPos.y);
	}
}
