using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressLoader : MonoBehaviour
{
	int levelToGo = 1;
	[SerializeField] Text levelText;
	private void Start() {
		int doneLevel = 0;
		if ( PlayerPrefs.HasKey("DoneLevel") ) {
			doneLevel = PlayerPrefs.GetInt("DoneLevel");
		}
		levelToGo = doneLevel + 1;
		if (doneLevel == 2 ) {
			levelToGo = 1;
			PlayerPrefs.DeleteKey("DoneLevel");
		}
		levelText.text = "Уровень " + levelToGo.ToString();
	}

	public void GoToGame() {
		GameObject.FindObjectOfType<SceneChange>().GoToScene("Level" + levelToGo);
	}
}
