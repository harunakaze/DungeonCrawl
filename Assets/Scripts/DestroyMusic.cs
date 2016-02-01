using UnityEngine;
using System.Collections;

public class DestroyMusic : MonoBehaviour {
	public void DestroyBGM() {
		Destroy (BGMHandler.bgmHandler.gameObject);
	}
}
