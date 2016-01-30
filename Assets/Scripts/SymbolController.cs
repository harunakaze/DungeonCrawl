using UnityEngine;
using System.Collections;

public class SymbolController : MonoBehaviour {

	public GameObject symbols;
	private GameObject instance;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			instance = Instantiate(symbols, new Vector2(transform.position.x, transform.position.y + 1.35f), Quaternion.identity) as GameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			Destroy(instance.gameObject);
		}
	}
}
