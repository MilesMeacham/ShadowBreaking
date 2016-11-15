using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {
    public IntVector2 coordinates;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int initializedEdgeCount;

    // START:
    void Start () { }
	
	// UPDATE:
	void Update () { }

    // GET EDGE:
	public MazeCellEdge GetEdge (MazeDirection direction) {
		return edges[(int)direction];
	}

    // SET EDGE:
	public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
		edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

    // IS FULLY INITIALIZED:
    public bool IsFullyInitialized {
		get {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

    // RANDOM UNINITIALIZED DIRECTION:
    public MazeDirection RandomUninitializedDirection {
		get {
			int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
			for (int i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0) {
						return (MazeDirection)i;
					}
					skips -= 1;
				}
			}
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions remaining.");
        }
	}
}