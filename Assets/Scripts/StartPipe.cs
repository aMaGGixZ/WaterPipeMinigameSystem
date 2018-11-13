using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPipe : MonoBehaviour {
	public openings[] openingsArray;
	public openings startingOP;
	public Slot assignedSlot;
	openings input;
	Image fillImg;

	float fillAmount;
	float refreshTime;
	bool filled;

	void Start(){
		refreshTime = GameManager.st.refreshTime;
		transform.position = assignedSlot.transform.position;
		assignedSlot.startPipe = this;
		fillImg = transform.GetChild (0).GetChild (0).GetComponent<Image> ();

		StartCoroutine (startRollin ());
	}

	IEnumerator startRollin(){
		yield return new WaitForSeconds (GameManager.st.delayStartTime);
		checkStart ();
	}

	void checkStart(){
		if (startingOP == openings.top || startingOP == openings.right) {
			fillImg.fillOrigin = 1;
		} else {
			fillImg.fillOrigin = 0;
		}
		startFilling ();
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
				checkEnd ();
			}
		}
	}

	void fillImage(){
		fillImg.fillAmount = fillAmount;
	}

	void checkEnd(){
		foreach (openings op in openingsArray) {
			if (op != startingOP) {
				checkNextPipe (op);
			}
		}
	}

	void checkNextPipe(openings exit){
		bool hasExit = false;

		switch (exit) {
		case openings.top:
			if (assignedSlot.top.pipe != null && assignedSlot.top != null) {
				foreach (openings op in assignedSlot.top.pipe.openingsArray) {
					if (op == openings.bottom) {
						assignedSlot.top.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		case openings.bottom:
			if (assignedSlot.bottom.pipe != null && assignedSlot.bottom != null) {
				foreach (openings op in assignedSlot.bottom.pipe.openingsArray) {
					if (op == openings.top) {
						assignedSlot.bottom.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		case openings.right:
			if (assignedSlot.right.pipe != null && assignedSlot.right != null) {
				foreach (openings op in assignedSlot.right.pipe.openingsArray) {
					if (op == openings.left) {
						assignedSlot.right.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		case openings.left:
			if (assignedSlot.left.pipe != null && assignedSlot.left != null) {
				foreach (openings op in assignedSlot.left.pipe.openingsArray) {
					if (op == openings.right) {
						assignedSlot.left.pipe.checkStart (exit);
						hasExit = true;
					}
				}
				if (!hasExit) {
					Debug.Log ("Pierdes");
				}
			} else {
				Debug.Log ("Pierdes");
			}
			break;
		}
	}
}
