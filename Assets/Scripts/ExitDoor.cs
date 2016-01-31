using UnityEngine;
using System.Collections;

public class ExitDoor : MonoBehaviour {

	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			audioSource.Play();
		}
	}
}
