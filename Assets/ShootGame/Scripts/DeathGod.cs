using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGod : MonoBehaviour {

	public int maxLives = 5;
	public int currentLives = 3;

	[Header("Object links")]
	public Transform spawnPoint;
	public PlayerController player;

	public void AddLife() {
		maxLives = Mathf.Clamp(currentLives + 1, 0, maxLives);
	}

	public void TryRespawn() {
		if (currentLives > 0) {
			currentLives -= 1;

			player.transform.position = spawnPoint.transform.position;
			player.Respawn();
		}
	}
}
