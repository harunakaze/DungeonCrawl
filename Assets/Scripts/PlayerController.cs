﻿using UnityEngine;
using HKExtension;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public TileManager tileManager;

	[HideInInspector]
	public Vector3 lastPosition;

	public GameObject deadButton;
	public GameObject victory;
	public GameObject lastPlaceSouls;

	public SoulsManager soulsManager;

	public Pauser pauser;
	public ExitButton exitButton;

	[HideInInspector]
	public GameObject lastPlaceSoulsInstance;

	private bool isDead = false;
	private bool isWin = false;
	private Animator animator;

	private AudioSource audioSource;

	private float safeMoveOffset = 0.45f;

	void Start() {
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update() {
		if (isDead || pauser.isPaused || isWin || pauser.isTutorial || exitButton.isDialogShown) {
			return;
		}

		if (TouchInput.GetSwap ("Left") || Input.GetButtonDown("Left")) {
			if(transform.position.x > 0.3f + safeMoveOffset) {
				Vector2 newPos = new Vector2(transform.position.x - 0.9f, transform.position.y);
				lastPosition = transform.position;
				transform.position = newPos;

				if(transform.localScale.x == 1)
				{
					Vector2 newScale = new Vector2(-1, 1);
					transform.localScale = newScale;
				}

				audioSource.Play();
			}
		}

		if (TouchInput.GetSwap ("Right") || Input.GetButtonDown("Right")) {
			if(transform.position.x < 3.9f - safeMoveOffset) {
				Vector2 newPos = new Vector2(transform.position.x + 0.9f, transform.position.y);
				lastPosition = transform.position;
				transform.position = newPos;

				if(transform.localScale.x == -1)
				{
					Vector2 newScale = new Vector2(1, 1);
					transform.localScale = newScale;
				}

				audioSource.Play();
			}
		}

		if (TouchInput.GetSwap ("Up") || Input.GetButtonDown("Up")) {
			if(transform.position.y < 6.07f - safeMoveOffset) { //6.07f player pos on top
				Vector2 newPos = new Vector2(transform.position.x, transform.position.y + 0.9f);
				lastPosition = transform.position;
				transform.position = newPos;

				audioSource.Play();
			}
		}

		if (TouchInput.GetSwap ("Down") || Input.GetButtonDown("Down")) {
			if(transform.position.y > 0.67f + safeMoveOffset) {
				Vector2 newPos = new Vector2(transform.position.x, transform.position.y - 0.9f);
				lastPosition = transform.position;
				transform.position = newPos;

				audioSource.Play();
			}
		}
	}

	void Dead() {
		deadButton.SetActive (true);
	}

	public void ResetCondition() {
		tileManager.SetFogOfWar ();
		
		//Vector2 startPosition = new Vector2 (0.3f, 0.67f);
		
		transform.position = tileManager.playerStartPosition;

		isDead = false;

		animator.SetBool ("IsDead", false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("FogOfWar")) {
			Destroy(other.gameObject);
		}

		if (other.CompareTag ("Trap")) {
			StartCoroutine(DeadStart());
		}

		if (other.CompareTag ("ExitDoor")) {
			StartCoroutine(WinStart());
		}
	}

	void PutLastSouls() {
		lastPlaceSoulsInstance = Instantiate (lastPlaceSouls, lastPosition, Quaternion.identity) as GameObject;
	}

	public void DeleteLastSouls() {
		Destroy (lastPlaceSoulsInstance);
	}

	IEnumerator DeadStart() {
		soulsManager.PlayerDie ();
		PutLastSouls ();

		isDead = true;

		Vector2 deadPos = new Vector2(transform.position.x, transform.position.y - 0.334f);
		transform.position = deadPos;

		animator.SetBool ("IsDead", true);

		yield return new WaitForSeconds(2.32f);

		Dead();
	}

	IEnumerator WinStart() {
		isWin = true;
		yield return new WaitForSeconds(2.32f);
		victory.SetActive (true);
	}
}
