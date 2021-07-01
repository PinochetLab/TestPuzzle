using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollorSetter : MonoBehaviour
{
	public List<Color> allColors;
	Figure[] figures;

	private void Start() {
		figures = GameObject.FindObjectsOfType<Figure>();
		for (int i = 0; i < figures.Length; i++ ) {
			Color color = allColors[Random.Range(0, allColors.Count)];
			allColors.Remove(color);
			foreach (Transform child in figures[i].transform) {
				child.GetComponent<Image>().color = color;
			}
		}
	}
}
