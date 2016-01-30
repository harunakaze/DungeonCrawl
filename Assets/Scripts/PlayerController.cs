using UnityEngine;
using HKExtension;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public TileManager tileManager;

	[HideInInspector]
	public Vector3 lastPosition;

	public GameObject deadButton;
	public GameObject victory;

	private bool isDead = false;

	void Update() {
		if (isDead) {
			return;
		}

		if (TouchInput.GetSwap ("Left")) {
			if(transform.position.x > 0.3f) {
				Vector2 newPos = new Vector2(transform.position.x - 0.9f, transform.position.y);
				lastPosition = transform.position;
				transform.position = newPos;

				if(transform.localScale.x == 1)
				{
					Vector2 newScale = new Vector2(-1, 1);
					transform.localScale = newScale;
				}
			}
		}

		if (TouchInput.GetSwap ("Right")) {
			if(transform.position.x < 3.9f) {
				Vector2 newPos = new Vector2(transform.position.x + 0.9f, transform.position.y);
				lastPosition = transform.position;
				transform.position = newPos;

				if(transform.localScale.x == -1)
				{
					Vector2 newScale = new Vector2(1, 1);
					transform.localScale = newScale;
				}
			}
		}

		if (TouchInput.GetSwap ("Up")) {
			if(transform.position.y < 5.62f) {
				Vector2 newPos = new Vector2(transform.position.x, transform.position.y + 0.9f);
				lastPosition = transform.position;
				transform.position = newPos;
			}
		}

		if (TouchInput.GetSwap ("Down")) {
			if(transform.position.y > 0.67f) {
				Vector2 newPos = new Vector2(transform.position.x, transform.position.y - 0.9f);
				lastPosition = transform.position;
				transform.position = newPos;
			}
		}
	}

	void Dead() {
		isDead = true;
		deadButton.SetActive (true);
	}

	public void ResetCondition() {
		tileManager.SetFogOfWar ();
		
		Vector2 startPosition = new Vector2 (0.3f, 0.67f);
		
		transform.position = startPosition;

		isDead = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("FogOfWar")) {
			Destroy(other.gameObject);
		}

		if (other.CompareTag ("Trap")) {
			Dead();
		}

		if (other.CompareTag ("ExitDoor")) {
			StartCoroutine(WinStart());
		}
	}

	IEnumerator WinStart() {
		victory.SetActive (true);

		yield return new WaitForSeconds(2);

		Application.LoadLevel(Application.loadedLevel);
	}
}
