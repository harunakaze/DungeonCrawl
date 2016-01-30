using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	public GameObject PauseObject;
	public GameObject TutorialObject;
	[HideInInspector]
	public bool isPaused;

	void Start() {
		isPaused = true;
	}

	public void TogglePause() {
		PauseObject.SetActive (!PauseObject.activeInHierarchy);
		isPaused = PauseObject.activeInHierarchy;
	}

	public void GoToMainMenu() {
		Application.LoadLevel ("Menu");
	}

	public void ExitTutorial() {
		isPaused = false;
		TutorialObject.SetActive (false);
	}
}
