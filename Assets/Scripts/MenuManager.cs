﻿using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject splashObject;
	public GameObject loadingImage;

	void Start() {
		if (!LevelManager.isReset) {
			StartCoroutine (HideSplash ());
		} else {
			splashObject.SetActive (false);
		}
	}

	public void GoToGame() {
		LevelManager.isReset = false;
		loadingImage.SetActive (true);
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