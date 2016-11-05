using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	public GameObject pooledObject;
	public int pooledAmount = 5;
	List <GameObject> pooledObjects;

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
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = this.gameObject.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
	
    /// <summary>
    /// Returns an object
    /// </summary>
    /// <returns>The pooled object</returns>
	public GameObject GetPooledObject()
    {
		// Look through the existing Objects
		for (int i = 0; i < pooledObjects.Count; i++) {
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

}
