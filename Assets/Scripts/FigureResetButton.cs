using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigureResetButton : MonoBehaviour
{
	public FigureReset figureReset;
	Image image;
	public Desk desk;

	private void Start() {
		image = GetComponent<Image>();
	}

	public void ResetFigure() {
		int i = 0, j = 0;
		for (int m = 0; m < 4; m++ ) {
			for (int n = 0; n < 4; n++ ) {
				if (desk.cellsObjects[m, n] == gameObject ) {
					i = m; j = n;
					break;
				}
			}
		}

		if ( !desk.cellFills[i, j] ) return;
		figureReset.ResetFigure(image.color);
	}
}
