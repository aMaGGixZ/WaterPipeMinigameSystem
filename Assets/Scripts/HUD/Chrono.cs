using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour {

	Text txt;
	float time;
	bool rollin;

	void Start () {
		txt = GetComponent<Text> ();
		time = GameManager.st.delayStartTime;
	}

	void Update () {
		time -= Time.deltaTime;
		if (time <= 0) {
			txt.text = "";
			this.enabled = false;
		} else {
			txt.text = time.ToString ("0");
		}
	}
}
