using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeSupport : MonoBehaviour {

	public int maxLife = 6;
	public int life = 6;

	public bool soakEnergy = true;

	public UnityEvent OnDie;
	public UnityEvent OnRespawn;
	public UnityEvent OnHit;


	public void Respawn() {
		life = maxLife;

		OnRespawn.Invoke();
	}

	public int TakeHit(int energy) {
		int hitForce = 1;
		if (soakEnergy) {
			hitForce = energy;
			if (energy > life) {
				hitForce = life;
			}
		}

		life -= hitForce;
		OnHit.Invoke();
		if (life <= 0) {
			OnDie.Invoke();
		}

		return hitForce;
	}

	private void Die() {
		OnDie.Invoke();
	}
}
