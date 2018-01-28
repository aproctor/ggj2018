using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGod : MonoBehaviour {

	private int _score = 0;
	public int Score {
		get {
			return _score;
		}
	}

	public void AddScore(int value) {
		_score += value;
	}


}
