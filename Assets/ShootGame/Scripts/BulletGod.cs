using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGod : MonoBehaviour {

	public int bulletLimit = 500;
	public Bullet bulletPrefab;
	public Bullet[] bulletPool;

	private int currentOffset = 0;

	// Use this for initialization
	void Start () {
		bulletPool = new Bullet[bulletLimit];
		for (int i = 0; i < bulletLimit; i++) {
			Bullet bullet = Instantiate(bulletPrefab, this.transform) as Bullet;
			bulletPool[i] = bullet;
			bullet.gameObject.SetActive(false);
		}
	}

	public Bullet SpawnBullet() {		
		Bullet bullet = bulletPool[currentOffset];
		currentOffset = (currentOffset + 1) % bulletLimit;

		bullet.Respawn();
		return bullet;
	}

	public void TameBullets() {
		for (int i = 0; i < bulletLimit; i++) {
			if (bulletPool[i].energy > 0) {
				bulletPool[i].energy = 1;
			}
		}
	}
}
