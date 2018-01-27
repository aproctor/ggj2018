using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour {

	public int maxEnergy = 5;
	public int energy = 5;

	public void Respawn() {
		energy = maxEnergy;
		this.gameObject.SetActive(true);
	}
		
	void OnCollisionEnter2D(Collision2D collision) {
		LifeSupport lifeSupport = collision.collider.gameObject.GetComponent<LifeSupport>();
		if (lifeSupport) {
			this.energy -= lifeSupport.TakeHit(this.energy);
		} else {
			this.energy -= 1;
		}

		if (this.energy <= 0) {
			Die();
		}
	}

	void Die() {
		this.gameObject.SetActive(false);
	}
}
