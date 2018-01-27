using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGod : MonoBehaviour {

	public int bulletLimit = 500;
	public GameObject bulletPrefab;
	public GameObject[] bulletPool;

	private int currentOffset = 0;

	// Use this for initialization
	void Start () {
		bulletPool = new GameObject[bulletLimit];
		for (int i = 0; i < bulletLimit; i++) {
			GameObject bullet = Instantiate(bulletPrefab, this.transform) as GameObject;
			bulletPool[i] = bullet;
			bullet.SetActive(false);
		}
	}

	public GameObject SpawnBullet() {		
		GameObject bullet = bulletPool[currentOffset];
		currentOffset = (currentOffset + 1) % bulletLimit;

		bullet.SetActive(true);
		return bullet;
	}
}
