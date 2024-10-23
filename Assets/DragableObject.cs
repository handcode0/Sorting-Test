using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class DragableObject : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler {

	public float distanceBetCam = 0;
	public Vector3 defaultPos;
	public bool Reached = false;
	public Vector3 Position;

	public void OnDrag(PointerEventData data)
	{
		
		Vector2 touchPosition = data.position;
		Vector3 touchPositionInWorld = Camera.main.ScreenToWorldPoint(
			new Vector3(
				touchPosition.x,
				touchPosition.y,
				distanceBetCam)
		);

		transform.position = touchPositionInWorld;

	}

	public void OnBeginDrag(PointerEventData data)
	{
		if(GenerateBox.instance) GenerateBox.instance.CurrentObject = this;
        Debug.Log("Drag Begin!");
		defaultPos = transform.position;
	}

	public void OnEndDrag(PointerEventData data)
	{
        if (!Reached)
        {
			transform.position = defaultPos;
			GenerateBox.instance.CurrentObject = null;
        }
        

		GenerateBox.instance.CheckCompleteLevel();
	}
}
