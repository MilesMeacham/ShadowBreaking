using UnityEngine;
using System.Collections;

public class MazeManager : MonoBehaviour {
    public Maze mazePrefab;
    private Maze mazeInstance;

    // START:
    void Start () {
        GenerateMaze();
	}
	
	// UPDATE:
	void Update () {
	
	}

    // GENERATE MAZE:
    private void GenerateMaze() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        //StartCoroutine(mazeInstance.GenerateIE());
        mazeInstance.Generate();
    }

    // RESET MAZE:
    private void ResetMaze() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        GenerateMaze();
    }
}
