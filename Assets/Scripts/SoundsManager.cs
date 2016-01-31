using UnityEngine;
using System.Collections;

public class SoundsManager : MonoBehaviour {

	public bool isMute = false;
	public Animator muteAnimator;

	void Start() {
		isMute = AudioListener.pause;
		muteAnimator.SetBool ("IsMute", AudioListener.pause);
	}
	
	public void ToggleMute() {
		isMute = !isMute;

		AudioListener.pause = isMute;

		muteAnimator.SetBool ("IsMute", isMute);
	}
}
