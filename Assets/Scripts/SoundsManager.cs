using UnityEngine;
using System.Collections;

public class SoundsManager : MonoBehaviour {

	public bool isMute = false;
	public Animator muteAnimator;

	void Start() {
		AudioListener.pause = false;
		muteAnimator.SetBool ("IsMute", false);
	}
	
	public void ToggleMute() {
		isMute = !isMute;

		AudioListener.pause = isMute;

		muteAnimator.SetBool ("IsMute", isMute);
	}
}
