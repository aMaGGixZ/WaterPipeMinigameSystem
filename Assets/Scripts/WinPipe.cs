using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPipe : MonoBehaviour {

	public openings[] openingsArray;
	public Slot currentSlot;
	openings input;
	Image fillImg;

	float fillAmount;
	float refreshTime;
	bool filled;

	void Start(){
		refreshTime = GameManager.st.refreshTime;
		fillImg = transform.GetChild (0).GetChild (0).GetComponent<Image> ();

		transform.position = currentSlot.transform.position;
		currentSlot.winPipe = this;
	}

	public void checkStart(openings income){
		switch (income) {
		case openings.top:
			input = openings.bottom;
			break;
		case openings.bottom:
			input = openings.top;
			break;
		case openings.right:
			input = openings.left;
			break;
		case openings.left:
			input = openings.right;
			break;
		}
		checkFillOrigin ();
		startFilling ();
	}

	void checkFillOrigin(){
		if (fillImg.fillMethod == Image.FillMethod.Horizontal || fillImg.fillMethod == Image.FillMethod.Vertical) {
			if (input == openings.top || input == openings.right) {
				fillImg.fillOrigin = 1;
			} else {
				fillImg.fillOrigin = 0;
			}
		}
	}

	void startFilling () {
		fillAmount = 0;
		StartCoroutine (fill (GameManager.st.fillDuration));
	}

	IEnumerator fill (float duration) {
		while (!filled) {
			fillAmount += refreshTime/duration;
			fillImage ();
			yield return new WaitForSeconds (refreshTime);
			if (fillAmount >= 1) {
				filled = true;
				Debug.Log ("Ganas");
			}
		}
	}

	void fillImage(){
		fillImg.fillAmount = fillAmount;
	}
}
