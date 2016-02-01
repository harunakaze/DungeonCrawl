using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour {

	public GameObject trapTile;
	public int trapCount = 9;

	public GameObject exitTile;
	public GameObject fogOfWar;
	public GameObject player;

	public Transform trapHolder;

	public bool enableFog;

	[HideInInspector]
	public Vector2 playerStartPosition;

	private int[][] tileMap = new int[][] {
		new int[5]  { 0, 0, 0, 0, 0 },
		new int[5]  { 0, 0, 0, 0, 0 },
		new int[5]  { 0, 0, 0, 0, 0 },
		new int[5]  { 0, 0, 0, 0, 0 },
		new int[5]  { 0, 0, 0, 0, 0 },
		new int[5]  { 0, 0, 0, 0, 0 },
		new int[5]  { 0, 0, 0, 0, 0 }
	};

	//Use (Y, X) Format to access array, sorry :p
	//1 = Danger
	//2 = Exit
	//3 = Player

	private Transform gelapHolder;
	private int playerXPos;

	void SetRandomPlayer() {
		int xPos = Random.Range (0, 5);
		int yPos = 6;
		
		tileMap [yPos] [xPos] = 3;

		playerXPos = xPos;
	}

	void SetRandomExit() {
		int xPos = Random.Range (0, 5);
		int yPos = Random.Range (0, 3);

		tileMap [yPos] [xPos] = 2;
	}


	void SetRandomTrap() {
		int curTrap = 0;

		if (trapCount > 33) {
			Debug.LogError("Out of bounds!");
			return;
		}

		while (curTrap < trapCount) {

			int xPos = Random.Range (0, 5);
			int yPos = Random.Range (0, 7);

			while(tileMap[yPos][xPos] != 0) {
				xPos = Random.Range (0, 5);
				yPos = Random.Range (0, 7);
			}

			tileMap[yPos][xPos] = 1;

			curTrap++;
		}
	}

	void LayTile() {
		const float increment = 0.9f;
		const float xPos = 0.3f;
		const float yPos = 0.22f;
		float curX = xPos;
		float curY = yPos;


		for (int i = 0; i < 5; i++) {
			for(int j = 6; j >= 0; j--) {
				if(tileMap[j][i] == 1) { //Trap Tile
					GameObject instance = Instantiate(trapTile, new Vector3(curX, curY), Quaternion.identity) as GameObject;
					instance.transform.SetParent(trapHolder);
				}
				else if(tileMap[j][i] == 2) { //Exit Tile
					Instantiate(exitTile, new Vector3(curX, curY), Quaternion.identity);
				}

				curY += increment;
			}

			curY = yPos;

			curX += increment;
		}
	}

	void GenerateMap() {
		SetRandomPlayer ();
		SetRandomExit ();
		SetRandomTrap ();
	}

	bool CheckDFS() {
		Stack searchStack = new Stack ();

		int[][] checkMap = new int[][] {
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 }
		};

		Vector2 titik;
		titik.x = playerXPos;
		titik.y = 6;
		searchStack.Push (titik);

		//Mark player
		checkMap[(int)titik.y][(int)titik.x] = 1;

		while (searchStack.Count != 0) {
			Vector2 curTitik = (Vector2)searchStack.Pop();
			int curTitikData = tileMap[(int)curTitik.y][(int)curTitik.x];

			if(curTitikData == 2) //If exit reached
				return true;
			else {
				if (curTitik.x + 1 >= 0 && curTitik.x + 1 <= 4 && tileMap[(int)curTitik.y][(int)curTitik.x + 1] != 1 && checkMap[(int)curTitik.y][(int)curTitik.x + 1] != 1) { //Kanan 
					searchStack.Push(new Vector2(curTitik.x + 1, curTitik.y));
					checkMap[(int)curTitik.y][(int)curTitik.x + 1] = 1;
				}

				if (curTitik.x - 1 >= 0 && curTitik.x - 1 <= 4 && tileMap[(int)curTitik.y][(int)curTitik.x - 1] != 1 && checkMap[(int)curTitik.y][(int)curTitik.x - 1] != 1) { //Kiri
					searchStack.Push(new Vector2(curTitik.x - 1, curTitik.y));
					checkMap[(int)curTitik.y][(int)curTitik.x - 1] = 1;
				}

				if (curTitik.y + 1 >= 0 && curTitik.y + 1 <= 6 && tileMap[(int)curTitik.y + 1][(int)curTitik.x] != 1 && checkMap[(int)curTitik.y + 1][(int)curTitik.x] != 1) {//Atas
					searchStack.Push(new Vector2(curTitik.x, curTitik.y + 1));
					checkMap[(int)curTitik.y + 1][(int)curTitik.x] = 1;
				}

				if (curTitik.y - 1 >= 0 && curTitik.y - 1 <= 6 && tileMap[(int)curTitik.y - 1][(int)curTitik.x] != 1 && checkMap[(int)curTitik.y - 1][(int)curTitik.x] != 1) { //Bawah
					searchStack.Push(new Vector2(curTitik.x, curTitik.y - 1));
					checkMap[(int)curTitik.y - 1][(int)curTitik.x] = 1;
				}

			}
		}

		ResetTileMap ();
		return false;
	}

	void ResetTileMap() {
		tileMap = new int[][] {
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 },
			new int[5]  { 0, 0, 0, 0, 0 }
		};
	}

	public void SetFogOfWar() {
		if (!enableFog)
			return;

		if (gelapHolder != null)
			Destroy (gelapHolder.gameObject);

		gelapHolder = new GameObject ("FogOfWarHolder").transform;

		const float increment = 0.9f;
		const float xPos = 0.3f;
		const float yPos = 0.22f;
		float curX = xPos;
		float curY = yPos;


		for (int i = 0; i < 5; i++) {
			for(int j = 6; j >= 0; j--) {
				if(tileMap[j][i] != 3) { //Trap Tile
					GameObject instance = Instantiate(fogOfWar, new Vector3(curX, curY), Quaternion.identity) as GameObject;
					instance.transform.SetParent (gelapHolder);
				}
				
				curY += increment;
			}
			
			curY = yPos;
			
			curX += increment;
		}
	}

	void PlacePlayer() {
		const float playerIncrement = 0.9f;
		const float playerOffset = 0.3f;
		float xPos = (playerXPos * playerIncrement) + playerOffset;

		playerStartPosition = new Vector2 (xPos, 0.67f);

		player.transform.position = playerStartPosition;
		player.SetActive (true);
	}

	void Start() {

		//Keep generating map until solveable
		do {
			GenerateMap ();
		} while (!CheckDFS());

		PlacePlayer ();
		LayTile ();
		SetFogOfWar ();
	}
}
