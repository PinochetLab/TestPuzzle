using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData
{
	public int[] template;
	public int[] parts;
}

public class FileReader : MonoBehaviour
{
	public TextAsset textJson;
	string deskString = "";
	string[] partsStrings = new string[4] { "", "", "", "" };
	Desk desk;
	List<Figure> figures = new List<Figure>();
	List<GameObject> places = new List<GameObject>();
	[SerializeField] Transform figuresField;
	[SerializeField] GameObject figurePrefab;
	[SerializeField] GameObject pointPrefab;
	[SerializeField] Transform canvas;

	void Init() {
		desk = GameObject.FindObjectOfType<Desk>();
	}

	private void Start() {
		Init();
		LevelData levelData = JsonUtility.FromJson<LevelData>(textJson.text);

		foreach ( int cell in levelData.template ) {
			deskString += cell.ToString();
		}
		int i = 0;

		for ( int s = 0; s < levelData.parts.Length / 16; s++ ) {

		}


		for ( int s = 0; s < levelData.parts.Length / 16; s++ ) {

			GameObject go = Instantiate(pointPrefab, figuresField);
			places.Add(go);

			Figure figure = Instantiate(figurePrefab, go.transform).GetComponent<Figure>();
			figures.Add(figure);
		}

		for ( int m = 0; m < figures.Count; m++ ) {
			figures[m].transform.position = places[m].transform.position;
		}



		foreach ( int cell in levelData.parts ) {
			partsStrings[i / 16] += cell.ToString();
			i++;
		}

		StartCoroutine(SetUp());
	}

	IEnumerator SetUp() {
		yield return new WaitForSeconds(0f);
		desk.SetUp(deskString);
		for ( int j = 0; j < figures.Count; j++ ) {
			figures[j].SetUp(partsStrings[j]);
		}
	}
}
