using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desk : MonoBehaviour
{
	public GameObject[,] cellsObjects = new GameObject[4, 4];
	public bool[,] cellFills = new bool[4, 4];
	public bool[,] cellAvailable = new bool[4, 4];

	public void SetUp(string str) {
		for ( int i = 0; i < transform.childCount; i++ ) {
			cellsObjects[i / 4, i % 4] = transform.GetChild(i).gameObject;
			cellFills[i / 4, i % 4] = false;
		}
		char[] chars = str.ToCharArray();
		for ( int i = 0; i < chars.Length; i++ ) {
			cellAvailable[i / 4, i % 4] = int.Parse(chars[i].ToString()) != 0;
			cellsObjects[i / 4, i % 4].GetComponent<Image>().enabled = cellAvailable[i / 4, i % 4];
		}
	}

	public void TryFill(Figure figure) {
		float distanceMax = 80;
		bool can = true;
		for (int i = 0; i < 4; i++ ) {
			for (int j = 0; j < 4; j++ ) {
				if (figure.cells[i, j] ) {
					float minDistance = 100f;
					for ( int m = 0; m < 4; m++ ) {
						for (int n = 0; n < 4; n++ ) {
							if (!cellFills[m, n] && cellAvailable[m, n]) {
								float dist = (figure.cellsObjects[i, j].transform.position - cellsObjects[m, n].transform.position).magnitude;
								if ( dist < minDistance ) {
									minDistance = dist;
								}
							}
						}
					}

					if (minDistance > distanceMax ) {
						can = false;
						print(minDistance);
						break;
					}
				}
			}
		}

		if ( can ) {
			for (int i = 0; i < 4; i++ ) {
				for (int j = 0; j < 4; j++ ) {
					if (figure.cells[i, j] ) {
						for ( int m = 0; m < 4; m++ ) {
							for ( int n = 0; n < 4; n++ ) {
								if (!cellFills[m, n] && cellAvailable[m, n]) {
									float dist = (figure.cellsObjects[i, j].transform.position - cellsObjects[m, n].transform.position).magnitude;
									if (dist < distanceMax ) {
										cellFills[m, n] = true;
										cellsObjects[m, n].GetComponent<Image>().color =
											figure.cellsObjects[i, j].GetComponent<Image>().color;
										
									}
								}
							}
						}
					}
				}
			}
			figure.Rescale();
			figure.ResetPosition();
			figure.gameObject.SetActive(false);

			GameObject.FindObjectOfType<WinController>().WinCheck();
		}

		

		else{
			StartCoroutine(ReturnToPlace(figure));
		}
	}

	IEnumerator ReturnToPlace(Figure figure) {
		Vector3 nowPos = figure.transform.position;
		RectTransform rectTransform = figure.GetComponent<RectTransform>();
		CellsResizer cellResizer = figure.GetComponent<CellsResizer>();
		Vector2 nowScale = rectTransform.sizeDelta;
		
		figure.Rescale();
		figure.ResetPosition();
		Vector3 targetPos = figure.transform.position;
		print(targetPos);
		Vector2 targetScale = rectTransform.sizeDelta;



		figure.transform.position = nowPos;
		rectTransform.sizeDelta = nowScale;
		cellResizer.Resize();
		float speed = 2000f;
		float dist = (nowPos - targetPos).magnitude;
		
		int n = (int)((dist / speed) / 0.01f);
		for (int i = 0; i <= n; i++ ) {
			figure.transform.position = nowPos + (targetPos - nowPos) * (float)(i) / (float)(n);
			rectTransform.sizeDelta = nowScale + (targetScale - nowScale) * (float)(i) / (float)(n);
			cellResizer.Resize();
			yield return new WaitForSeconds(dist / speed / n);
		}
	}
}
