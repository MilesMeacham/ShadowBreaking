using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerCreator : MonoBehaviour {

    public List<GameObject> objectsToPool;
    private GameObject newPool;

    // Access this list to find the object pooler you desire.
    public List<GameObject> objectPoolers;


    /// <summary>
    /// Creates an object pooler for each GameObject found in objectsToPool
    /// </summary>
	void Start ()
    {
        // Clear this to make sure it is empty.
        objectPoolers.Clear();

        // Goes through each object
	    foreach (GameObject go in objectsToPool)
        {
            // Instatiate new gameobject to become an object pooler
            newPool = new GameObject ();
            newPool.SetActive(false);

            // Assign the ObjectPooler script to this new object.
            newPool.AddComponent<ObjectPooler>();
            newPool.GetComponent<ObjectPooler>().pooledObject = go; 
            newPool.SetActive(true);

            // Name and parent the new object.
            newPool.name = go.name + "Pooler";
            newPool.transform.parent = this.gameObject.transform;

            // Add new object pooler to this list.
            objectPoolers.Add(newPool);
        }
	}
	
}
