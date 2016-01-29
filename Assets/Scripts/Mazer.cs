using UnityEngine;
using System.Collections;

public class Mazer : MonoBehaviour
{
	public GameObject killerTiles;

	ArrayList walls = new ArrayList ();
	ArrayList cells = new ArrayList ();

	private Transform enemyHolder;
	
	void MazeIt ()
	{
		
		// Maze Generator by Charles-William Crete
		// Using Prim's algorithm
		// More info: http://en.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_Prim.27s_algorithm
		// Released on the GPL Lisence (http://www.gnu.org/copyleft/gpl.html)
		int max = 10000;
		int size = 14; // must be odd?
		int sizer = (size - 1) / 2;
		Debug.Log ("size : " + size + " r " + sizer);

		enemyHolder = new GameObject ("EnemyBoard").transform;
		
		for (int x = -1; x <= 8; x++) {
			for (int y = 0; y <= 8; y++) {
				cells.Add (new Cell (x, y));
			}
		}
		

		
		Cell startingCell = getCellAt (0, 0);
		walls.Add (startingCell);
		
		while (true) {
			
			Cell wall = (Cell)walls [Random.Range (0, walls.Count)];
			
			processWall (wall);
			
			
			if (walls.Count <= 0)
				break;
			if (--max < 0)
				break;
			
		}
		
		foreach (Cell cell in cells) {
			GameObject cube = Instantiate(killerTiles) as GameObject;

			cube.transform.SetParent (enemyHolder);
			
			if (cell.wall) {
				cube.transform.position = new Vector3 ((float)cell.x, (float)cell.y, 0);
				
				//cube.transform.localScale = new Vector3 (1, 3, 1);
				
			} else {
				//cube.transform.localScale = new Vector3 (1, 0.1, 1);
			}
			
		}
		
	}
	
	void processWall (Cell cell)
	{
		float x = cell.x;
		float z = cell.y;
		if (cell.from == null) {
			if (Random.Range (0, 2) == 0) {
				x += Random.Range (0, 2) - 1;
			} else {
				z += Random.Range (0, 2) - 1;
			}
		} else {
			
			x += (cell.x - cell.from.x);
			z += (cell.y - cell.from.y);
		}
		Cell next = getCellAt (x, z);
		if (next == null || !next.wall)
			return; 
		cell.wall = false;
		next.wall = false;
		
		
		foreach (Cell process in getWallsAroundCell (next)) {
			process.from = next;
			walls.Add (process);
		}
		walls.Remove (cell);
		
	}
	
	Cell getCellAt (float x, float z)
	{
		foreach (Cell cell in cells) {
			if (cell.x == x && cell.y == z)
				return cell;
		}
		return null;
	}
	
	ArrayList getWallsAroundCell (Cell cell)
	{
		ArrayList near = new ArrayList ();
		ArrayList check = new ArrayList ();
		
		check.Add (getCellAt (cell.x + 1, cell.y));
		check.Add (getCellAt (cell.x - 1, cell.y));
		check.Add (getCellAt (cell.x, cell.y + 1));
		check.Add (getCellAt (cell.x, cell.y - 1));
		
		foreach (Cell checking in check) {
			if (checking != null && checking.wall)
				near.Add (checking);
		}
		return near;
		
	}
	
	
	public class Cell
	{
		public float x { get; set; }
		
		public float y { get; set; }
		
		public bool wall { get; set; }
		
		public Cell from { get; set; }
		
		public Cell (float x, float y)
		{
			this.x = x;
			this.y = y;
			this.wall = true;
		}
		
		
	}
	
	// Use this for initialization
	// Making it cleaner
	void Start ()
	{
		MazeIt ();
	}
}