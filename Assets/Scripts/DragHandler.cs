using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Pipe))]
public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	Vector3 startPosition;
	Pipe pipe;
	[HideInInspector]
	public Slot startSlot;

	void Start(){
		pipe = GetComponent<Pipe> ();
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		GetComponent<Image> ().raycastTarget = false;
		GameManager.pickedItem = gameObject;
		startPosition = transform.position;
		startSlot = pipe.currentSlot;
		pipe.changeSlot (null);
	}

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		GetComponent<Image> ().raycastTarget = true;
		GameManager.pickedItem = null;

		if (pipe.currentSlot == null) {
			pipe.changeSlot (startSlot);
		}
	}

}
