using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 5f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public int maxSpawn = 7;
	public int remainingSpawn;
	public int deaths = 0;
	public Text victory;


    void Start ()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		remainingSpawn = maxSpawn;
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
		if(remainingSpawn == 0)
			return;

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		
		remainingSpawn = remainingSpawn - 1;
    }
	
	public void UpdateDeath()
	{
		deaths += 1;
		if(deaths == maxSpawn)
			victory.enabled = true;
	}
}