    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pipe : MonoBehaviour, IDropHandler {
	public openings[] openingsArray;
	public bool moveable = true;
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
		currentSlot.pipe = this;
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
		} else if (fillImg.fillMethod == Image.FillMethod.Radial90) {
			switch (fillImg.fillOrigin) {
			case 0: // BOTTOM LEFT
				if (input == openings.bottom) {
					fillImg.fillClockwise = false;
				} else {
					fillImg.fillClockwise = true;
				}
				break;
			case 1: // TOP LEFT
				if (input == openings.left) {
					fillImg.fillClockwise = false;
				} else {
					fillImg.fillClockwise = true;
				}
				break;
			case 2: // TOP RIGHT
				if (input == openings.top) {
					fillImg.fillClockwise = false;
				} else {
					fillImg.fillClockwise = true;
				}
				break;
			case 3: // BOTTOM RIGHT
				if (input == openings.right) {
					fillImg.fillClockwise = false;
				} else {
					fillImg.fillClockwise = true;
				}
				break;
			}
		}
	}

	void startFilling () {
		fillAmount = 0;
		moveable = false;
		GetComponent<DragHandler> ().enabled = false;
		StartCoroutine (fill (GameManager.st.fillDuration));
	}

	IEnumerator fill (float duration) {
		while (!filled) {
			fillAmount += refreshTime/duration;
			fillImage ();
			yield return new WaitForSeconds (refreshTime);
			if (fillAmount >= 1) {
				filled = true;
				checkEnd ();
			}
		}
	}

	void fillImage(){
		fillImg.fillAmount = fillAmount;
	}

	void checkEnd(){
		foreach (openings op in openingsArray) {
			if (op != input) {
				checkNextPipe (op);
			}
		}
	}

	void checkNextPipe(openings exit){
		bool hasExit = false;

		switch (exit) {
		case openings.top:
			if (currentSlot.top != null && currentSlot.top.pipe != null && !currentSlot.top.pipe.filled) {
				foreach (openings op in currentSlot.top.pipe.openingsArray) {
					if (op == openings.bottom) {
						currentSlot.top.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else if (currentSlot.top.winPipe != null) {
				currentSlot.top.winPipe.checkStart (exit);
				hasExit = true;
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		case openings.bottom:
 			if (currentSlot.bottom != null && currentSlot.bottom.pipe != null && !currentSlot.bottom.pipe.filled) {
				foreach (openings op in currentSlot.bottom.pipe.openingsArray) {
					if (op == openings.top) {
						currentSlot.bottom.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else if (currentSlot.bottom.winPipe != null) {
				currentSlot.bottom.winPipe.checkStart (exit);
				hasExit = true;
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		case openings.right:
			if (currentSlot.right != null && currentSlot.right.pipe != null && !currentSlot.right.pipe.filled) {
				foreach (openings op in currentSlot.right.pipe.openingsArray) {
					if (op == openings.left) {
						currentSlot.right.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else if (currentSlot.right.winPipe != null) {
				currentSlot.right.winPipe.checkStart (exit);
				hasExit = true;
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		case openings.left:
			if (currentSlot.left != null && currentSlot.left.pipe != null && !currentSlot.left.pipe.filled) {
				foreach (openings op in currentSlot.left.pipe.openingsArray) {
					if (op == openings.right) {
						currentSlot.left.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else if (currentSlot.left.winPipe != null) {
				currentSlot.left.winPipe.checkStart (exit);
				hasExit = true;
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		}
	}
		
	public void changeSlot(Slot slt){
		if (currentSlot) {
			currentSlot.pipe = null;
		}

		currentSlot = slt;
		if (slt != null) {
			slt.pipe = this;
			transform.position = slt.transform.position;
		}
	}
		
	public void OnDrop (PointerEventData eventData)
	{
		Slot thisSlot = currentSlot;

		changeSlot (GameManager.pickedItem.GetComponent<DragHandler> ().startSlot);
		GameManager.pickedItem.GetComponent<Pipe> ().changeSlot (thisSlot);
	}
}
