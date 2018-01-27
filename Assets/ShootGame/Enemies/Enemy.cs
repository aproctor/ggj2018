using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public enum Mode {
		Entering,
		Idling,
		Seeking
	}
	[HideInInspector]
	public Mode mode = Mode.Entering;

	public float enterSpeed = 10f;
	public float idleSpeed = 0f;
	public float seekSpeed = 4f;

	public Vector2 targetPosition;

	public float rateOfFire = 2f;

	[Header("Object Links")]
	public Transform[] guns;

	[Header("Gods")]
	public EnemyGod god;
	public BulletGod bulletGod;


	public void Respawn(Vector2 startPosition, Vector2 targetPos) {
		mode = Mode.Entering;

		this.transform.position = startPosition;
		targetPosition = targetPos;
	}


	void Update() {
		if (mode == Mode.Entering) {
			float d = (((Vector2)this.transform.position) - this.targetPosition).sqrMagnitude;
			if (d < 0.5) {
				mode = Mode.Idling;
			} else {
				this.transform.position = Vector2.Lerp(this.transform.position, targetPosition, Time.deltaTime * enterSpeed);
			}
		}

		if (mode == Mode.Idling) {
			UpdateGuns();
		}
	}

	private float _lastFireTime = 0f;
	void UpdateGuns() {
		if (Time.time - _lastFireTime > rateOfFire) {
		}
	}
}
