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

		winWindow.SetActive(true);
		PlayerPrefs.SetInt("DoneLevel", level);
	}
}
