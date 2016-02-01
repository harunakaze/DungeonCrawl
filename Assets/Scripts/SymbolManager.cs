using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SymbolManager : MonoBehaviour {

	public PlayerController pC;
	public GameObject left;
	public GameObject up;
	public GameObject down;
	public GameObject right;

	public GameObject leaveMessageDialogObject;
	public GameObject showMessageObject;

	public Image selectedImages;
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

	public Transform symbolsHolder;
	
	private Vector3 lastPos;

	private int selectedSymbols = 0; //Not selecting
	
	void RemoveOldSymbols() {
		Vector2 fixedPos = new Vector2 (lastPos.x, lastPos.y - 0.45f);
		Collider2D[] oldSymbols = Physics2D.OverlapPointAll(fixedPos);

		foreach(Collider2D oldSymbol in oldSymbols)
		{
			if (oldSymbol != null) {
				if(oldSymbol.CompareTag("Symbols")) {
					Destroy(oldSymbol.gameObject);
				}
			}
		}
	}

	public void PutObject() {
		if (selectedSymbols == 0)
			return;


		lastPos = pC.lastPosition;

		RemoveOldSymbols ();

		Vector2 fixedPos = new Vector2 (lastPos.x, lastPos.y - 0.45f);
		GameObject instance;

		if (selectedSymbols == 1) { // left
			instance = Instantiate(left, fixedPos, Quaternion.identity) as GameObject;
		} else if (selectedSymbols == 2) { //up
			instance = Instantiate(up, fixedPos, Quaternion.identity) as GameObject;
		} else if (selectedSymbols == 3) { //down
			instance = Instantiate(down, fixedPos, Quaternion.identity) as GameObject;
		} else { // if (selectedSymbols == 4) { //right
			instance = Instantiate(right, fixedPos, Quaternion.identity) as GameObject;
		}

		instance.transform.SetParent (symbolsHolder);

		leaveMessageDialogObject.SetActive (false);
		pC.ResetCondition ();
		ResetUI ();
	}

	void ResetUI() {
		selectedSymbols = 0;

		Color trans = new Color (1, 1, 1, 0);
		selectedImages.color = trans;
	}

	public void ChangeSelected(int index) {
		selectedSymbols = index;

		ChangeSelectedImage ();
	}

	void ChangeSelectedImage() {
		selectedImages.color = Color.white;

		if (selectedSymbols == 1) { //left
			selectedImages.sprite = leftSprite;
		} else if (selectedSymbols == 2) { //up
			selectedImages.sprite = upSprite;
		} else if (selectedSymbols == 3) { //down
			selectedImages.sprite = downSprite;
		} else if (selectedSymbols == 4) { //right
			selectedImages.sprite = rightSprite;
		}
	}

	public void ToggleLeaveMessage() {
		leaveMessageDialogObject.SetActive (!leaveMessageDialogObject.activeInHierarchy);
		showMessageObject.SetActive(!showMessageObject.activeInHierarchy);
	}
}
