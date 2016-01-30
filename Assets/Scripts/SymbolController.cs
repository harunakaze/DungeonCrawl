using UnityEngine;
using System.Collections;

public class SymbolController : MonoBehaviour {

	public GameObject symbols;
	private GameObject instance;

	public Transform playerPos;

	private AudioSource audioSource;

	void Start() {
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		audioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (playerPos.position.y >= 5.62f)
				instance = Instantiate(symbols, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
			else
				instance = Instantiate(symbols, new Vector2(transform.position.x, transform.position.y + 1.35f), Quaternion.identity) as GameObject;

			audioSource.Play();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			Destroy(instance.gameObject);
		}
	}
}
