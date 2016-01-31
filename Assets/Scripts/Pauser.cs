using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	public GameObject PauseObject;
	public GameObject TutorialObject;
	public GameObject LoadingObject;
	[HideInInspector]
	public bool isPaused;

	void Start() {
		isPaused = true;

		if (LevelManager.isReset) {
			ExitTutorial();
		}
	}

	public void TogglePause() {
		PauseObject.SetActive (!PauseObject.activeInHierarchy);
		isPaused = PauseObject.activeInHierarchy;
	}

	public void GoToMainMenu() {
		LevelManager.isReset = true;
		LoadingObject.SetActive (true);
		StartCoroutine (StartLoading ());
	}

	public void ExitTutorial() {
		isPaused = false;
		TutorialObject.SetActive (false);
	}

	IEnumerator StartLoading() {
		yield return new WaitForSeconds (1);
		AsyncOperation async = Application.LoadLevelAsync("Menu");
		yield return async;
	}
}
