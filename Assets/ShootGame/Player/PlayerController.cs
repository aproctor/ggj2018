using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

	public bool activated = true;

	[HideInInspector]
	public InputDevice device = null;

	public int lives = 3;

	//Movement
	private Vector2 lookDirection = Vector2.zero;
	public float minMoveAmt = 0.05f;
	public float moveSpeed = 1f;
	public float boostMultiplier = 3f;
	public bool boosting = false;

	[Header("Object Links")]
	public Rigidbody2D rbody;
	public LifeSupport lifeSupport;
	public Transform firePoint1;

	[Header("Gods")]
	public BulletGod bulletGod;
	public DeathGod deathGod;

	public bool canRespawn = false;
	public bool Alive { 
		get {
			return this.lifeSupport.life > 0;
		}
	}

	void Update() {
		if (device == null && InputManager.Devices.Count >= 1) {
			Debug.Log("Controller online");
			device = InputManager.Devices[0];
		}

		if (device != null && activated) {
			if (this.Alive) {
				UpdateMovement();
				UpdateFireControlls();
			} else {
				if (device.Action1) {
					if (canRespawn) {
						deathGod.TryRespawn();
					}
				}
			}
		}
	}

	void UpdateMovement() {
		lookDirection = device.Direction.Vector;

		boosting = device.Action2;

	
		float movePower = lookDirection.sqrMagnitude;
		if (movePower > minMoveAmt) {

			//rotate ship
			this.transform.rotation = Quaternion.Euler(0f, 0f, -device.Direction.Angle);

			float speed = moveSpeed;
			if (boosting) {
				speed = speed * boostMultiplier;
			}
			this.rbody.velocity = lookDirection * speed;
		}			
	}

	private float _lastFireTime = 0f;
	public float fireRate = 0.2f;
	public float fireSpeed = 3f;
	void UpdateFireControlls() {
		if (device.Action1) {
			if (Time.time - _lastFireTime > fireRate) {
				//Debug bullet fire

				Bullet bullet = bulletGod.SpawnBullet();
				bullet.transform.position = firePoint1.position;
				Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
				bulletRB.velocity = this.transform.up * fireSpeed;

				_lastFireTime = Time.time;
			}
		}
	}

	public void Die() {		
		StartCoroutine(DeathThrows());
	}

	IEnumerator DeathThrows() {
		yield return new WaitForSeconds(2f);

		canRespawn = true;
	}

	public void Respawn() {
		canRespawn = false;

		this.lifeSupport.Respawn();
	}


	void OnDrawGizmos() {
		if (lookDirection.sqrMagnitude > minMoveAmt) {
			Gizmos.color = Color.green;
		} else {
			Gizmos.color = Color.red;
		}

		Gizmos.DrawCube(this.transform.position + new Vector3(lookDirection.x, lookDirection.y, 0), Vector3.one*0.05f);
	}
}
