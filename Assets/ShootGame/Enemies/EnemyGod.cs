using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGod : MonoBehaviour {

	[Header("Gods")]
	public BulletGod bulletGod;
	public ScoreGod scoreGod;

	[Header("Enemy Types")]
	public Enemy roundboyPrefab;
	public Enemy catboyPrefab;


	private int wave = 0;
	private float _lastWaveTime = 0f;
	public float timeBetweenWaves = 30f;

	public Transform[] spawnLocations;

	void Update() {
		if (wave == 0 || Time.time - _lastWaveTime > timeBetweenWaves) {
			SpawnWave();
		}
	}


	void SpawnWave() {
		_lastWaveTime = Time.time;

		wave += 1;

		Debug.Log("Wave " + wave);

		//Spawn Cats
		int numCats = wave % 3 + 1;
		for (int i = 0; i < numCats; i++) {
			Enemy cat = Instantiate(catboyPrefab) as Enemy;
			Vector2 targetPosition = RandomSpawnLocation();
			cat.transform.localScale = new Vector3(targetPosition.x > 0 ? 1f : -1f, 1f, 1f);

			cat.god = this;
			cat.bulletGod = bulletGod;
			cat.Respawn(targetPosition * 4, targetPosition);
		}

		//Spawn RoundBoys
		if (wave % 5 == 0) {
			//Spawn RoundBoys 
			int numRoundboys = wave / 5;
			for (int i = 0; i < numRoundboys; i++) {
				Enemy roundboy = Instantiate(roundboyPrefab) as Enemy;
				roundboy.god = this;
				roundboy.bulletGod = bulletGod;

				Vector2 targetPosition = RandomSpawnLocation();
				roundboy.Respawn(targetPosition * 4f, targetPosition);
			}

		}

	}

	Vector2 RandomSpawnLocation() {
		return ((Vector2)spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Length)].position) + new Vector2(Random.value*3-1, Random.value*3-1);
	}
}
