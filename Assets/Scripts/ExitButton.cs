using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	public GameObject dialogObject;
	public bool isDialogShown = false;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(!dialogObject.activeInHierarchy)
				ShowDialog();
			else
				CloseDialog();
		}
	}

	public void ShowDialog() {
		isDialogShown = true;
		dialogObject.SetActive (true);
	}

	public void CloseDialog() {
		isDialogShown = false;
		dialogObject.SetActive (false);
	}

	public void ExitGame() {
		Application.Quit();
	}
}
