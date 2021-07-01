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
		float distanceMax = 50;
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
			figure.Rescale();
			figure.ResetPosition();
		}
	}
}
