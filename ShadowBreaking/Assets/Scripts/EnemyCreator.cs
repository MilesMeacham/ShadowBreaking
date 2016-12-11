using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EnemyCreator : MonoBehaviour
{
    public GameObject skeleton;                // The enemy prefab to be spawned.
	public GameObject whiteSkeleton;
	public GameObject sorcerer;
	public GameObject healer;
	private EnemyHealth enemyHealth;
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	private GameObject[] gameObjects;
	private GameObject[] healerObjects;
	public float spawnTime = 5f; 
	public int maxSpawn = 7;
	public int remainingSpawn;
	public int deaths = 0;
	public bool minibossDead = false;
	public bool bossDead = false;

	
    //public ObjectPoolerCreator objectPoolerCreator;  // Assign the ObjectPoolerCreator in the scene to this instead of having to do Gameobject.Find
    //public int skeleton = 1;                // This number is associated with the ObjectsToPool found in the ObjPoolCreator. 1 = Skeleton
    //public ObjectPooler skeletonObjPooler;

    void Start()
    {
        //StartCoroutine(LateStart());
		Spawn();
    }
	
	public void Spawn()
	{
		if(Application.loadedLevelName == "Arena_Scene_Final")
		{
			Debug.Log("In Arena Scene Final");
			remainingSpawn = maxSpawn;
			InvokeRepeating ("ArenaSpawn", spawnTime, spawnTime);
		}
		else
		{
			for(int spawnPointIndex = 0; spawnPointIndex < spawnPoints.Length; spawnPointIndex++)
			{
				if(Application.loadedLevelName == "Ultimate_Forest")
				{
					if(spawnPointIndex == 28 || spawnPointIndex == 29 || spawnPointIndex == 30 || spawnPointIndex == 31)
					{
						Instantiate (whiteSkeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
					}
					//else if for forest miniboss
					else if(spawnPointIndex == 32)
					{
						//Debug.Log("Sorcerer should spawn");
						if(bossDead != true)
						{
							Instantiate (sorcerer, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
						}
					}
					else if(spawnPointIndex == 33 || spawnPointIndex == 34)
					{
						Instantiate (healer, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
					}
					else
					{
						Instantiate (skeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
					}			
				}
				else if(Application.loadedLevelName == "Brady_Cave_Scene" || Application.loadedLevelName == "Cave_Final")
				{
					if(spawnPointIndex == 4 || spawnPointIndex == 7 || spawnPointIndex == 11 || spawnPointIndex == 12 || spawnPointIndex == 15 || spawnPointIndex == 19 || spawnPointIndex == 24 || spawnPointIndex == 26)
					{
						Instantiate (whiteSkeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
					}
					else if(spawnPointIndex == 28 || spawnPointIndex == 29)
					{
						//Debug.Log("Sorcerer should spawn");
						if(minibossDead != true)
						{
							Instantiate (sorcerer, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
						}
					}
					else if(spawnPointIndex == 33 || spawnPointIndex == 34 || spawnPointIndex == 35)
					{
						Instantiate (healer, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
					}
					//else if for knight boss
					else
					{
						Instantiate (skeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
					}	
				}
			}

		}
	}
	
	void ArenaSpawn()
    {
		if(remainingSpawn == 0)
			return;

		Debug.Log("In ArenaSpawn");
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate (skeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		
		remainingSpawn = remainingSpawn - 1;
    }
	
	/*void SpawnBosses(GameObject enemy, int spawnPointIndex)
	{
		
		Instantiate (sorcerer, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	} */

    /// <summary>
    /// Calls the deactivateAllEnemies from the object pooler and the spawns the enemies again.
    /// </summary>
    public void ResetAllEnemies()
    {
		gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");
     
		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			Destroy(gameObjects[i]);
		}
		
		healerObjects = GameObject.FindGameObjectsWithTag("Healer");
		for(var j = 0; j < healerObjects.Length; j++)
		{
			Destroy(healerObjects[j]);
		}
	 
		Spawn();

        //skeletonObjPooler.DeactivateAllEnemies();

        //SpawnEnemies();
    }
	
	public int getDeaths()
	{
		return deaths;
	}
	
	public void addDeath()
	{
		deaths += 1;
	}
	
	public void setBossDeathState(string type, bool state)
	{
		if(type == "miniboss")
			minibossDead = state;
		else if(type == "boss")
			bossDead = state;
	}
	    // Had to implement this Custom LateStart function because I was getting an error because this stuff
    // was getting called before the objectPoolers were created.
    /*IEnumerator LateStart()
    {

        yield return new WaitForSeconds(0.5f);

        // This gets the correct object pooler.
        skeletonObjPooler = objectPoolerCreator.objectPoolers[skeleton].GetComponent<ObjectPooler>();

        SpawnEnemies();
 
	} 
	
	

    public void SpawnEnemies()
    {
        // local Gameobject variable to be used to place enemies
        GameObject _skeleton;

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Gets the skeleton and assigns to _skeleton
            _skeleton = skeletonObjPooler.GetPooledObject();

            // Set position and rotation of _skeleton
            _skeleton.transform.position = spawnPoints[i].position;
            _skeleton.transform.rotation = spawnPoints[i].rotation;

            // Need to set to true because it returns and inactive gameObject
            _skeleton.SetActive(true);
        }
    } */
}