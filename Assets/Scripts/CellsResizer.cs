using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CellsResizer : MonoBehaviour
{
	[SerializeField] RectTransform rectTransform;
	[SerializeField] GridLayoutGroup gridLayoutGroup;

	private void Start() {
		Resize();
	}

	public void Resize() {
		if ( !gridLayoutGroup || !rectTransform ) return;
		Vector2 size = rectTransform.sizeDelta;
		float minSize = Mathf.Min(size.x, size.y);
		gridLayoutGroup.cellSize = Vector2.one * 150f * minSize / 700;
		gridLayoutGroup.spacing = Vector2.one * 20f * minSize / 700;
		foreach (Transform child in transform ) {
			child.GetComponent<Outline>().effectDistance = Vector2.one * 20f * minSize / 700;
		}
	}
	private void Update() {
	//	if ( EditorApplication.isPlaying ) return;
	//	Resize();
	}
}

