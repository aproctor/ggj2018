using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

	public bool activated = true;

	[HideInInspector]
	public InputDevice device = null;

	//Movement
	private Vector2 lookDirection = Vector2.zero;
	public float minMoveAmt = 0.05f;
	public float moveSpeed = 1f;

	[Header("Object Links")]
	public Rigidbody2D rigidbody;

	public GameObject debugBulletPrefab;
	public Transform firePoint1;


	void Update() {
		if (device == null && InputManager.Devices.Count >= 1) {
			Debug.Log("Controller online");
			device = InputManager.Devices[0];
		}

		if (device != null && activated) {
			UpdateMovement();
			UpdateFireControlls();
		}
	}

	void UpdateMovement() {
		lookDirection = device.Direction.Vector;

		//rotate ship
		this.transform.rotation = Quaternion.Euler(0f, 0f, -device.Direction.Angle);

		float movePower = lookDirection.sqrMagnitude;
		if (movePower > minMoveAmt) {
			this.rigidbody.AddForce(lookDirection * moveSpeed);
		}			
	}

	private float _lastFireTime = 0f;
	public float fireRate = 0.2f;
	public float fireSpeed = 3f;
	void UpdateFireControlls() {
		if (device.Action1) {
			if (Time.time - _lastFireTime > fireRate) {
				Debug.LogError("Pow1");	

				//Debug bullet fire
				GameObject bullet = GameObject.Instantiate(debugBulletPrefab, firePoint1.position, Quaternion.identity);
				Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
				bulletRB.AddForce(Vector2.up*fireSpeed);

				_lastFireTime = Time.time;
			}
		}
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
