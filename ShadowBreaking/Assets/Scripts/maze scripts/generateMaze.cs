using UnityEngine;
using System.Collections;

public class generateMaze : MonoBehaviour {
	public int mazeWidth_Y = 50;
	public int mazeLength_X = 50;
    public int LastTileInList = 34;
	public float spacing = 0.96f;

	public GameObject tile_001;
	public GameObject tile_002;
	public GameObject tile_003;
	public GameObject tile_004;
	public GameObject tile_005;
	public GameObject tile_006;
	public GameObject tile_007;
	public GameObject tile_008;
	public GameObject tile_009;
	public GameObject tile_010;
	public GameObject tile_011;
	public GameObject tile_012;
	public GameObject tile_013;
	public GameObject tile_014;
	public GameObject tile_015;
	public GameObject tile_016;
    public GameObject tile_017;
    public GameObject tile_018;
    public GameObject tile_019;
    public GameObject tile_020;
    public GameObject tile_021;
    public GameObject tile_022;
    public GameObject tile_023;
    public GameObject tile_024;
    public GameObject tile_025;
    public GameObject tile_026;
    public GameObject tile_027;
    public GameObject tile_028;
    public GameObject tile_029;
    public GameObject tile_030;
    public GameObject tile_031;
    public GameObject tile_032;
    public GameObject tile_033;
    public GameObject tile_034;
    public GameObject tile_035;

    void Start () {
		GameObject maze = new GameObject("Maze");
		GameObject innerTiles = new GameObject("Inner Tiles");
		GameObject walls = new GameObject("Walls");
		GameObject northWall = new GameObject("North Wall");
		GameObject southWall = new GameObject("South Wall");
		GameObject eastWall = new GameObject("East Wall");
		GameObject westWall = new GameObject("West Wall");

		walls.transform.parent = maze.transform;
		innerTiles.transform.parent = maze.transform;
		northWall.transform.parent = walls.transform;
		southWall.transform.parent = walls.transform;
		eastWall.transform.parent = walls.transform;
		westWall.transform.parent = walls.transform;
           
        for (int y = 0; y < mazeWidth_Y; y++) {
			for (int x = 0; x < mazeLength_X; x++) {
				int rand = Random.Range (0, LastTileInList);
				GameObject tile = tile_001;

				if (rand == 0) { tile = tile_001; }
				else if (rand == 1) { tile = tile_002; }
				else if (rand == 2) { tile = tile_003; }
				else if (rand == 3) { tile = tile_004; }
				else if (rand == 4) { tile = tile_005; }
				else if (rand == 5) { tile = tile_006; }
				else if (rand == 6) { tile = tile_007; }
				else if (rand == 7) { tile = tile_008; }
				else if (rand == 8) { tile = tile_009; }
				else if (rand == 9) { tile = tile_010; }
				else if (rand == 10) { tile = tile_011; }
				else if (rand == 11) { tile = tile_012; }
				else if (rand == 12) { tile = tile_013; }
				else if (rand == 13) { tile = tile_014; }
				else if (rand == 14) { tile = tile_015; }
				else if (rand == 15) { tile = tile_016; }
                else if (rand == 16) { tile = tile_017; }
                else if (rand == 17) { tile = tile_018; }
                else if (rand == 18) { tile = tile_019; }
                else if (rand == 19) { tile = tile_020; }
                else if (rand == 20) { tile = tile_021; }
                else if (rand == 21) { tile = tile_022; }
                else if (rand == 22) { tile = tile_023; }
                else if (rand == 23) { tile = tile_024; }
                else if (rand == 24) { tile = tile_025; }
                else if (rand == 25) { tile = tile_026; }
                else if (rand == 26) { tile = tile_027; }
                else if (rand == 27) { tile = tile_028; }
                else if (rand == 28) { tile = tile_029; }
                else if (rand == 29) { tile = tile_030; }
                else if (rand == 30) { tile = tile_031; }
                else if (rand == 31) { tile = tile_032; }
                else if (rand == 32) { tile = tile_033; }
                else if (rand == 33) { tile = tile_034; }
                else if (rand == 34) { tile = tile_035; }

                //int z_rotation = 0;
                /*rand = Random.Range (0, 3);
				if (rand == 0){ z_rotation = 0; }
				if (rand == 1){ z_rotation = 90; }
				if (rand == 2){ z_rotation = 180; }
				if (rand == 3){ z_rotation = 270; }*/

                GameObject innerTile = Instantiate(tile, new Vector3((x * spacing), (y * spacing), 0), Quaternion.identity) as GameObject;
				innerTile.transform.parent = innerTiles.transform;
			}
		}

		for (int x = 0; x < mazeLength_X; x++) {
			GameObject tile = tile_003;
			GameObject northWallTile = Instantiate(tile, new Vector3((x * spacing), (mazeWidth_Y * spacing), 0), Quaternion.identity) as GameObject; // North Wall

            tile = tile_008;
            GameObject southWallTile = Instantiate(tile, new Vector3((x * spacing), -spacing, 0), Quaternion.identity) as GameObject; // South Wall


            northWallTile.transform.parent = northWall.transform;
			southWallTile.transform.parent = southWall.transform;
		}

		for (int y = 0; y < mazeWidth_Y; y++) {
			GameObject tile = tile_010;
			GameObject eastWallTile = Instantiate(tile, new Vector3((mazeLength_X * spacing), (y * spacing), 0), Quaternion.identity) as GameObject; // East Wall

            tile = tile_006;
            GameObject westWallTile = Instantiate(tile, new Vector3(-spacing, (y * spacing), 0), Quaternion.identity) as GameObject; // West Wall

            eastWallTile.transform.parent = eastWall.transform;
			westWallTile.transform.parent = westWall.transform;
		}
	}
}