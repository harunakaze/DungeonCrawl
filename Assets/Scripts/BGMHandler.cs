using UnityEngine;
using System.Collections;

public class BGMHandler : MonoBehaviour {
	
	public static BGMHandler bgmHandler;

	void Awake() {
		if (bgmHandler == null) {
			DontDestroyOnLoad (gameObject);
			bgmHandler = this;
		} else if (bgmHandler != this) {
			Destroy(gameObject);
		}
	}
}
