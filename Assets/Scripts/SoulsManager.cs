using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoulsManager : MonoBehaviour {

	public int soulsDied = 0;
	public Text soulsText;
	public Text victoryText;

	void Start() {
		victoryText.text = "No souls are lost.";
	}

	public void PlayerDie() {
		soulsDied++;
		UpdateAllText ();
	}

	void UpdateAllText() {
		UpdateText ();
		UpdateVictoryText ();
	}

	void UpdateText() {
		soulsText.text = "x " + soulsDied.ToString ();
	}

	void UpdateVictoryText() {
		victoryText.text = "Pray for the\n" + soulsDied.ToString () + "\n souls.";
	}
}
