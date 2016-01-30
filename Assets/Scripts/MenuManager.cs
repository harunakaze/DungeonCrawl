using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject splashObject;

	void Start() {
		StartCoroutine (HideSplash ());
	}

	public void GoToGame() {
		Application.LoadLevel ("Main");
	}

	public void GoToCredit() {
		Debug.Log ("CREDIT");
	}

	IEnumerator HideSplash() {
		yield return new WaitForSeconds(3);
		splashObject.SetActive (false);
	}
}
