﻿using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count
	{
		public int minimum;             //Minimum value for our Count class.
		public int maximum;             //Maximum value for our Count class.
		
		
		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
	
	
	public int columns = 8;                                         //Number of columns in our game board.
	public int rows = 8;                                            //Number of rows in our game board.
	public float offsetX = 0.9f;
	public float offsetY = 0.9f;
	public int dangerTileCount = 3;
	public Count wallCount = new Count (5, 9);                      //Lower and upper limit for our random number of walls per level.
	public Count foodCount = new Count (1, 5);                      //Lower and upper limit for our random number of food items per level.
	public GameObject exit;                                         //Prefab to spawn for exit.
	public GameObject player;
	public GameObject[] floorTiles;                                 //Array of floor prefabs.
	public GameObject[] wallTiles;                                  //Array of wall prefabs.
	public GameObject[] foodTiles;                                  //Array of food prefabs.
	public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
	public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
	
	private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

	private float exitX, exitY;
	public GameObject debugTile;
	
	
	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();
		
		//Loop through x axis (columns).
		for(float x = -0.9884f; x < columns + 1; x += offsetX)
		{
			//Within each column, loop through y axis (rows).
			for(float y = -1.7126f; y < rows + 1; y += offsetY)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3(x, y, 0f));
			}
		}
	}
	
	
	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;
		
		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for(float x = -0.9884f; x < columns + 1; x += offsetX)
		{
			//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for(float y = -1.7126f; y < rows + 1; y += offsetY)
			{
				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
				GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];


				//Debug.Log("Luar " + y);
				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				if(x == -1 || x >= columns || y == -1 || y >= rows)
				{
					//Debug.Log(y);
					//toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
				}
				
				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance =
					Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				
				//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
				instance.transform.SetParent (boardHolder);
			}
		}
	}
	
	
	//RandomPosition returns a random position from our list gridPositions.
	Vector3 RandomPosition ()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (0, gridPositions.Count);
		
		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions[randomIndex];
		
		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt (randomIndex);
		
		//Return the randomly selected Vector3 position.
		return randomPosition;
	}
	
	
	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range (minimum, maximum+1);
		
		//Instantiate objects until the randomly chosen limit objectCount is reached
		for(int i = 0; i < objectCount; i++)
		{
			//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
			Vector3 randomPosition = RandomPosition();
			
			//Choose a random tile from tileArray and assign it to tileChoice
			GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
			
			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
	}

	Vector3 RandomExitPosition() 
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range (gridPositions.Count - 3, gridPositions.Count);
		//int theX = Random.Range (-1, 6);
		//int theY = Random.Range (5, 8);

		//int randomIndex = gridPositions.FindIndex (a => a.x == theX && a.y == theY);
		
		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions[randomIndex];

		exitX = randomPosition.x;
		exitY = randomPosition.y;
		
		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt (randomIndex);
		
		//Return the randomly selected Vector3 position.
		return randomPosition;
	}

	void LayoutExitAtRandom (GameObject tileArray, int minimum, int maximum)
	{
		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range (minimum, maximum+1);
		
		//Instantiate objects until the randomly chosen limit objectCount is reached
		for(int i = 0; i < objectCount; i++)
		{
			//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
			Vector3 randomPosition = RandomExitPosition();
			
			//Choose a random tile from tileArray and assign it to tileChoice
			//GameObject tileChoice = tileArray;
			
			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
			Instantiate(tileArray, randomPosition, Quaternion.identity);
		}
	}

	void MakeSureThereIsAtLeastAWay() 
	{
		float step;
		float dx, dy, x_inc, y_inc;
		float x, y;

		dx = exitX - (offsetX - 1 - 1);
		dy = exitY - (offsetY - 1 - 1);
		x = (offsetX - 1 - 1);
		y = (offsetY - 1 - 1);
		
		if (Mathf.Abs(dx) > Mathf.Abs(dy)) {
			step = (int) Mathf.Round(Math.Abs(dx));
		} else {
			step = (int) Mathf.Round(Math.Abs(dy));
		}

		x_inc = dx / step;
		y_inc = dy / step;
		
		for (int i = 1; i <= step; i++) {
			x += x_inc;
			y += y_inc;

			int index = getIndexAtGrid((int)x, (int)y);

			if(index != -1)
			{
				float theX = gridPositions[index].x;
				float theY = gridPositions[index].y;
				Instantiate(debugTile, new Vector3(theX, theY), Quaternion.identity);

				gridPositions.RemoveAt(index);
			}

			int safeIndex = getIndexAtGrid((int)x, (int)y - 1);

			if(safeIndex != -1)
			{
				float theX = gridPositions[safeIndex].x;
				float theY = gridPositions[safeIndex].y;
				Instantiate(debugTile, new Vector3(theX, theY), Quaternion.identity);

				gridPositions.RemoveAt(safeIndex);
			}
		}
	}

	bool atRange(float a, float b)
	{
		const float range = 0.00000000000001f;

		return (b >= a - range) || (b <= a + range) || a == b;
	}

	int getIndexAtGrid(float x, float y) 
	{
		//Vector3 search = new Vector3 (x, y);

		if (x == exitX && y == exitY)
			return -1;

//		if (x == (offsetX - 1 - 1) && y == (offsetY - 1 - 1))
//			return -1;

		return gridPositions.FindIndex (a => atRange(a.x, x) && atRange(a.y, y));
	}
	
	
	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		//Creates the outer walls and floor.
		BoardSetup ();
		
		//Reset our list of gridpositions.
		InitialiseList ();
		gridPositions.RemoveAt (0); //Remove player positions
		
		//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
		//LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		
		//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
		//LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		
		//Determine number of enemies based on current level number, based on a logarithmic progression
		//int enemyCount = (int)Mathf.Log(level, 2f);
		int enemyCount = dangerTileCount;

		LayoutExitAtRandom (exit, 1, 1);

		//MakeSureThereIsAtLeastAWay ();
		
		//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		
		//Instantiate the exit tile in the upper right hand corner of our game board
		Instantiate (player, new Vector3 (-0.9884f, -1.7126f), Quaternion.identity);
	}
}