using UnityEngine;
using System.Collections;

public class BGMHandler : MonoBehaviour {

	void Start() {
		DontDestroyOnLoad (gameObject);
	}

	public void DestroySounds() {
		Destroy (gameObject);
	}
}
