using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject splashObject;
	public GameObject loadingImage;
	public AudioSource bgmSource;

	void Start() {
		if (!LevelManager.isReset) {
			splashObject.SetActive (true);
			StartCoroutine (HideSplash ());
		} else {
			splashObject.SetActive (false);
		}

		Time.timeScale = 1;
	}

	public void GoToGame() {
		LevelManager.isReset = false;
		loadingImage.SetActive (true);
		bgmSource.mute = true;
		StartCoroutine (StartLoading ());
	}

	public void GoToCredit() {
		Debug.Log ("CREDIT");
	}

	IEnumerator HideSplash() {
		yield return new WaitForSeconds(3);
		splashObject.SetActive (false);
	}

	IEnumerator StartLoading() {
		yield return new WaitForSeconds (1);
		AsyncOperation async = Application.LoadLevelAsync("Main");
		yield return async;
	}
}
