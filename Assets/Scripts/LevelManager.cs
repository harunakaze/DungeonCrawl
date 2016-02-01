using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject loadingMenu;
	public Pauser pauser;
	public static bool isReset = false;

	public void ResetGame() {
		isReset = true;
		pauser.isPaused = true;
		loadingMenu.SetActive (true);
		StartCoroutine (StartLoading ());
	}

	IEnumerator StartLoading() {
		yield return new WaitForSeconds (1);
		AsyncOperation async = Application.LoadLevelAsync("Main");
		yield return async;
	}
}
