using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public static bool isReset = false;

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	public void ResetGame() {
		isReset = true;
		Application.LoadLevel (Application.loadedLevel);
	}
}
