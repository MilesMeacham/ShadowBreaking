using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {
    //public int sizeX, sizeY;
    public MazeCell cellPrefab;
    private MazeCell[,] cells;
    public float positionOffset = 0.5f;
    public float spacing = 0.96f;
    public float generationStepDelay = 0.01f;
    public IntVector2 size;
    public MazePassage passagePrefab;

    //public MazeWall wallPrefab;
    public MazeWall NorthWall;
    public MazeWall SouthWall;
    public MazeWall EastWall;
    public MazeWall WestWall;

    // START:
    void Start () {

    }
	
	// UPDATE:
	void Update () {
	
	}

    // GET CELL:
    public MazeCell GetCell(IntVector2 coordinates) {
        return cells[coordinates.x, coordinates.y];
    }

    // GENERATE:
    public IEnumerator GenerateIE() {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.y];

        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    // GENERATE:
    public void Generate() {
        cells = new MazeCell[size.x, size.y];

        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) {
            DoNextGenerationStep(activeCells);
        }
    }

    // CREATE CELL:
    private MazeCell CreateCell(IntVector2 coordinates) {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.y] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.y;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3((coordinates.x * spacing) - size.x * positionOffset + positionOffset, (coordinates.y * spacing) - size.y * positionOffset + positionOffset, 0.0f);
        return newCell;
    }

    // RANDOM COORDINATES:
    public IntVector2 RandomCoordinates {
        get {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.y));
        }
    }

    // CONTAINS COORDINATES:
    public bool ContainsCoordinates(IntVector2 coordinate) {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
    }

    // DO FIRST GENERATION STEP:
    private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}
    
    // DO NEXT GENERATION STEP
	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized) {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

		if (ContainsCoordinates(coordinates)) {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null) {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else {
                CreateWall(currentCell, neighbor, direction);
            }
		}

		else {
            CreateWall(currentCell, null, direction);
		}
	}

    // CREATE PASSAGE:
    private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

    // CREATE WALL:
	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall;
        direction.GetDirection();

        if (direction.GetDirection() == MazeDirection.North) {
            wall = Instantiate(NorthWall) as MazeWall;
            wall.Initialize(cell, otherCell, direction);
        }

        else if (direction.GetDirection() == MazeDirection.South) {
            wall = Instantiate(SouthWall) as MazeWall;
            wall.Initialize(cell, otherCell, direction);
        }

        else if (direction.GetDirection() == MazeDirection.East) {
            wall = Instantiate(EastWall) as MazeWall;
            wall.Initialize(cell, otherCell, direction);
        }

        else if (direction.GetDirection() == MazeDirection.West) {
            wall = Instantiate(WestWall) as MazeWall;
            wall.Initialize(cell, otherCell, direction);
        }

		/*if (otherCell != null) {
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}*/
	}
}