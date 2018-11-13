using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

	public Slot top;
	public Slot bottom;
	public Slot right;
	public Slot left;

	[HideInInspector]
	public StartPipe startPipe;
	//[HideInInspector]
	public Pipe pipe;
	[HideInInspector]
	public WinPipe winPipe;


	public void OnDrop (PointerEventData eventData)
	{
		GameManager.pickedItem.GetComponent<Pipe> ().changeSlot (this);
	}
}
