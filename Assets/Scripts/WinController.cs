using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
	[SerializeField] GameObject winWindow;
	[SerializeField] int level;
	Desk desk;

	private void Start() {
		desk = GameObject.FindObjectOfType<Desk>();
	}
	public void WinCheck() {
		for (int i = 0; i < 4; i++ ) {
			for (int j = 0; j < 4; j++ ) {
				if ( desk.cellAvailable[i, j] && !desk.cellFills[i, j] ) return;
			}
		}

		
		PlayerPrefs.SetInt("DoneLevel", level);
		StartCoroutine(Win());
	}

	IEnumerator Win() {
		yield return new WaitForSeconds(0.5f);
		winWindow.SetActive(true);
		CanvasGroup canvasGroup = winWindow.GetComponent<CanvasGroup>();
		int n = 30;
		float time = 0.5f;
		for (int i = 0; i <= n; i++ ) {
			canvasGroup.alpha = (float)i / (float)n;
			yield return new WaitForSeconds(time / n);
		}
	}
}
