using UnityEngine;
using System.Collections;

public class SymbolController : MonoBehaviour {

	public GameObject symbols;
	public GameObject baloon;

	private GameObject arrowInstance;
	private GameObject balloonInstance;

	public Transform playerPos;

	private AudioSource audioSource;

	void Start() {
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		audioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (playerPos.position.y >= 5.62f) { //Atas
				arrowInstance = Instantiate(symbols, new Vector2(transform.position.x, transform.position.y - 0.568f), Quaternion.identity) as GameObject;
				balloonInstance = Instantiate(baloon, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.AngleAxis(180, Vector3.forward)) as GameObject;
			}
			else { //Biasa
				arrowInstance = Instantiate(symbols, new Vector2(transform.position.x, transform.position.y + 1.422f), Quaternion.identity) as GameObject;
				balloonInstance = Instantiate(baloon, new Vector2(transform.position.x, transform.position.y + 1.35f), Quaternion.identity) as GameObject;
			}

			audioSource.Play();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			Destroy(arrowInstance.gameObject);
			Destroy(balloonInstance.gameObject);
		}
	}
}
