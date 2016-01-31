using UnityEngine;
using System.Collections;

public class Spiker : MonoBehaviour {

	private Animator animator;
	private AudioSource audioSource;

	void Start() {
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			audioSource.Play();
			animator.SetTrigger("Spike");
		}
	}
}
