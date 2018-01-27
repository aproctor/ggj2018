using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

	public DeathGod deathGod;

	public Text livesLabel;

	private int curLives = 1;

	void Update() {
		if (deathGod.currentLives != curLives) {
			curLives = deathGod.currentLives;
			this.livesLabel.text = curLives.ToString();
		}
	}
}
