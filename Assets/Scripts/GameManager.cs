using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum openings {top,bottom,right,left}
public class GameManager : MonoBehaviour {
	public static GameManager st;
	public static GameObject pickedItem;

	public float fillDuration;
	public float refreshTime;
	public float delayStartTime;

	void Awake () {
		st = this;
	}
}
