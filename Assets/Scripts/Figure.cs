using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Figure : MonoBehaviour
{

	public GameObject[,] cellsObjects = new GameObject[4, 4];
	public bool[,] cells = new bool[4, 4];
	RectTransform rectTransform;
	CellsResizer cellsResizer;
	GridLayoutGroup gridLayoutGroup;
	float scaleCoef;
	Vector3 startPos = Vector3.zero;

	private void Start() {
		Initialization();
		Init();
		
	}

	void Initialization() {
		rectTransform = GetComponent<RectTransform>();
		cellsResizer = GetComponent<CellsResizer>();
		gridLayoutGroup = GetComponent<GridLayoutGroup>();
	}

	public void Rescale() {
		gridLayoutGroup.enabled = true;
		Resize();
		Relocate();
	}

	void Init() {
		for ( int i = 0; i < transform.childCount; i++ ) {
			cellsObjects[i / 4, i % 4] = transform.GetChild(i).gameObject;
		}
	}

	public void SetUp(string str) {
		

		char[] chars = str.ToCharArray();

		for ( int i = 0; i < chars.Length; i++ ) {
			cells[i / 4, i % 4] = int.Parse(chars[i].ToString()) != 0;
		}

		for ( int i = 0; i < 4; i++ ) {
			for ( int j = 0; j < 4; j++ ) {
				cellsObjects[i, j].GetComponent<Image>().enabled = cells[i, j];
			}
		}

		Rescale();
	}

	void Resize() {
		int maxInOne = 0;

		for ( int i = 0; i < 4; i++ ) {
			int inOne = 0;
			for ( int j = 0; j < 4; j++ ) {
				if ( cells[i, j] ) inOne++;
			}
			if ( inOne > maxInOne ) maxInOne = inOne;
		}

		for ( int i = 0; i < 4; i++ ) {
			int inOne = 0;
			for ( int j = 0; j < 4; j++ ) {
				if ( cells[j, i] ) inOne++;
			}
			if ( inOne > maxInOne ) maxInOne = inOne;
		}

		scaleCoef = 4f / (float)maxInOne;
		scaleCoef = 200f / rectTransform.sizeDelta.x * Mathf.Sqrt(scaleCoef);
		rectTransform.sizeDelta *= scaleCoef;
		cellsResizer.Resize();
	}

	public void ResetPosition() {
		transform.position = startPos;
	}

	public Vector3 GetStartPos() {
		return startPos;
	}

	public void ScaleToFieldSize() {
		Vector3 lastCenter = GetCenter();
		float scale = 700f / rectTransform.sizeDelta.x;
		rectTransform.sizeDelta = 700f * Vector2.one;
		cellsResizer.Resize();
		Vector3 newCenter = transform.position + (lastCenter - transform.position) * scale;
		Vector3 move = lastCenter - newCenter;
		transform.position += move;
	}

	public Color GetColor() {
		for (int i = 0; i < 4; i++ ) {
			for (int j = 0; j < 4; j++ ) {
				Color color = cellsObjects[i, j].GetComponent<Image>().color;
				if ( color != Color.white ) return color;
			}
		}
		return Color.white;
	}

	Vector3 GetCenter() {
		Vector3 realCenter = Vector3.zero;
		int cellsCount = 0;

		for ( int i = 0; i < 4; i++ ) {
			for ( int j = 0; j < 4; j++ ) {
				if ( cells[i, j] ) {
					realCenter += cellsObjects[i, j].transform.position;
					cellsCount++;
				}
			}
		}
		if ( cellsCount < 1 ) return Vector3.zero;
		realCenter /= cellsCount;
		return realCenter;
	}

	Vector3 GetCenterOfSquare() {
		Vector3 center = Vector3.zero;
		int cellsCount = 0;

		for ( int i = 0; i < 4; i++ ) {
			for ( int j = 0; j < 4; j++ ) {
				if ( true ) {
					center += cellsObjects[i, j].transform.position;
					cellsCount++;
				}
			}
		}
		if ( cellsCount < 1 ) return Vector3.zero;
		
		center /= cellsCount;
		return center;
	}

	void Relocate() {
		Vector3 center = GetCenterOfSquare();
		Vector3 realCenter = GetCenter();
		transform.position -= (realCenter - center) * scaleCoef;
		if (startPos == Vector3.zero) startPos = transform.position;
	}
}
