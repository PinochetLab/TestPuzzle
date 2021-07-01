using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigureReset : MonoBehaviour
{
	Desk desk;
	Figure[] figures;

	private void Start() {
		desk = GetComponent<Desk>();
		figures = GameObject.FindObjectsOfType<Figure>();
	}
	public void ResetFigure(Color color) {
		print("reset color of " + color);
		bool[,] cells = new bool[4, 4];
		for (int i = 0; i < 4; i++ ) {
			for (int j = 0; j < 4; j++ ) {
				if ( !desk.cellFills[i, j] ) {
					cells[i, j] = false;
				}
				else {
					Color col = desk.cellsObjects[i, j].GetComponent<Image>().color;
					if ( col == color ) {
						cells[i, j] = true;
					}
					else {
						cells[i, j] = false;
					}
				}
				
			}
		}

		for (int m = 0; m < 4; m++ ) {
			for (int n = 0; n < 4; n++ ) {
				if (cells[m, n] ) {
					desk.cellFills[m, n] = false;
					desk.cellsObjects[m, n].GetComponent<Image>().color = Color.white;
				}
			}
		}
				
		for (int i = 0; i < figures.Length; i++ ) {
			if (figures[i].GetColor() == color ) {
				figures[i].gameObject.SetActive(true);
				break;
			}
		}
		
	}
}
