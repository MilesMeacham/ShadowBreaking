using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    //public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 5f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public int maxSpawn = 7;
	public int deaths = 0;
	public Text victory;


    void Start ()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        // If player is dead
        /*if(playerHealth.currentHealth <= 0f)
        {
            return;
        }*/
		if(maxSpawn == 0)
			return;

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		maxSpawn = maxSpawn - 1;
    }
	
	public void UpdateDeath()
	{
		deaths += 1;
		if(deaths == maxSpawn)
			victory.enabled = true;
	}
}