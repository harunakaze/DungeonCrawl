using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	public GameObject PauseObject;
	[HideInInspector]
	public bool isPaused;

	public void TogglePause() {
		PauseObject.SetActive (!PauseObject.activeInHierarchy);
		isPaused = PauseObject.activeInHierarchy;
	}

	public void GoToMainMenu() {
		Debug.Log ("MENU");
	}
}
