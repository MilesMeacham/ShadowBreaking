// Author: Miles Meacham
// Purpose: Makes game more effecient by not instantiating and destroying objects that will be reused.


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public GameObject pooledObject;
	public int pooledAmount = 10;
	List <GameObject> pooledObjects;

    /// <summary>
    /// Creates a list to store the game objects and then calls the InstantiateMultipleObjects Function
    /// </summary>
	void Start ()
    {
		// Create a new list of GameObjects
		pooledObjects = new List<GameObject> ();

        InstantiateMultipleObjects();
	}
        

    /// <summary>
    /// Instatiate the objects into the scene but set them to inactive
    /// </summary>
    public void InstantiateMultipleObjects()
    {
        // Create as many objects as set in pooledAmount
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = this.gameObject.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
	
    /// <summary>
    /// Returns an object by finding an inactive one or if all are active then it instantiates a new one
    /// </summary>
    /// <returns>The pooled object</returns>
	public GameObject GetPooledObject()
    {
		// Look through the existing Objects
		for (int i = 0; i < pooledObjects.Count; i++)
        {
			// If one of the desired objects is inactive, use that one
			if(!pooledObjects[i].activeInHierarchy)
            {
				// Return the desired object
				return pooledObjects[i];
			}
		}
	
		// If a desired object was not found Inactive
		// Create the object and add it to the pooledObjects list
		GameObject obj = (GameObject)Instantiate(pooledObject);
		obj.transform.parent = this.gameObject.transform;
		obj.SetActive (false);
		pooledObjects.Add (obj);

		return obj;
	}

    /// <summary>
    /// Call this function to deactivate all the active enemies
    /// </summary>
    public void DeactivateAllEnemies()
    {
        // Look through the existing Objects
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // If one of the desired objects is inactive, use that one
            if (pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(false);
            }
        }
    }

}
