using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject splashObject;

	void Start() {
		if (!LevelManager.isReset) {
			StartCoroutine (HideSplash ());
		} else {
			splashObject.SetActive (false);
		}
	}

	public void GoToGame() {
		LevelManager.isReset = false;
		Application.LoadLevel ("Main");
	}

	public void GoToCredit() {
		Debug.Log ("CREDIT");
	}

	IEnumerator HideSplash() {
		yield return new WaitForSeconds(3);
		splashObject.SetActive (false);
	}
}
