using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EnemyCreator : MonoBehaviour
{
    public GameObject skeleton;                // The enemy prefab to be spawned.
	public GameObject whiteSkeleton;
	public GameObject sorcerer;
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	private GameObject[] gameObjects;

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
		for(int spawnPointIndex = 0; spawnPointIndex < spawnPoints.Length; spawnPointIndex++)
		{
			if(Application.loadedLevelName == "Brady_Forest_Sandbox")
			{
				if(spawnPointIndex == 28 || spawnPointIndex == 29 || spawnPointIndex == 30 || spawnPointIndex == 31)
				{
					Instantiate (whiteSkeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				}
				else if(spawnPointIndex == 32)
				{
					//Debug.Log("Sorcerer should spawn");
					Instantiate (sorcerer, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				}
				else
				{
					Instantiate (skeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				}			
			}
			else if(Application.loadedLevelName == "Brady_Cave_Scene")
			{
				if(spawnPointIndex == 4 || spawnPointIndex == 7 || spawnPointIndex == 11 || spawnPointIndex == 12 || spawnPointIndex == 15 || spawnPointIndex == 19 || spawnPointIndex == 24 || spawnPointIndex == 26)
				{
					Instantiate (whiteSkeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				}
				else if(spawnPointIndex == 28 || spawnPointIndex == 29)
				{
					//Debug.Log("Sorcerer should spawn");
					Instantiate (sorcerer, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				}
				else
				{
					Instantiate (skeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				}	
			}
		}

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
	 
		Spawn();

        //skeletonObjPooler.DeactivateAllEnemies();

        //SpawnEnemies();
    }
	
	/*public void ResetEnemies()
	{
		skeletonObjPooler = objectPoolerCreator.objectPoolers[skeleton].GetComponent<ObjectPooler>();
		for (int i = 0; i < spawnPoints.Length; i++)
		{
            // Gets the skeleton and assigns to _skeleton
            _skeleton = skeletonObjPooler.GetPooledObject();

            Debug.Log(skeletonObjPooler.name);

            // Set position and rotation of _skeleton
            _skeleton.transform.position = spawnPoints[i].position;
            _skeleton.transform.rotation = spawnPoints[i].rotation;

            // Need to set to true because it returns and inactive gameObject
            _skeleton.SetActive(true);

            Debug.Log("Found " + _skeleton.name);
            //Instantiate (enemy, spawnPoints[i].position, spawnPoints[i].rotation);
        }
	} */
}