using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {

	public enum Mode {
		Entering,
		Idling,
		Enraged
	}
	[HideInInspector]
	public Mode mode = Mode.Entering;

	public float enterSpeed = 10f;
	public float idleSpeed = 0f;
	public float seekSpeed = 4f;

	public Vector2 targetPosition;

	public float rateOfFire = 2f;
	public float bulletSpeed = 5f;
	public float enrageTimer = 4f;

	public int scoreValue = 100;

	public UnityEvent OnEnrage;

	[Header("Object Links")]
	public Transform[] guns;

	[Header("Gods")]
	public EnemyGod god;
	public BulletGod bulletGod;
	public ScoreGod scoreGod;

	private float _idleTime = 0f;

	public void Respawn(Vector2 startPosition, Vector2 targetPos) {
		mode = Mode.Entering;

		this.transform.position = startPosition;
		targetPosition = targetPos;
	}


	void Update() {
		if (mode == Mode.Entering) {
			float d = (((Vector2)this.transform.position) - this.targetPosition).sqrMagnitude;
			if (d < 0.5) {
				GoIdle();
			} else {
				this.transform.position = Vector2.Lerp(this.transform.position, targetPosition, Time.deltaTime * enterSpeed);
			}
		}

		if (mode == Mode.Idling) {
			UpdateGuns();

			if (Time.time - _idleTime > enrageTimer) {
				GoEnrage();
			}
		}

		if (mode == Mode.Enraged) {
			UpdateGuns();
		}
	}

	
	void GoIdle() {
		mode = Mode.Idling;
		_idleTime = Time.time;
	}

	void GoEnrage() {
		mode = Mode.Enraged;
		OnEnrage.Invoke();
	}

	private float _lastFireTime = 0f;
	void UpdateGuns() {
		if (this.guns.Length < 1) {
			return;
		}

		if (Time.time - _lastFireTime > rateOfFire) {
			for (int i = 0; i < guns.Length; i++) {
				if (guns[i].gameObject.activeInHierarchy) {
					Bullet bullet = bulletGod.SpawnBullet();

					bullet.transform.position = guns[i].position;
					Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
					bulletRB.velocity = guns[i].up * bulletSpeed;
					bullet.energy = 1;
				}
			}

			_lastFireTime = Time.time;	
		}
	}

	public void Die() {
		StartCoroutine(DeathRattle());

		if (scoreGod) {
			scoreGod.AddScore(scoreValue);
		}

		//Maybe register with god that you died.
	}

	IEnumerator DeathRattle() {
		yield return new WaitForSeconds(3f);

		Destroy(this);
	}
}
