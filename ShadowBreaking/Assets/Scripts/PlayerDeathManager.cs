using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;


public class PlayerDeathManager : MonoBehaviour {

	//Needs to be added to Game Manager _GM

	public Character player;
	public Text deathText;
	private UnityAction deathListener; //this is the action that will be triggered if PlayerDead has been triggered
	private SpriteRenderer sprite_renderer;
    private EnemyCreator enemyCreator;

	Camera mainCamera;


	Vector2 initialPos;
	Quaternion initialRotation;

	void Start ()
	{
		 //this will find the Player object
		initialPos = player.rbody.transform.position; //sets the initial position of the player when the scene starts

        enemyCreator = GameObject.Find("EnemyManager").GetComponent<EnemyCreator>();
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
		ResetPlayer ();
		
		//timing function
		StartCoroutine(Timer());

		EventManager.TriggerEvent ("Resurrection");
	}


	void ResetPlayer() // player dies and player position is set to its initial position.
	{
		Debug.Log("PlayerDead has been triggered");

        enemyCreator.ResetAllEnemies();

        if (player.restedPos.x != 0 && player.restedPos.y != 0)
		{
			player.rbody.transform.position = new Vector2(player.restedPos.x, player.restedPos.y);
		}
		else{
			player.rbody.transform.position = new Vector2(initialPos.x,initialPos.y);
		}
        player.Resurrection();
        
	}
	
	IEnumerator Timer()
	{
		deathText.enabled = true;
		yield return new WaitForSeconds(3);
		deathText.enabled = false;
	}
}
