using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolerCreator : MonoBehaviour {

    public List<GameObject> objectsToPool;

    private GameObject newPool;

	void Start ()
    {
	    foreach (GameObject go in objectsToPool)
        {

            newPool = new GameObject ();
            newPool.SetActive(false);
            newPool.AddComponent<ObjectPooler>();
            newPool.GetComponent<ObjectPooler>().pooledObject = go;
            newPool.SetActive(true);
            newPool.name = go.name + "Pooler";
            newPool.transform.parent = this.gameObject.transform;

            Debug.Log("Created " + newPool.name);
        }
	}
	
}
