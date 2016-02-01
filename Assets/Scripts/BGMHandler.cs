using UnityEngine;
using System.Collections;

public class BGMHandler : MonoBehaviour {

	void Start() {
		GameObject[] oldBGMManager = GameObject.FindGameObjectsWithTag ("BGMManager");

		if (oldBGMManager.Length > 1)
			Destroy (gameObject);
		else
			DontDestroyOnLoad (gameObject);
	}

	public void DestroySounds() {
		Destroy (gameObject);
	}
}
