using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FigureDragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	Vector3 offset = Vector3.zero;
	Figure figure;
	Desk desk;
	private void Start() {
		figure = GetComponent<Figure>();
		desk = GameObject.Find("Desk").GetComponent<Desk>();
	}
	public void OnBeginDrag(PointerEventData data) {
		figure.ScaleToFieldSize();
		offset = Input.mousePosition - transform.position;
	}

	public void OnDrag(PointerEventData data) {
		transform.position = Input.mousePosition - offset;
	}

	public void OnEndDrag(PointerEventData data) {
		desk.TryFill(figure);
	}
}
