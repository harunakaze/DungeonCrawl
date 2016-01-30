using UnityEngine;
using System.Collections;

public class Spiker : MonoBehaviour {

	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			Debug.Log("SPIKEIT");
			animator.SetTrigger("Spike");
		}
	}
}
