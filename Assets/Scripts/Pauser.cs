using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	public GameObject PauseObject;
	public GameObject TutorialObject;
	public GameObject LoadingObject;
	public bool isTutorial;
	[HideInInspector]
	public bool isPaused;

	void Start() {
		isPaused = true;

		if (LevelManager.isReset) {
			ExitTutorial();
		}

		Time.timeScale = 1;
	}

	public void TogglePause() {
		PauseObject.SetActive (!PauseObject.activeInHierarchy);
		isPaused = PauseObject.activeInHierarchy;

		if (isPaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void GoToMainMenu() {
		Time.timeScale = 1;
		LevelManager.isReset = true;
		LoadingObject.SetActive (true);
		StartCoroutine (StartLoading ());
	}

	public void ExitTutorial() {
		isPaused = false;
		isTutorial = false;
		TutorialObject.SetActive (false);
	}

	IEnumerator StartLoading() {
		yield return new WaitForSeconds (1);
		AsyncOperation async = Application.LoadLevelAsync("Menu");
		yield return async;
	}
}
