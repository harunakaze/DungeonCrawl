using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour {

	public GameObject dialog;
	public GameObject showButton;

	public void ToggleDialogMessage() {
		dialog.SetActive (!dialog.activeInHierarchy);
		showButton.SetActive(!showButton.activeInHierarchy);
	}
}
