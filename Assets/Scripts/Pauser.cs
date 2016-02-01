using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	public GameObject pauseObject;
	public GameObject tutorialObject;
	public GameObject loadingObject;
	public bool enableTutorial = true;
	[HideInInspector]
	public bool isPaused;
	[HideInInspector]
	public bool isTutorial;

	void Start() {
		isPaused = true;

		//So you not forget :v
		if (enableTutorial) {
			tutorialObject.SetActive(true);
		}

		if (LevelManager.isReset || !enableTutorial) {
			ExitTutorial();
		}

		Time.timeScale = 1;
	}

	public void TogglePause() {
		pauseObject.SetActive (!pauseObject.activeInHierarchy);
		isPaused = pauseObject.activeInHierarchy;

		if (isPaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void GoToMainMenu() {
		Time.timeScale = 1;
		LevelManager.isReset = true;
		loadingObject.SetActive (true);
		StartCoroutine (StartLoading ());
	}

	public void ExitTutorial() {
		isPaused = false;
		isTutorial = false;
		tutorialObject.SetActive (false);
	}

	IEnumerator StartLoading() {
		yield return new WaitForSeconds (1);
		AsyncOperation async = Application.LoadLevelAsync("Menu");
		yield return async;
	}
}
