using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerDeathManager : MonoBehaviour {

	private GameObject player;

	private UnityAction deathListener; //this is to test player death
	private SpriteRenderer sprite_renderer;

	Camera mainCamera;


	Vector2 initialPos;
	Quaternion initialRotation;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		initialPos = player.transform.position;
	}

	void Awake ()
	{
		deathListener = new UnityAction (DeathFunctions);
	}
	
	void OnEnable ()
	{
		EventManager.StartListening ("PlayerDead", deathListener);

	}

	void OnDisable ()
	{
		EventManager.StopListening ("PlayerDead", deathListener);
	}

	void DeathFunctions ()
	{
		//ResetPlayerLevel ();
		ResetPlayer ();


		EventManager.TriggerEvent ("Resurrection");
	}


	IEnumerator ResetPlayerLevel()
	{
		Debug.Log("PlayerDead has been triggered");

		float fadeTime = GameObject.Find ("_GM").GetComponent<Fader> ().BeginFade (-1);
		yield return new WaitForSeconds (fadeTime);
		player.transform.position = new Vector2(initialPos.x,initialPos.y);
	}

	void ResetPlayer()
	{
		Debug.Log("PlayerDead has been triggered");

		player.transform.position = new Vector2(initialPos.x,initialPos.y);
	}
}
