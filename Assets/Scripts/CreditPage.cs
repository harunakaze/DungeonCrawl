using UnityEngine;
using System.Collections;

public class CreditPage : MonoBehaviour {
	
	public void ToggleCredit() {
		gameObject.SetActive (!gameObject.activeInHierarchy);
	}
}
